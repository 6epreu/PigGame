using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class WheelController : MonoBehaviour
{

	public Text score;
	public GameObject messageWindow;
	public UnityEngine.UI.Button continueButton;

	private float angleVelocity = 0;
	private float startTouchY = -100;
	private Rigidbody2D wheelRigidBody;
	private bool startedRotation = false;
	private float timeLeft = 0;

	public void hideMessageWindow(){
		messageWindow.SetActive (false);
	}

	void Start ()
	{
		Screen.orientation = ScreenOrientation.Portrait;
		
		messageWindow.SetActive (false);
		continueButton.onClick.AddListener (() => {
			print("continue");
			messageWindow.SetActive (false);
		});

//		if (!QuizApp.getInstance ().isFirstGame ())
//			messageWindow.SetActive (false);
//
//		wheelRigidBody = gameObject.GetComponent<Rigidbody2D> () as Rigidbody2D;
//		score.text = "Score: " + QuizApp.getInstance ().Score;
	}

	private void rotateWheelBy (float rotationAngle)
	{
		wheelRigidBody.MoveRotation (
			wheelRigidBody.rotation + rotationAngle / Mathf.PI);
	}

	private string getCategoryByAngle (float gradus)
	{
		string result = "";
		int angle = (int)gradus;
		Debug.Log ("angle " + angle);

		if (angle > 30 && angle <= 90) {
			result = "Huisvesting";
		} else if (angle > 90 && angle <= 150) {
			result = "Huisvesting"; // Verzorging
		} else if (angle > 150 && angle <= 210) {
			result = "Voeding";
		} else if (angle > 210 && angle <= 270) {
			result = "Varkensproducten";
		} else if (angle > 270 && angle <= 330) {
			result = "Varken zelf";
		} else if (angle > 330 || angle <= 30) {
			result = "Emoties en omgang";
		}

		Debug.Log ("Category " + result);

		return result;
	}

	private void startRotationAnimation ()
	{
		startedRotation = true;
		angleVelocity = 50;
		timeLeft = 2.0f;
	}

	float toDegrees (float rotation)
	{

		float degrees = wheelRigidBody.rotation;

		while (degrees > 360)
			degrees -= 360;
		
		return degrees;
	}

	// Update is called once per frame
	void Update ()
	{
//		if (Input.GetKeyDown (KeyCode.Escape))
//			SceneManager.LoadScene ("MainMenu");
		/*
		if (!messageWindow.activeSelf) {
			if (timeLeft > 0) {
				timeLeft -= Time.deltaTime;
			} else if (startedRotation) {
				angleVelocity = 0f;
				startedRotation = false;

				float degrees = toDegrees (wheelRigidBody.rotation);
				string group = getCategoryByAngle (degrees);
				QuizApp.Group = group;
				SceneManager.LoadScene ("QuizQuestion");
			}
		
			float rotation = wheelRigidBody.rotation + angleVelocity;
			wheelRigidBody.MoveRotation (rotation);

			if (!startedRotation && Input.touchCount > 0) {
				if (Input.GetTouch (0).phase == TouchPhase.Began) {
					startTouchY = Input.GetTouch (0).position.y;
				} else if (Input.GetTouch (0).phase == TouchPhase.Moved) {
					float nextTouchY = Input.GetTouch (0).position.y;
					rotateWheelBy (startTouchY - nextTouchY);
				} else if (Input.GetTouch (0).phase == TouchPhase.Ended) {
					startRotationAnimation ();
				}
			}
		}
*/
	}
}
