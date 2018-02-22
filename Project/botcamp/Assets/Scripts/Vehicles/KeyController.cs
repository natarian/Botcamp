using UnityEngine;
using System.Collections;

public class KeyController : MonoBehaviour {

	Robot r;

	public bool ready = false;

	// Use this for initialization
	void Start () {
		r = GetComponent<Robot> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!ready) {
			return;
		}

		if (Input.GetKey (KeyCode.S)) {
			r.goForward ();
		} 
		if (Input.GetKey (KeyCode.W)) {
			r.goBackward ();
		} 
		if (Input.GetKey (KeyCode.D)) {
			r.turnRight ();
		} 
		if (Input.GetKey (KeyCode.A)) {
			r.turnLeft ();
		} 
		if (Input.GetKeyUp (KeyCode.W) || Input.GetKeyUp (KeyCode.S) || Input.GetKeyUp (KeyCode.A) || Input.GetKeyUp (KeyCode.D)) {
			r.stop ();
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log ("claw");
			r.ClawToggle ();
		}
		if (Input.GetKey (KeyCode.E)) {

		}
	}
}
