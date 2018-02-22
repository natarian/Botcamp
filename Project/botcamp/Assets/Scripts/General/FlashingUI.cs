using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlashingUI : MonoBehaviour {

	public Color startCol;
	public Color endCol;

	public Image i;
	public Text t;

	private bool useText = false;
	private bool useImage = false;

	void Start(){
		if (i != null) {
			useImage = true;
		} else if (t != null) {
			useText = true;
		}
	}

	// Update is called once per frame
	void Update () {
		Color c = Color.Lerp (startCol, endCol, 0.5f * Mathf.Sin(Time.deltaTime * 5f) + 0.5f);
		if (useText) {
			t.color = c;
		}
		if (useImage) {
			i.color = c;
		}
	}
}
