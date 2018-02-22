using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
	public AudioClip splashClip;
	public AudioClip menuMusic1;
	public AudioClip gameMusic1;

	private AudioSource audioSource;

	public static MusicManager mm;

	void OnAwake ()
	{
		//singleton
		DontDestroyOnLoad (this.gameObject);
	}

	void Start ()
	{
		if (mm == null) {
			mm = this;
		}
		if (mm != null && mm != this) {
			Destroy (gameObject);
		}
		//singleton
		DontDestroyOnLoad (this.gameObject);

		//load the audioSource
		audioSource = GetComponent<AudioSource> ();
		//set audio volume

		loadLevel (Application.loadedLevel);
	}

	void OnLevelWasLoaded (int level)
	{
		//play proper background audio clip
		Debug.Log ("Level Loadedd int: " + level);

		loadLevel (level);
	}

	void loadLevel (int level)
	{
		if (audioSource) {
			bool changed = false;
			switch (Application.loadedLevel) {
			case 0:
				changed = true;
				audioSource.clip = splashClip;
				break;
			case 1:
				changed = musicWasChanged (menuMusic1);
				audioSource.clip = gameMusic1;
				break;
			case 2:
				changed = musicWasChanged (gameMusic1);
				audioSource.clip = gameMusic1;
				break;
			
			default:
				changed = musicWasChanged (gameMusic1);
				audioSource.clip = gameMusic1;
				break;
			}

			//Play if changed
			if (changed)
				audioSource.Play ();
		} else {
			Debug.LogWarning ("Missing Audiosource");
		}
	}
	bool musicWasChanged(AudioClip clip){
		if (audioSource == null || audioSource.clip == null) {
			return true;
		}
		return !audioSource.clip.Equals (clip);
	}
}
