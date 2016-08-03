using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour {

	/// <summary>
	/// The main gameplay script for the game.
	/// </summary>

	// Ninja objects list
	private GameObject[] ninjas1;
	private GameObject[] ninjas2;
	private GameObject[] ninjas3;
	private GameObject[] ninjas4;

	// Boolean to check if each hole is taken or empty
	private bool[] isHoleTaken;

	// GUI Text Variables
	private GameObject textTime;
	private static GameObject textScore;
	private static GameObject textMisses;

	// Hammer and blam sprite variables
	private static GameObject hammer;
	private static GameObject blam;

	// Variable to store misses and time value
	private int timeNum; // Only used in timer mode
	public static int missesNum;

	// Sound for when hammer hits the ninja
	public AudioClip hitClip;
	private static AudioSource hitSource;

	// Game music clip
	public AudioClip musicClip;
	public static AudioSource musicSource;

	private bool overSceneLoaded;

    bool isGameOver = false;


	// Use this for initialization
	void Start () {

        //textTime.GetComponent<Renderer>().enabled = false;
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        // Get paused screen elements and hide them, we'll show them when game is paused
        GameObject.Find("title-paused").GetComponent<Renderer>().enabled = false;
		GameObject.Find("btn-resume").GetComponent<Renderer>().enabled = false;
		GameObject.Find("btn-home").GetComponent<Renderer>().enabled = false;
		GameObject.Find("bg-paused").GetComponent<Renderer>().enabled = false;
	

		// Initialize music and sound
		hitSource = gameObject.AddComponent<AudioSource>();
		hitSource.playOnAwake = false;
		hitSource.rolloffMode = AudioRolloffMode.Linear;
		hitSource.clip = hitClip;

		musicSource = gameObject.AddComponent<AudioSource>();
		musicSource.playOnAwake = false;
		musicSource.rolloffMode = AudioRolloffMode.Linear;
		musicSource.loop = true;
		musicSource.clip = musicClip;
		

		// Get other screen elements
		textTime = GameObject.Find("TextTime");
        textScore = GameObject.Find("TextScore");
		textMisses = GameObject.Find("TextMisses");

		hammer = GameObject.Find("hammer");
		hammer.GetComponent<SpriteRenderer>().enabled = false;

		blam = GameObject.Find("blam");
		blam.GetComponent<SpriteRenderer>().enabled = false;


		// Initialize integer variables 

		timeNum = 60;
		AppGlobal.scoreNum = 0;
		missesNum = 0;
		isGameOver = false;
		overSceneLoaded = false;
		


		// Initialize boolean variables
		isHoleTaken = new bool[7];
		isHoleTaken[0] = false;
		isHoleTaken[1] = false;
		isHoleTaken[2] = false;
		isHoleTaken[3] = false;
		isHoleTaken[4] = false;
		isHoleTaken[5] = false;
		isHoleTaken[6] = false;


		// Get Ninja gameobjects from screen, we'll show/hide them with code
		// We're creating 7 ninjas of each type at the start, so that we don't have to
		// create it during gameplay

		ninjas1 = new GameObject[7];
		ninjas1[0] = GameObject.Find("n10");
		ninjas1[1] = GameObject.Find("n11");
		ninjas1[2] = GameObject.Find("n12");
		ninjas1[3] = GameObject.Find("n13");
		ninjas1[4] = GameObject.Find("n14");
		ninjas1[5] = GameObject.Find("n15");
		ninjas1[6] = GameObject.Find("n16");

		ninjas2 = new GameObject[7];
		ninjas2[0] = GameObject.Find("n20");
		ninjas2[1] = GameObject.Find("n21");
		ninjas2[2] = GameObject.Find("n22");
		ninjas2[3] = GameObject.Find("n23");
		ninjas2[4] = GameObject.Find("n24");
		ninjas2[5] = GameObject.Find("n25");
		ninjas2[6] = GameObject.Find("n26");

		ninjas3 = new GameObject[7];
		ninjas3[0] = GameObject.Find("n30");
		ninjas3[1] = GameObject.Find("n31");
		ninjas3[2] = GameObject.Find("n32");
		ninjas3[3] = GameObject.Find("n33");
		ninjas3[4] = GameObject.Find("n34");
		ninjas3[5] = GameObject.Find("n35");
		ninjas3[6] = GameObject.Find("n36");

		ninjas4 = new GameObject[7];
		ninjas4[0] = GameObject.Find("n40");
		ninjas4[1] = GameObject.Find("n41");
		ninjas4[2] = GameObject.Find("n42");
		ninjas4[3] = GameObject.Find("n43");
		ninjas4[4] = GameObject.Find("n44");
		ninjas4[5] = GameObject.Find("n45");
		ninjas4[6] = GameObject.Find("n46");

		// Start method to show ninjas in loop
		StartCoroutine(popNinjas());


        // если это заново запушенная игра - включаем режит таймера
        if (AppGlobal.isContinious) {
            StartCoroutine(countDownTimer());
            textTime.SetActive(true);
        } else
            textTime.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        // load main screen
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("MainMenu");
        }
        
        // This is for PC control, when mouse is clicked in ninja, show hammer
        if (Input.GetButtonDown ("Fire1")) {
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = 10;
			Vector3 wp = Camera.main.ScreenToWorldPoint(mousePos);
			Vector2 touchPos = new Vector2(wp.x + 0.3f, wp.y);
//			Debug.Log("X= "+ wp.x + "   Y= "+ wp.y);
			showHammer(touchPos);
		}

		// This is for Mobile control, when tapped in ninja, show hammer
		if (Input.touchCount == 1)
		{
			Touch touch = Input.touches[0];
			if(touch.phase == TouchPhase.Began){
				Vector3 mousePos = touch.position; // Input.GetTouch(0).position;
				mousePos.z = 10;
				Vector3 wp = Camera.main.ScreenToWorldPoint(mousePos);
				Vector2 touchPos = new Vector2(wp.x + 0.3f, wp.y);
				showHammer(touchPos);
			}
		}


		// If game is over but gameover screen is not shown, show gameover screen
		if(isGameOver && !overSceneLoaded){
			overSceneLoaded = true;

            if (AppGlobal.isContinious)
            {
                AppGlobal.totalScore += AppGlobal.scoreNum;
                SceneManager.LoadScene("Level1");
            } else
            {
                SceneManager.LoadScene("MainMenu");
            }
		}
	}

	// Method to pop Ninjas every few seconds.
	IEnumerator popNinjas() {
		while (!isGameOver){
			yield return new WaitForSeconds(Random.Range(0.2f, 0.5f));
			{
				// We get a random hole number,
				// If that hole is empty, we spawn a ninja
				// Else we check for another random hole

				int randomHole;
				while(true){
					int rand = Random.Range(1, 8);
					if(!isHoleTaken[rand-1]){
						randomHole= rand-1;
						break;
					}
				}

				int randomNinjaType = Random.Range(1, 5);
				Ninja ninjaScript;
				if(randomNinjaType == 1){
					ninjaScript = ninjas1[randomHole].GetComponent<Ninja>();
				}else if(randomNinjaType == 2){
					ninjaScript = ninjas2[randomHole].GetComponent<Ninja>();
				}else if(randomNinjaType == 3){
					ninjaScript = ninjas3[randomHole].GetComponent<Ninja>();
				}else{
					ninjaScript = ninjas4[randomHole].GetComponent<Ninja>();
				}
				ninjaScript.pop();

				isHoleTaken[randomHole] = true;

				// After we have spawned the ninja out of hole
				// We create another delay and make the hole empty again with the code below.
				StartCoroutine(changeHoleState(randomHole));
			}
		}
	}

	// Change the state of hole to empty after few seconds of spawn
	IEnumerator changeHoleState(int holeNum){
		yield return new WaitForSeconds(1.5f);
		isHoleTaken[holeNum] = false;
	}

	// Countdown timer, this is used in Timer Mode
	IEnumerator countDownTimer(){
		while(timeNum > 0){
			yield return new WaitForSeconds(1);
			timeNum -= 1;
			textTime.GetComponent<GUIText>().text = timeNum + " secs";
			if(timeNum == 0){
				isGameOver = true;
                AppGlobal.totalScore += AppGlobal.scoreNum;
                SceneManager.LoadScene("Level1");
            }
		}
	}

	// Method to update game score, this is called when hammer hits the ninjas
	public static void updateScore() {
		AppGlobal.scoreNum += 1;
		textScore.GetComponent<GUIText>().text = "Score: "+ AppGlobal.scoreNum;
	}

	// Method to update misses
	public static void updateMisses(){
		missesNum += 1;
		//textMisses.GetComponent<GUIText>().text = "Misses: "+ missesNum;
		//if(missesNum == 200){
		//	MenuScene.isGameOver = true;
		//}
	}

	// Method to show hammer
	public void showHammer(Vector2 pos){
		hammer.transform.position = pos;
		hammer.GetComponent<SpriteRenderer>().enabled = true;
		hammer.GetComponent<Animator>().SetTrigger("fire");
	}

	// Method to show the BLAM message when hammer hits the ninja
	public static void showBlam(Vector2 pos){
		blam.transform.position = pos;
		blam.GetComponent<SpriteRenderer>().enabled = true;
        hitSource.Play();
		StaticCoroutine.DoCoroutine(hideBlam());
	}

	// Method to hide the BLAM message after few seconds delay
	private static IEnumerator hideBlam(){
		yield return new WaitForSeconds(0.6f);
		blam.GetComponent<SpriteRenderer>().enabled = false;
	}
	
}



