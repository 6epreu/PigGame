using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {


	/// <summary>
	/// Button Script
	/// Add this script to the buttons and it will determine
	/// the button being clicked and execute the required code.
	/// </summary>


	// Color for button clicked state
	public Color colorPushed  = new Color(0.8f,0.8f,0.8f);   
	private Color originalColor;

	// Sound and Music buttons variable
	private GameObject btnSoundOn;
	private GameObject btnSoundOff;
	private GameObject btnMusicOn;
	private GameObject btnMusicOff;

	// Use this for initialization
	void Start () {
		// Get original color of the button
		originalColor = gameObject.GetComponent<Renderer>().material.color;

		// If the screen is menu screen
		// we check if the music and sound are enabled
		// and show the button states accordingly
		if(Application.loadedLevel == 0){
			btnSoundOn = GameObject.Find("btn-sOn");
			btnSoundOff = GameObject.Find("btn-sOff");
			btnMusicOn = GameObject.Find("btn-mOn");
			btnMusicOff = GameObject.Find("btn-mOff");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}  

	void OnMouseDown() {
		// When tapped, change button color
		gameObject.GetComponent<Renderer>().material.color= colorPushed;
	}

	void OnMouseExit() {
		// When released, show original color
		gameObject.GetComponent<Renderer>().material.color= originalColor;
	}

	// When tapped as button, do the following function
	void OnMouseUpAsButton() {       
		if(gameObject.name == "btn-play" || gameObject.name == "btn-replay")
		{
			Application.LoadLevel(Levels.Modes);
		}else if(gameObject.name == "btn-quit")
		{
			Application.Quit();
		}else if(gameObject.name == "btn-modes-normal")
		{
			Application.LoadLevel(Levels.NormalMode);
		}else if(gameObject.name == "btn-modes-hostage")
		{
			Application.LoadLevel(Levels.HostageMode);
		}else if(gameObject.name == "btn-modes-timer")
		{
			Application.LoadLevel(Levels.TimerMode);
		}
	}

}
