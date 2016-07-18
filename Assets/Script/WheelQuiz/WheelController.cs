﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 

public class WheelController : MonoBehaviour
{
	private float angleVelocity = 0;

	private float startTouchY = -100;
	private Rigidbody2D wheelRigidBody;
	public Text label;

	private bool startedRotation = false;
	private float timeLeft = 0;


	// Use this for initialization
	void Start ()
	{
		wheelRigidBody = gameObject.GetComponent<Rigidbody2D> () as Rigidbody2D;
//		Debug.Log ("rotation " + wheelRigidBody.rotation);
	}

	private void rotateWheelBy (float rotationAngle)
	{
		wheelRigidBody.MoveRotation (
			wheelRigidBody.rotation + rotationAngle / Mathf.PI);
	}

	private string getCategoryByAngle (float gradus)
	{
		string result = "";
		int angle = (int) gradus;
		Debug.Log ("angle " + angle);

		if (angle > 30 && angle <= 90) {
			result = "House";
		} else if (angle > 90 && angle <= 150) {
			result = "Comb";
		} else if (angle > 150 && angle <= 210) {
			result = "Food";
		} else if (angle > 210 && angle <= 270) {
			result = "Meat";
		} else if (angle > 270 && angle <= 330) {
			result = "Animals";
		} else if (angle > 330 || angle <= 30) {
			result = "Angry";
		}

		Debug.Log ("Category " + result);

		label.text = result;

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
		if (timeLeft > 0) {
			timeLeft -= Time.deltaTime;
		} else if (startedRotation) {
			angleVelocity = 0f;
			startedRotation = false;

			float degrees = toDegrees (wheelRigidBody.rotation);
			getCategoryByAngle (degrees);
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
}
