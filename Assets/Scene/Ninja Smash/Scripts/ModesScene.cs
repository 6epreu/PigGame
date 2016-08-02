using UnityEngine;
using System.Collections;

public class ModesScene : MonoBehaviour {

	// Music variable
	public AudioClip musicClip;
	public static AudioSource musicSource;


	// Use this for initialization
	void Start () {

		// Initialize music
		musicSource = gameObject.AddComponent<AudioSource>();
		musicSource.playOnAwake = false;
		musicSource.rolloffMode = AudioRolloffMode.Linear;
		musicSource.loop = true;
		musicSource.clip = musicClip;
		

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.LoadLevel(Levels.Menu); // goto menu screen if back key is pressed
		}
	}
}
