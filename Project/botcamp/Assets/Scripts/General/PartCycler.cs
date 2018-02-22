using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PartCycler : MonoBehaviour {

	public int currentIndex = 0;
	public List<GameObject> parts = new List<GameObject>();

	public Vector3 pos1;
	public Vector3 pos2;
	public Vector3 pos3;
	public Vector3 pos4;

	public int thumbnailRes = 256;
	public string pathname = "";
	
	void Start () {
		Invoke ("init", .1f);
	}
	void init(){//call it late to instantiate parts
		//get the items for the list
		Debug.Log("boom");
		foreach(GameObject obj in PartData.chassis) parts.Add(obj);
		foreach(GameObject obj in PartData.wheels) parts.Add(obj);
		foreach(GameObject obj in PartData.arms) parts.Add(obj);
		foreach(GameObject obj in PartData.sensors) parts.Add(obj);
		
		//currentIndex = Random.Range (0, parts.Count - 1);
		displayPart ();
	}
	public void displayPart(){
		//remove all children
		foreach (Transform child in transform) {
			Destroy (child.gameObject);
		}
		GameObject obj = Instantiate (parts[currentIndex], transform.position, gameObject.transform.rotation) as GameObject;
		obj.transform.SetParent (gameObject.transform);
		obj.name = parts[currentIndex].name;

		Rigidbody rb = obj.GetComponent<Rigidbody> ();
		if (rb)
			rb.useGravity = false;
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha1)){
			gameObject.transform.eulerAngles = pos1;
		} else if (Input.GetKeyDown(KeyCode.Alpha2)){
			gameObject.transform.eulerAngles = pos2;
		} else if (Input.GetKeyDown(KeyCode.Alpha3)){
			gameObject.transform.eulerAngles = pos3;
		} else if (Input.GetKeyDown(KeyCode.Alpha4)){
			gameObject.transform.eulerAngles = pos4;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha0)){
			gameObject.transform.eulerAngles = Vector3.zero;
		}

		if (Input.GetKeyDown(KeyCode.RightArrow)){
			if (currentIndex < parts.Count-1){
				currentIndex++;
			} else {
				currentIndex=0;
			}
			displayPart();
		} else if (Input.GetKeyDown(KeyCode.LeftArrow)){
			if (currentIndex > 0){
				currentIndex--;
			} else {
				currentIndex=parts.Count-1;
			}
			displayPart();
		}

		//movement
		if (Input.GetKey(KeyCode.W)){
			transform.position += new Vector3(0,Time.deltaTime*3,0);
		}else if (Input.GetKey(KeyCode.S)){
			transform.position += new Vector3(0,Time.deltaTime*-3,0);
		}
		if (Input.GetKey(KeyCode.A)){
			transform.position += new Vector3(Time.deltaTime*3,0,0);
		}else if (Input.GetKey(KeyCode.D)){
			transform.position += new Vector3(Time.deltaTime*-3,0,0);
		}
		if (Input.GetKey(KeyCode.DownArrow)){
			transform.position += new Vector3(0,0,Time.deltaTime*5);
		}else if (Input.GetKey(KeyCode.UpArrow)){
			transform.position += new Vector3(0,0,Time.deltaTime*-5);
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			this.takeScreenShot();
		}

		//reset
		if (Input.GetKeyDown (KeyCode.R)) {
			transform.position = Vector3.zero;
		}
	}

	public void takeScreenShot(){
		Camera camera = Camera.main;
		RenderTexture rt = new RenderTexture(thumbnailRes,thumbnailRes, 24);
		camera.targetTexture = rt;
		Texture2D screenShot = new Texture2D(thumbnailRes,thumbnailRes, TextureFormat.RGB24, false);
		camera.Render();
		RenderTexture.active = rt;
		screenShot.ReadPixels(new Rect(0, 0, thumbnailRes,thumbnailRes), 0, 0);
		camera.targetTexture = null;
		RenderTexture.active = null; // JC: added to avoid errors
		Destroy(rt);
		byte[] bytes = screenShot.EncodeToPNG();
		//save to specific location
		string filename = "C:/Users/William/Documents/unity projects/Unity/Projects/Bitcamp 2016/Assets/Prefabs/Resources/Thumbs/"+ "Thumb_" + parts[currentIndex].name +".png";
		System.IO.File.WriteAllBytes(filename, bytes);
		Debug.Log(string.Format("Took screenshot to: {0}", filename));
	}
}
