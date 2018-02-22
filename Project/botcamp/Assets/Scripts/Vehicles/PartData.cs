using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PartData : MonoBehaviour {

	public static Object[] chassis;
	public static Object[] wheels;
	public static Object[] arms; //eh
	public static Object[] sensors; //eh

	public static Object[] thumbs;

	public static List<Texture2D> thumbnails = new List<Texture2D>();
	void Start () {
		chassis = Resources.LoadAll ("VehicleParts/Chassis");
		wheels = Resources.LoadAll ("VehicleParts/Wheels");
		arms = Resources.LoadAll ("VehicleParts/Arms");
		sensors = Resources.LoadAll ("VehicleParts/Sensors");
		thumbs = Resources.LoadAll ("Thumbs");

		foreach (Texture2D img in thumbs)
			thumbnails.Add (img);
	}
	public static  GameObject findChassis(string name){
		foreach (GameObject o in chassis) {
			if (o.name.Equals (name)) {
				return o;
			}
		}
		return null;
	}
	public static GameObject findWheel(string name){
		foreach (GameObject o in wheels) {
			if (o.name.Equals (name)) {
				return o;
			}
		}
		return null;
	}
	public static  GameObject findArm(string name){
		foreach (GameObject o in arms) {
			if (o.name.Equals (name)) {
				return o;
			}
		}
		return null;
	}
	public static  GameObject findSensor(string name){
		foreach (GameObject o in sensors) {
			if (o.name.Equals (name)) {
				return o;
			}
		}
		return null;
	}
	public static  Texture2D findThumbnail(string name){
		foreach (Texture2D o in thumbnails) {
			if (o.name.Equals ("Thumb_"+name)) {
				return o;
			}
		}
		return null;
	}
}
