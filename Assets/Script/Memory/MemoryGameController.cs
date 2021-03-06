﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class MemoryGameController : MonoBehaviour
{
	private GameObject[] pigs;
	private Text score;
	private GameObject fail1;
	private GameObject fail2;
	private GameObject fail3;
	private GameObject descriptionHolder;
	private Text description;
	private UnityEngine.UI.Button continueBtn;


	// game state
	private HashSet<string> selectedPigs;
	private int failCount = 0;
	private int state;
	private int level = 1;
	private float scoreValue = 0;

	// utilities
	private float timer;


	// remember part vars
	private float REMEMBER_DELAY = 5f;

	// on done
	private float NEXT_LEVEL_TIMEOUT = 4f;

	const int WELCOME_PART = 1;
	const int REMEMBER_PART = 2;
	const int PRE_GAME_PART = 3;
	const int GAME_PART = 4;
	const int GAME_OVER_PART = 5;

	const float WINNING_SCORE_POINTS = 50f;

	void initUI ()
	{
		print ("MEMORY: " + "initUI");
		pigs = GameObject.FindGameObjectsWithTag ("pigs");
		print ("MEMORY: 1");
		score = GameObject.Find ("score").GetComponent<Text> ();
		print ("MEMORY: 2");
		fail1 = GameObject.Find ("failure1");
		print ("MEMORY: 3");
		fail2 = GameObject.Find ("failure2");
		print ("MEMORY: 4");
		descriptionHolder = GameObject.Find ("descriptionHolder");
		print ("MEMORY: 5");
		fail3 = GameObject.Find ("failure3");
		print ("MEMORY: 6");
		description = GameObject.Find ("description").GetComponent<Text> ();
		print ("MEMORY: 7");
		continueBtn = GameObject.Find ("continue").GetComponent<UnityEngine.UI.Button> ();
		print ("MEMORY: 8");
		print ("MEMORY: " + "initUI done");
	}

	void Start ()
	{
		print ("MEMORY: " + "Start");
		initUI ();
		state = WELCOME_PART;

		score.text = "Score: " + AppGlobal.totalScore;
		scoreValue = AppGlobal.totalScore;
		showIntro ();
		failCount = 0;

		Screen.orientation = ScreenOrientation.LandscapeLeft;

	}

	void showScreenWithSelectedPigs ()
	{
		print ("showScreenWithSelectedPigs");

		UnityEngine.Random.seed = (int)System.DateTime.Now.Ticks;
		int amount = level;
		selectedPigs = selectPigs (amount);
		foreach (GameObject pig in pigs) {
			if (selectedPigs.Contains (pig.name)) {
				pig.SetActive (true);
			} else {
				pig.SetActive (false);
			}
		}
	}

	HashSet<string> selectPigs (int amount)
	{
		print ("selectPigs");

		HashSet<string> selectedPigs = new HashSet<string> ();

		while (amount > 0) {
			int id = (int)UnityEngine.Random.Range (1, pigs.Length + 1);
			GameObject pig = pigs [id - 1];
			if (selectedPigs.Contains (pig.name))
				continue;
			selectedPigs.Add (pig.name);
			amount--;
		}

		foreach (string pig in selectedPigs)
			print ("selected pig " + pig);

		return selectedPigs;
	}

	void hidePigs ()
	{
		foreach (GameObject pig in pigs)
			pig.SetActive (false);
	}

	void hideFailures ()
	{
		fail1.SetActive (false);
		fail2.SetActive (false);
		fail3.SetActive (false);
		failCount = 0;
	}

	void showRemember ()
	{
		print ("showRemember");

		showScreenWithSelectedPigs ();
		timer = REMEMBER_DELAY;
		state = REMEMBER_PART;
	}

	void showIntro ()
	{
		hidePigs ();
		hideFailures ();
		description.text = "Onthoud op welke plek de varkentjes op de boerderij zijn. Elke level verschijnen er meer varkentjes. Hoe meer varkentjes goed, hoe meer punten.";
		descriptionHolder.SetActive (true);
		continueBtn.gameObject.SetActive (true);
        
		continueBtn.onClick.AddListener (() => {
			continueBtn.onClick.RemoveAllListeners ();
			descriptionHolder.SetActive (false);
			continueBtn.gameObject.SetActive (false);
			showRemember ();
		});
	}


	void showGameOver ()
	{

		hidePigs ();
		description.text = "GAME OVER";
		descriptionHolder.SetActive (true);

		state = GAME_OVER_PART;
		timer = 2f;

		AppGlobal.totalScore = scoreValue;
	}

	void addFail ()
	{
		failCount++;

		switch (failCount) {
		case 3:
			fail3.SetActive (true);
			showGameOver ();
			break;
		case 2:
			fail2.SetActive (true);
			break;
		case 1:
			fail1.SetActive (true);
			break;
		}
	}

	void showPigs ()
	{
		foreach (GameObject pig in pigs) {
			pig.SetActive (true);
			pig.GetComponent<SpriteRenderer> ().color = Color.white;
		}
	}

	private int trueClicks;

	void showGame ()
	{
		showPigs ();
		trueClicks = 0;
		timer = 1f;
		state = GAME_PART;
	}

	void showPreGame ()
	{
		hidePigs ();
		hideFailures ();
		description.text = "Remember all pigs? Click on it";
		descriptionHolder.SetActive (true);
		continueBtn.gameObject.SetActive (true);

		continueBtn.onClick.AddListener (() => {
			continueBtn.onClick.RemoveAllListeners ();
			descriptionHolder.SetActive (false);
			continueBtn.gameObject.SetActive (false);
			showGame ();
		});
	}


	void showNextLevel ()
	{
		if (level == 10) {
			showGameOver ();
			return;
		}

		timer = NEXT_LEVEL_TIMEOUT;
		scoreValue += WINNING_SCORE_POINTS;
		score.text = "Score: " + scoreValue;
		clickedPigs.Clear ();
		trueClicks = 0;
		level++;
		showPigs ();
		showIntro ();
		description.text = String.Format ("Next level. Remember {0} pigs", level);
	}

	HashSet<string> clickedPigs = new HashSet<string> ();

	void processClicks ()
	{
		if (Input.GetMouseButtonUp (0)) {
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit.collider != null) {
				SpriteRenderer sr = hit.collider.gameObject.GetComponent<SpriteRenderer> ();
				print ("hit " + hit.collider.gameObject.name);
				foreach (string name in selectedPigs)
					print ("selected pig: " + name);

				string name2 = hit.collider.gameObject.name;
				if (selectedPigs.Contains (name2)) {
					if (clickedPigs.Contains (name2))
						return;
					clickedPigs.Add (name2);
					sr.color = Color.green;
					trueClicks++;
					if (trueClicks >= level)
						showNextLevel ();

					
				} else {
					sr.color = Color.red;
					addFail ();
				}
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
			SceneManager.LoadScene ("MainMenu");

		if (timer > 0) {
			timer -= Time.deltaTime;
			return;
		}

		switch (state) {
		case WELCOME_PART:
			break;

		case REMEMBER_PART:	
			showPreGame ();
			break;

		case PRE_GAME_PART:
			break;

		case GAME_PART:
			processClicks ();
			break;

		case GAME_OVER_PART:
			if (AppGlobal.isContinious)
				SceneManager.LoadScene ("QuizWheel");
			else
				SceneManager.LoadScene ("MainMenu");
			break;
		}
	}
}
