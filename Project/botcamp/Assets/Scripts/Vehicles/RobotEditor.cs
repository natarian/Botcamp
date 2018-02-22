using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RobotEditor : MonoBehaviour {
	GameObject robot;
	Robot r;
	RobotData rd;

	public GameObject chassisPanel;
	public GameObject WheelsPanel;
	public GameObject ArmsPanel;
	public GameObject SensorsPanel;
	public GameObject blocksPanel;

	public GameObject templateMenuObj;


	void Start(){
		Invoke ("setup", .5f);
	}

	public void setup(){
		robot  = GameObject.Find ("Robot") as GameObject;

		if (!robot) {
			Debug.LogError ("Couldn't find robot in setup for RobotEditor");
			gameObject.SetActive (false);
			return;
		}
		r = robot.GetComponent<Robot> ();
		rd = r.rd;

		populatePages ();
	}

	//setup editor pages
	void populatePages(){
		foreach(Object o in PartData.chassis){
			GameObject go = (GameObject)o;
			GameObject panel = Instantiate (templateMenuObj) as GameObject;
			panel.transform.SetParent (chassisPanel.transform);
			panel.name = go.name;
			panel.SetActive (true);
			panel.transform.localScale = new Vector3 (1, 1, 1);

			Image i = panel.GetComponent<Image> ();
			Texture2D tex = PartData.findThumbnail (panel.name);
			i.sprite = Sprite.Create(tex, new Rect(0,0,tex.width,tex.height), new Vector2(.5f, .5f));
		}
		foreach(Object o in PartData.wheels){
			GameObject go = (GameObject)o;
			GameObject panel = Instantiate (templateMenuObj) as GameObject;
			panel.transform.SetParent (WheelsPanel.transform);
			panel.name = go.name;
			panel.SetActive (true);
			panel.transform.localScale = new Vector3 (1, 1, 1);

			Image i = panel.GetComponent<Image> ();
			Texture2D tex = PartData.findThumbnail (panel.name);
			i.sprite = Sprite.Create(tex, new Rect(0,0,tex.width,tex.height), new Vector2(.5f, .5f));
		}
		foreach(Object o in PartData.arms){
			GameObject go = (GameObject)o;
			GameObject panel = Instantiate (templateMenuObj) as GameObject;
			panel.transform.SetParent (ArmsPanel.transform);
			panel.name = go.name;
			panel.SetActive (true);
			panel.transform.localScale = new Vector3 (1, 1, 1);

			Image i = panel.GetComponent<Image> ();
			Texture2D tex = PartData.findThumbnail (panel.name);
			i.sprite = Sprite.Create(tex, new Rect(0,0,tex.width,tex.height), new Vector2(.5f, .5f));
		}
		foreach(Object o in PartData.sensors){
			GameObject go = (GameObject)o;
			GameObject panel = Instantiate (templateMenuObj) as GameObject;
			panel.transform.SetParent (SensorsPanel.transform);
			panel.name = go.name;
			panel.SetActive (true);
			panel.transform.localScale = new Vector3 (1, 1, 1);

			Image i = panel.GetComponent<Image> ();
			Texture2D tex = PartData.findThumbnail (panel.name);
			i.sprite = Sprite.Create(tex, new Rect(0,0,tex.width,tex.height), new Vector2(.5f, .5f));
		}
	}

	public void loadPart(GameObject obj){
		if (PartData.findChassis (obj.name) != null)
			loadChassis (obj.name);
		if (PartData.findWheel(obj.name) != null)
			loadWheels (obj.name);
		if (PartData.findArm(obj.name) != null)
			loadArm (obj.name);
		//sensors and arms
	}

	public void loadChassis (string partName){
		removePart ("Chassis");

		GameObject c = Instantiate (PartData.findChassis (partName)) as GameObject;
		//robot.AddComponent<Robot> ();
		c.transform.SetParent (robot.transform);

		r.wheelAnchors = new GameObject[4];
		r.wheels = new Wheel[4];
		r.armAnchors = new GameObject[1];
		int count = 0;
		foreach (Transform t in c.transform) {
			if (t.gameObject.tag == "Anchor" && t.gameObject.name == "WheelAnchor") {
				r.wheelAnchors [count] = t.gameObject;
				count++;
			}
			if (t.gameObject.tag == "Anchor" && t.gameObject.name == "ArmAnchor") {
				r.armAnchors [0] = t.gameObject;
			}
		}
		c.name = "Chassis";
	}
	public void loadWheels (string partName){
		if (!robot.transform.Find ("Chassis")) {
			return;
		}

		for (int a = 0; a < 4; a++){ 
			removeAttached ("Wheel");
		}

		int count = 0;
		foreach (GameObject anchor in r.wheelAnchors) {
			GameObject w = Instantiate (PartData.findWheel (partName));
			w.transform.SetParent (robot.transform.Find("Chassis"));
			w.transform.position = anchor.transform.position;
			Vector3 rot = anchor.transform.rotation.eulerAngles;
			rot.x = 0;
			rot.y = 0;
			w.transform.localRotation = Quaternion.Euler(rot);

			Wheel wheel = w.GetComponent<Wheel> ();
			if (anchor.transform.localPosition.y > 0) {
				w.transform.Rotate (new Vector3 (180, 0, 0));
				wheel.backwards = true;
			} else {
				w.transform.Rotate (new Vector3 (180, 0, 0));
				wheel.backwards = false;
			}
			r.wheels [count] = wheel;
			count++;
			//w.transform.rotation = anchor.transform.rotation;

			HingeJoint hj = w.GetComponent<HingeJoint> ();
			hj.connectedBody = robot.transform.Find("Chassis").gameObject.GetComponent<Rigidbody>();
			hj.axis = new Vector3 (0, 1, 0);
			w.name = "Wheel";
		}
	}
	public void loadArm (string partName){
		if (!robot.transform.Find ("Chassis")) {
			return;
		}

		while (removeAttached ("Arm"));
		GameObject anchor = r.armAnchors [0];

		GameObject w = Instantiate (PartData.findArm(partName));
		w.transform.SetParent (robot.transform.Find("Chassis"));
		w.transform.position = anchor.transform.position;
		Vector3 rot = anchor.transform.rotation.eulerAngles;
		rot.z = 0;
		rot.y = 0;
		w.transform.localRotation = Quaternion.Euler(rot);
		w.transform.Rotate (new Vector3(0, 90, 90));

		Claw c = w.GetComponent<Claw> ();
		HingeJoint[] hjs = w.GetComponentsInChildren<HingeJoint> ();
		foreach (HingeJoint hj in hjs) {
			hj.connectedBody = robot.transform.Find("Chassis").gameObject.GetComponent<Rigidbody>();
			Vector3 pos = hj.gameObject.transform.position;
			hj.connectedAnchor = pos;
			hj.gameObject.transform.position = pos;
		}

		w.name = "Arm";
	}

	public bool removePart(string part){
		GameObject p = GameObject.Find(part) as GameObject;
		if (p){
			p.transform.SetParent (null);
			Destroy (p);
			return true;
		} else {
			Debug.LogWarning("Not found: Part " + part);
			return false;
		}
	}
	public bool removeAttached(string part){
		GameObject chassis = robot.transform.Find ("Chassis").gameObject;
		if (!chassis) {
			return false;
		}
		Transform p = chassis.transform.Find (part);
		if (p){
			p.SetParent (null);
			Destroy (p.gameObject);
			return true;
		} else {
			Debug.LogWarning("Not found: Part " + part);
			return false;
		}
	}

	public void Begin(){
		r.gameObject.GetComponent<KeyController> ().ready = true; //testing
		gameObject.SetActive (false);

	}
}
