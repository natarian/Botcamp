using UnityEngine;
using System.Collections;

public class Wheel : MonoBehaviour {

	public float strength; //pushing power
	public float power; //0-1, % of strength

	private HingeJoint hinge;

	public bool backwards;

	void Start(){
		hinge = GetComponent<HingeJoint> ();
	}

	//basic
	public void TurnOn(){
		if (backwards) {
			power = -1;
		} else {
			power = 1;
		}
		hinge.useMotor = true;
		JointMotor jm = hinge.motor;
		jm.targetVelocity = power * strength;
		hinge.motor = jm;
	}
	public void TurnReverse(){
		if (backwards) {
			power = 1;
		} else {
			power = -1;
		}
		hinge.useMotor = true;
		JointMotor jm = hinge.motor;
		jm.targetVelocity = power * strength;
		hinge.motor = jm;
	}
	public void TurnLeft(){
		if (backwards) {
			power = 1;
		} else {
			power = 1;
		}
		hinge.useMotor = true;
		JointMotor jm = hinge.motor;
		jm.targetVelocity = power * strength;
		hinge.motor = jm;
	}
	public void TurnRight(){
		if (backwards) {
			power = -1;
		} else {
			power = -1;
		}
		hinge.useMotor = true;
		JointMotor jm = hinge.motor;
		jm.targetVelocity = power * strength;
		hinge.motor = jm;
	}
	public void TurnOff(){
		power = 0;
		hinge.useMotor = false;
	}
	public void ToggleOn(){
		if (power > 0) {
			TurnOff ();
		} else 
			TurnOn (); 
	}
	//advanced
	public void TurnOn(float powerPercentage){
		if (backwards) {
			power = -powerPercentage;
		} else {
			power = powerPercentage;
		}
		hinge.useMotor = true;
		JointMotor jm = hinge.motor;
		jm.targetVelocity = power * strength;
		hinge.motor = jm;

	}
	public void TurnOnFor(float seconds, float powerPercentage = 1){
		power = powerPercentage;
		hinge.useMotor = false;
		Invoke ("TurnOff", seconds);
	}

	void Update(){
		if (power > 0) {
			
		}
	}
}
