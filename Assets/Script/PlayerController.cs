using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    
    private Rigidbody2D mRigidBody;
    private Animator mAnimator;
    public float force = 500;
    
    
    // Use this for initialization
	void Start () {
        mRigidBody = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            mRigidBody.AddForce(Vector2.up * force);
        }

        mAnimator.SetFloat("vVelocity", mRigidBody.velocity.y);
    }

    void OnCollisionEnter2D( Collision2D collision )
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("asdf");
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
