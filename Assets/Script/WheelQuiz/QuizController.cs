using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class QuizController : MonoBehaviour
{

	public Button answerButton1;
	public Button answerButton2;
	public Button answerButton3;
	public Button answerButton4;

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

	// Use this for initialization
	void Start ()
	{
		Quiz quiz = QuizApp.getInstance ().nextQuiz ();
		String[] answers = ShuffleList (quiz.Answers).ToArray ();
		String correctAnswerStr = quiz.Answers.ToArray () [0];
		this.correctAnswer = findTrueAnswer (answers, correctAnswerStr);
		this.title.text = QuizApp.Group;

		answerButton1.onClick.AddListener (() => {
			SetAnswer (1);
		}); 
		answerButton2.onClick.AddListener (() => {
			SetAnswer (2);
		}); 
		answerButton3.onClick.AddListener (() => {
			SetAnswer (3);
		}); 
		answerButton4.onClick.AddListener (() => {
			SetAnswer (4);
		}); 

		answerButton1.GetComponentInChildren<Text> ().text = answers [0];
		answerButton2.GetComponentInChildren<Text> ().text = answers [1];
		answerButton3.GetComponentInChildren<Text> ().text = answers [2];
		answerButton4.GetComponentInChildren<Text> ().text = answers [3];
	}

	void SetAnswer (int number)
	{
		Debug.Log (number);
		Debug.Log (correctAnswer);
		Debug.Log (number == correctAnswer ? "YOU WIN" : "YOU LOSE");	
	}
		
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene("QuizWheel");
		}
	}
}
