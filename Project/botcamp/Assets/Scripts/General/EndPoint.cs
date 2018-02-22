using UnityEngine;
using System.Collections;

public class EndPoint : MonoBehaviour {
	LevelManager levelManager;

	public GameObject[] balls = new GameObject[0];
	public float maxDist = 10f;

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == "Wheel" && ballsInside()) 
		{
			if (!IsInvoking("foo"))
				Invoke ("foo", 1);
		}
	}

	void Update(){

	}

	bool ballsInside(){
		foreach(GameObject o in balls){
			if (balls == null)
				continue;
			float dist = Vector3.Distance(o.transform.position, transform.position);
			Debug.Log (dist);
			if (dist > maxDist) return false;
		}
		return true;
	}

	void foo()
	{
		Application.LoadLevel ("Scene Selection");

	}
}
