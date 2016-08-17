using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NinjaStartHandler : MonoBehaviour {

	private GameObject dialog;

	// Use this for initialization
	void Start () {
		dialog = GameObject.Find ("Dialog");
	}


	public void btnStartClick()
	{
		SceneManager.LoadScene("GameScene");
	}
}
