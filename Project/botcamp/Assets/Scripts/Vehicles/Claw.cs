using UnityEngine;
using System.Collections;

public class Claw : MonoBehaviour {

	public bool open = true;

	public float max = 30f;
	public float min = -30f;

	HingeJoint[] hjs;
	// Use this for initialization
	public void assignBody(Rigidbody rb){
		if (rb != null) {
			foreach (HingeJoint j in hjs) {
				j.connectedBody = rb;
			}
		}
	}

	void Start () {
		hjs = GetComponentsInChildren<HingeJoint> ();
	}
	
	public void toggle(){
		foreach (HingeJoint hj in hjs) {
			JointMotor newMotor = hj.motor;
			float newVel = newMotor.targetVelocity * -1;
			hj.motor = newMotor;
		}
	}
}
