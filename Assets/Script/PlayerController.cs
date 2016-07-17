using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D mRigidBody;
    public float playerUpForce = 500f;
    private Animator mAnimator;
    
    // Use this for initialization
	void Start () {
        mRigidBody = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();

        

    }
	
	// Update is called once per frame
	void Update () {
        

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            mRigidBody.AddForce(transform.up * playerUpForce);
        }
        
        mAnimator.SetFloat("vVelocity", Mathf.Abs(mRigidBody.velocity.y));
    }
}
