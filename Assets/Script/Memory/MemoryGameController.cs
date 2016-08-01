using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MemoryGameController : MonoBehaviour
{
	private GameObject[] pigs;
	private Text score;
	private GameObject fail1;
	private GameObject fail2;
	private GameObject fail3;
	private GameObject descriptionHolder;
	private Text description;
	private Button continueBtn;

	private string state = "";

	private float REMEMBER_DELAY = 5f;
	private float rememberTimeout;


	void showScreenWithSelectedPigs ()
	{
		Random.seed = (int)System.DateTime.Now.Ticks;
		int amount = 10;
		HashSet<GameObject> selectedPigs = selectPigs (amount);
		foreach (GameObject pig in pigs) {
			if (selectedPigs.Contains (pig)) {
				pig.SetActive (true);
			} else {
				pig.SetActive (false);
			}
		}
	}

	HashSet<GameObject> selectPigs (int amount)
	{
		HashSet<GameObject> selectedPigs = new HashSet<GameObject> ();

		while (amount > 0) {
			int id = (int)Random.Range (1, pigs.Length + 1);
			GameObject pig = pigs [id - 1];
			if (selectedPigs.Contains (pig))
				continue;
			selectedPigs.Add (pig);
			amount--;
		}

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
	}

	void showRemember ()
	{
		showScreenWithSelectedPigs ();
		rememberTimeout = REMEMBER_DELAY;
		state = "remember";
	}

	void showIntro ()
	{
		hidePigs ();
		hideFailures ();
		description.text = "Welcome to our cool game.\nRemember pigs on the Screen.\nThen click it all on next Stage.";
		descriptionHolder.SetActive (true);

		continueBtn.onClick.AddListener (() => {
			descriptionHolder.SetActive (false);
			continueBtn.gameObject.SetActive (false);
			showRemember ();
		});
	}

	void initUI ()
	{
		pigs = GameObject.FindGameObjectsWithTag ("pigs");
		score = GameObject.Find ("score").GetComponent<Text> ();
		fail1 = GameObject.Find ("failure1");
		fail2 = GameObject.Find ("failure2");
		descriptionHolder = GameObject.Find ("descriptionHolder");
		fail3 = GameObject.Find ("failure3");
		description = GameObject.Find ("description").GetComponent<Text> ();
		continueBtn = GameObject.Find ("continue").GetComponent<Button> ();
	}

	void Start ()
	{
		initUI ();
		state = "intro";
		score.text = "Score: 0.00";
		showIntro ();
	}

	void showPigs ()
	{
		foreach (GameObject pig in pigs)
			pig.SetActive (true);
	}

	void setPigsClickable ()
	{
//		foreach (GameObject pig in pigs) {
//			SpriteRenderer sr = pig.GetComponent<SpriteRenderer> ();
//			BoxCollider2D collider = pig.GetComponent<BoxCollider2D> ();
//			collider.
//		}
	}

	void showGame ()
	{
		state = "clicking";
		showPigs ();
		setPigsClickable ();
	}

	void showPreGame ()
	{
		hidePigs ();
		hideFailures ();
		description.text = "Remember all pigs? Click on it";
		descriptionHolder.SetActive (true);
		continueBtn.gameObject.SetActive (true);

		continueBtn.onClick.AddListener (() => {
			descriptionHolder.SetActive (false);
			continueBtn.gameObject.SetActive (false);
			showGame ();
		});
		
	}

	// Update is called once per frame
	void Update ()
	{
		switch (state) {
		case "intro":
			break;

		case "remember":
			if (rememberTimeout > 0) {
				rememberTimeout -= Time.deltaTime;
			} else {
				showPreGame ();
			}
			break;

		case "preGame":
			break;

		case "clicking":
			break;

		}
		if (Input.GetMouseButtonUp (0)) {
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit.collider != null) {
				Debug.Log ("Target Position: " + hit.collider.gameObject.transform.position);
				SpriteRenderer sr = hit.collider.gameObject.GetComponent<SpriteRenderer> ();
				sr.color = Color.green;
			}
		}
	
	}
}
