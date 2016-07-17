using UnityEngine;
using System.Collections;

public class WheelController : MonoBehaviour
{

	float[] mData;
	public float delay = 0.1f;
	public Material mainMaterial;
	public Material[] materials;
	public int segments;
	private int randomBuffer;
	private float angle = 0;
	private float angleVelocity = 0;
	WheelMesh wheelMesh;

	Rigidbody2D wheelRigidBody;
	// Use this for initialization
	void Start ()
	{
		wheelRigidBody = gameObject.GetComponent<Rigidbody2D> () as Rigidbody2D;

		randomBuffer = Random.seed;
		Random.seed = 10;

		materials = new Material[segments];
		for (int i = 0; i < segments; i++) {
			materials [i] = new Material (mainMaterial);
			materials [i].color = GenerateRandomColor ();
			Debug.Log (materials [i].color);
		}

		wheelMesh = gameObject.AddComponent<WheelMesh> () as WheelMesh;	
		wheelMesh.Init (mData, segments, segments, materials, delay);
		mData = GenerateWheel (segments);
		wheelMesh.Draw (mData);
		Random.seed = randomBuffer;
	}

	Color GenerateRandomColor ()
	{
		return new Color32 (
			(byte)Random.Range (0, 256),
			(byte)Random.Range (0, 256),
			(byte)Random.Range (0, 256), (byte)255);
	}

	float[] GenerateWheel (int segments)
	{
		float[] parts = new float[segments];

		for (int i = 0; i < segments; i++) {
			parts [i] = 1;
		}
		return parts;
	}

	private float angleVelocityDelta = 20f;
	private float startTouchY = -100;

	void rotateWheelBy (float rotationAngle)
	{
		wheelRigidBody.MoveRotation (rotationAngle / Mathf.PI);
	}

	private bool startedRotation = false;
	private float timeLeft = 0;


	void startRotationAnimation ()
	{
		startedRotation = true;
		angleVelocity = 50;
		timeLeft = 2L;
	}

	// Update is called once per frame
	void Update ()
	{
		Debug.Log ("timeLeft " + timeLeft);
		if (timeLeft > 0) {
			timeLeft -= Time.deltaTime;
		} else if (startedRotation) {
			angleVelocity = 0f;
			startedRotation = false;
		}

		float rotation = wheelRigidBody.rotation + angleVelocity;
		wheelRigidBody.MoveRotation (rotation);

		if (!startedRotation && Input.touchCount > 0) {
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				startTouchY = Input.GetTouch (0).position.y;
				Debug.Log ("startTouchY " + startTouchY);
			} else if (Input.GetTouch (0).phase == TouchPhase.Moved) {
				float nextTouchY = Input.GetTouch (0).position.y;
				rotateWheelBy (startTouchY - nextTouchY);
			} else if (Input.GetTouch (0).phase == TouchPhase.Ended) {
				startRotationAnimation ();
			}
		}
	}
}
