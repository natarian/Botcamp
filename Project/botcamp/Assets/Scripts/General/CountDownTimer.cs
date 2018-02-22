using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountDownTimer : MonoBehaviour {
	public static float timeRemaining = 60.0f;
	public static bool start;
	public Text t;

	void Start(){
		timeRemaining = 60f;
		start = false;
	}

	void Update () {

		if(start)
		{
			timeRemaining -= Time.deltaTime;
			t.text = "Time Remaining : " + (int)timeRemaining;

			if (timeRemaining < 0)
				loadLevels ();
		}
	}

	void OnLevelWasLoaded(int l){
		timeRemaining = 60.0f;
		start = false;
	}

	public void loadLevels()
	{
		Application.LoadLevel ("Scene Selection");

	}

	public void Activate()
	{
		start = true;

	}


}
