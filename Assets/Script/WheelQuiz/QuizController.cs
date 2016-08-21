﻿using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class QuizController : MonoBehaviour
{
	
	const float WIN_SCORE = 50f;
	const float delay = 2f;

	private float timeout;
	private Boolean IsFinishing = false;
	private Boolean IsLoadNextScene = false;

	public UnityEngine.UI.Button answerButton1;
	public UnityEngine.UI.Button answerButton2;
	public UnityEngine.UI.Button answerButton3;
	public UnityEngine.UI.Button answerButton4;

	public Text question;
	public Text title;

	public Image groupImage;

	int correctAnswer;

	private List<E> ShuffleList<E> (List<E> inputList)
	{
		List<E> newInputList = new List<E> (inputList);
		List<E> randomList = new List<E> ();

		System.Random r = new System.Random ();
		int randomIndex = 0;
		while (newInputList.Count > 0) {
			randomIndex = r.Next (0, newInputList.Count); //Choose a random object in the list
			randomList.Add (newInputList [randomIndex]); //add it to the new, random list
			newInputList.RemoveAt (randomIndex); //remove to avoid duplicates
		}

		return randomList; //return the new random list
	}

	int findTrueAnswer (String[] answers, string correctAnswer)
	{
		int result = 0;

		for (int i = 0; i < answers.Length; i++) {
			if (correctAnswer == answers [i]) {
				result = i;
				break;
			}
		}

		return result;
	}

	Sprite findImageByGroup (string group)
	{
		Dictionary<String, String> map = new Dictionary<String, String> ();
		map.Add ("Voeding", "foodGroup");
		map.Add ("Huisvesting", "houseGroup");
		map.Add ("Varken zelf", "animalGroup");
		map.Add ("Emoties en omgang", "angryGroup");
		map.Add ("Varkensproducten", "meatGroup");
//		map.Add ("Voeding", "foodGroup");
		// last...

		Sprite result = (Sprite)Resources.Load<Sprite> (map [group]);
		return result;
	}

	void DisableButtons ()
	{
		answerButton1.enabled = false;
		answerButton2.enabled = false;
		answerButton3.enabled = false;
		answerButton4.enabled = false;
	}

	// Use this for initialization
	void Start ()
	{
		Quiz quiz = QuizApp.getInstance ().nextQuiz ();
		String[] answers = ShuffleList (quiz.Answers).ToArray ();
		String correctAnswerStr = quiz.Answers.ToArray () [0];
		this.correctAnswer = findTrueAnswer (answers, correctAnswerStr);
		this.title.text = QuizApp.Group;
		this.question.text = quiz.Question;
		this.groupImage.sprite = findImageByGroup (QuizApp.Group);

		answerButton1 = GameObject.Find ("AnswerButton1").GetComponent<UnityEngine.UI.Button> ();
		answerButton2 = GameObject.Find ("AnswerButton2").GetComponent<UnityEngine.UI.Button> ();
		answerButton3 = GameObject.Find ("AnswerButton3").GetComponent<UnityEngine.UI.Button> ();
		answerButton4 = GameObject.Find ("AnswerButton4").GetComponent<UnityEngine.UI.Button> ();

		answerButton1.GetComponentInChildren<Text> ().text = answers [3];
		answerButton2.GetComponentInChildren<Text> ().text = answers [2];
		answerButton3.GetComponentInChildren<Text> ().text = answers [1];
		answerButton4.GetComponentInChildren<Text> ().text = answers [0];

		Debug.Log ("correct answer =" + correctAnswer);
	}

	void showDialog (Boolean isWinner)
	{
		GameObject dialog = GameObject.Find ("Dialog");
		Text answer = GameObject.Find ("Answer").GetComponent<Text> ();
		if (isWinner) {
			answer.text = "Your answer is correct!\nEarned 50 points";
		} else {
			answer.text = "Your answer is incorrect!";
		}

		UnityEngine.UI.Button button = GameObject.Find ("Button").GetComponent<UnityEngine.UI.Button> ();
		button.onClick.AddListener(() => {
			if (QuizApp.getInstance ().isGameOver ()) {
				QuizApp.getInstance ().NewGame ();
				if (AppGlobal.isContinious) {
					StartCoroutine(LeaderBoardAPI.addScore((int)QuizApp.getInstance().Score, 
						(s) => SceneManager.LoadScene ("Leaderboards")));
				} else {
					SceneManager.LoadScene ("MainMenu");
				}
			}

			if (IsFinishing) {
				QuizApp.getInstance ().AddGame ();
				SceneManager.LoadScene ("QuizWheel");
			}
		});
		dialog.SetActive (true);
	}

	public void SetAnswer (int number)
	{
		Debug.Log ("your answer = " + number);
		Boolean result = number == correctAnswer;
		Debug.Log (result ? "YOU WIN" : "YOU LOSE");	
			
		if (result) {
			QuizApp.getInstance ().addScore (WIN_SCORE);

		}
		showDialog (result);


		QuizApp.getInstance ().AddGame ();
		timeout = delay;
		IsFinishing = true;
	}
		
	// Update is called once per frame
	void Update ()
	{
		if (timeout > 0)
			timeout -= Time.deltaTime;



		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene ("QuizWheel");
		}
	}
}
