using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class LeaderBoardController : MonoBehaviour
{
	void showErrorDialog ()
	{
		print("showErrorDialog");

	}

	void showData (LeaderBoardAPI.LeaderBoardResponse response)
	{
		print ("showData");
		print (response.top);

		int i = 0; 
		foreach(LeaderBoardAPI.LeaderBoardResponse.Score score in response.top){

			GameObject user = GameObject.Find ("Score " + "(" + i +")");
			UnityEngine.UI.Text idValue = user.transform.Find ("title_id").GetComponent<UnityEngine.UI.Text>();
			UnityEngine.UI.Text nameValue = user.transform.Find ("title_name").GetComponent<UnityEngine.UI.Text>();
			UnityEngine.UI.Text scoreValue = user.transform.Find ("title_score").GetComponent<UnityEngine.UI.Text>();

			idValue.text = (i+1).ToString();
			nameValue.text = score.email;
			scoreValue.text = score.score.ToString();
			i++;
		}


	}

	// Use this for initialization
	void Start ()
	{
		print ("Start");
		StartCoroutine(LeaderBoardAPI.getLeaders((res) => {
			showData(res);
			hideProgress();

		}, (err) => {
			hideProgress();
			showErrorDialog();
		}));
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("MainMenu");
		}
	}
		
	public void showProgress()
	{
//		progressPanel.SetActive(true);
	}

	public void hideProgress()
	{
//		progressPanel.SetActive(false);
	}

}
