using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlphaFadeInOut : MonoBehaviour {

	public float fadeTime = 1f;
	public bool fadeIn = true;

	private Image img;
	private Text txt;

	void Start () {
		img = GetComponent<Image> ();
		txt = GetComponent<Text> ();
		if (fadeIn) {
			if (img)
				img.CrossFadeAlpha (255, fadeTime, false);
			if (txt)
				txt.CrossFadeAlpha (255, fadeTime, false);
		} else {
			Color fullAlpha = new Color ();
			if (img)
				fullAlpha = img.color;
			if (txt)
				fullAlpha = txt.color;
			fullAlpha.a = 255f;
			if (img) {
				img.color = fullAlpha;
				img.CrossFadeAlpha (0, fadeTime, false);
			} 
			if (txt) {
				txt.color = fullAlpha;
				txt.CrossFadeAlpha (0, fadeTime, false);
			}
		}
		Destroy (gameObject, fadeTime);
	}

}
