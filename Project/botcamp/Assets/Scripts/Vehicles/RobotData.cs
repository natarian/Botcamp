using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RobotData {

	public string botName;
	public string chassis;
	public string wheels;
	public string arm; //add multiple
	public string sensor; //add multiple 

	public RobotData(){
		botName = "";
		chassis = "";
		wheels = "";
		arm = "";
		sensor = "";
	}
}
