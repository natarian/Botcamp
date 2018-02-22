using UnityEngine;
using System.Collections;

public class Robot : MonoBehaviour {

	public RobotData rd; 

	public GameObject chassis;
	public GameObject[] wheelAnchors;
	public GameObject[] armAnchors;

	public Wheel[] wheels;
	public Claw[] arms;
	//public Sensor[] sensors

	//visual script data

	public bool ready = false; //not used yet

	public void goForward(){
		foreach (Wheel w in wheels){
			w.TurnOn ();
		}
	}
	public void stop(){
		foreach (Wheel w in wheels){
			w.TurnOff ();
		}
	}
	public void goBackward(){
		foreach (Wheel w in wheels){
			w.TurnReverse ();
		}
	}
	public void turnRight(){
		foreach (Wheel w in wheels){
			w.TurnRight ();
		}
	}
	public void turnLeft(){
		foreach (Wheel w in wheels){
			w.TurnLeft ();
		}
	}

	public void ClawToggle(){
		foreach (Claw c in arms){
			c.toggle ();
		}
	}

}
