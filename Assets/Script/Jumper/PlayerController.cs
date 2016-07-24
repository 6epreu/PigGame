using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
    
    private Rigidbody2D mRigidBody;
    private Animator mAnimator;
    public float force = 500;
    private float playerHurtTime = -1;
    private BoxCollider2D mCollider;
    public Text scoreText;
    public int jumpLeft = 1;
    public float bonus = 0;


    // Use this for initialization
    void Start () {
        mRigidBody = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
        mCollider = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        
        if (playerHurtTime == -1)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && jumpLeft > 0)
            {
                if (mRigidBody.velocity.y < 0)
                {
                    mRigidBody.velocity = Vector2.zero;
                }
                    

                mRigidBody.AddForce(Vector2.up * force);
                jumpLeft--;
                Debug.Log("Jump Left = " + jumpLeft);
            }

            mAnimator.SetFloat("vVelocity", mRigidBody.velocity.y);
            scoreText.text = (Time.time + bonus).ToString("0.0");
        }
        else if ( Time.time > playerHurtTime + 2 ) 
        {
            scoreText.text = "0.0";
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    void OnCollisionEnter2D( Collision2D collision )
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("hurt");
            
            mAnimator.SetBool("bHurt", true);
            playerHurtTime = Time.time;

            // остаенавливаем существующий кактусы!
            foreach (MoveLeft ml in FindObjectsOfType<MoveLeft>())
            {
                ml.enabled = false;
            }

            // остаенавливаем фабрику кактусов
            FindObjectOfType<EnemySpawner>().enabled = false;
            FindObjectOfType<MountainSpawner>().enabled = false;

            mRigidBody.velocity = Vector2.zero;
            mRigidBody.AddForce(Vector2.up * force);
            mCollider.enabled = false;
            scoreText.text = "0.0";
        }

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            jumpLeft = 2;
            Debug.Log("Jump Left ++ " + jumpLeft);
        }

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Gold"))
        {
            Destroy(collision.collider.gameObject);
            bonus += 50f;
        }
    }
}

