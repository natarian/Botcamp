using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	public float autoLoadTime = 5f;
	private bool skippable = false;

	void OnAwake(){
		//persistant
		//DontDestroyOnLoad (this.gameObject);
	}
	void Start () {
		//let the splash screen display then goto next
		if (autoLoadTime != 0) {
			Invoke ("LoadNextLevel", autoLoadTime);
			skippable = true; 
		}
	}
	public void LoadLevel(string level){
		Application.LoadLevel (level);
	}
	public void LoadNextLevel(){
		if (Application.loadedLevel < Application.levelCount - 1) {
			Debug.Log ("Loading next level: Level " + Application.loadedLevel + 1);
			//load the next level in build order
			Application.LoadLevel (Application.loadedLevel + 1);
		} else {
			Debug.LogError("Invalid Level Index.");
		}
	}
	public void GotoLevel(int level){
		if (level > 0 && level < Application.levelCount) {
			//load the level
			Application.LoadLevel (level);
		} else {
			Debug.LogError("Invalid Level Index.");
		}
	}
	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}
	public void reloadLevel(){
	}
	void Update(){
		if (skippable && Input.anyKeyDown) {
			this.LoadNextLevel ();
		}
	}
}

