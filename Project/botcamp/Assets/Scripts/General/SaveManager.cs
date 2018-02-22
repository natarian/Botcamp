using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

public class SaveManager : MonoBehaviour {

	public static SaveManager saveManager;

	//directories
	public string savePath = "";

	//file types
	private string savExt = ".sav";

	//settingsvariables
	public float masterVolume = 1f;
	public float musicVolume = 1f;
	public float sfxVolume = 1f;
	public int antiAliasing = 0; //0,2,4,8
	
	//other variables like time played and crap. add when needed

	//singleton manager
	void Awake(){
		//singleton
		if (saveManager == null) {
			DontDestroyOnLoad (gameObject);
			saveManager = this;
		} else if (saveManager != this) {
			Destroy(gameObject);
		}
		//set directory paths
		savePath = Application.persistentDataPath;

		if (!Directory.Exists(savePath)){
			Directory.CreateDirectory(savePath);
		}
	}
	//------------------------------------------------
	//r save
	//------------------------------------------------
	public RobotData newRobot(string newRobot)
	{
		if (!File.Exists (savePath + newRobot + savExt)) {
			Debug.Log("Creating new Robot: " + newRobot);
			RobotData r = new RobotData();
			r.botName = newRobot;
			saveRobot (r);
			return r;
		} else {
			Debug.Log ("A robot with this name already exists");
			return null;
		}
	}
	public void saveRobot(RobotData r){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (savePath + r.botName +savExt);

		bf.Serialize (file, r);
		file.Close();
	}
	public Robot loadRobot(string name){
		if (File.Exists (savePath + name +savExt)) 
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (savePath + name +savExt, FileMode.Open);
			Robot r = (Robot)bf.Deserialize(file);
			file.Close();
			return r;
		}
		return null;
	}
	public void deleteRobot(string name)
	{
		if (File.Exists (savePath + name + savExt)) {
			File.Delete (savePath + name + savExt);
		} else {
			Debug.LogError("No robot exists with that name: " + name);
		}
	}
	public string[] getSaves()
	{
		//return Directory.GetFiles (savePath);

		//just names
		IEnumerable<FileInfo> saves = Directory.GetFiles (savePath).Select (f => new FileInfo (f)).OrderByDescending (f => f.CreationTime);
		string[] saveFiles = new string[saves.Count()];
		int i = 0;
		foreach(FileInfo f in saves){
			saveFiles[i] = f.Name.Substring(0,f.Name.Length-savExt.Length);
			i++;
		}
		i = 0;
		return saveFiles;
	}
	//------------------------------------------------
	//config save
	//------------------------------------------------
	public void saveSettings()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "./config.ini");

		ConfigSettings cs = new ConfigSettings ();
		cs.masterVolume = masterVolume;
		cs.musicVolume = musicVolume;
		cs.sfxVolume = sfxVolume;
		cs.antiAliasing = antiAliasing;

		bf.Serialize (file, cs);
		file.Close ();
	}
	public void loadSettings(){
		if (File.Exists (Application.persistentDataPath + "./config.ini")) 
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "./config.ini", FileMode.Open);
			ConfigSettings cs = (ConfigSettings)bf.Deserialize(file);
			file.Close();

			masterVolume = cs.masterVolume;
			musicVolume = cs.musicVolume;
			sfxVolume = cs.sfxVolume;
			antiAliasing = cs.antiAliasing;
		}
	}
}

[System.Serializable]
class ConfigSettings
{
	//settingsvariables
	public float masterVolume = 1f;
	public float musicVolume = 1f;
	public float sfxVolume = 1f;
	public int antiAliasing = 0; //0,2,4,8	
}
