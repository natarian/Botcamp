using UnityEngine;
using System.Collections;

public class TimerHit : MonoBehaviour {

	void OnCollisionEnter(Collision col)
	{
		Debug.Log ("hit");
		if (col.gameObject.name != "Ground" && col.gameObject.name != "West Wall" && CountDownTimer.start &&
			col.gameObject.name != "East Wall" && col.gameObject.name != "North Wall" && col.gameObject.name != "South Wall") 
		{
			CountDownTimer.timeRemaining--;
		}
	}
}
