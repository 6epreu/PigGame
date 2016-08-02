using UnityEngine;
using System.Collections;

public class Ninja : MonoBehaviour {

	// Animator variable for ninja
	private Animator animator;

	// Boolean to check if ninja can be smashed
	private bool canSmash;
	
	// Use this for initialization
	void Start () {
	
		// Get animator from gameobject
		animator = GetComponent<Animator>();

		// Ninjas are hiding at start so they cannot be smashed
		canSmash = false;

	}
	
	// Update is called once per frame
	void Update () {
		if(animator)
		{
			//get the current state
			AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

			// If ninja is disappearing into hole, change its pop state to false
			if(stateInfo.nameHash == Animator.StringToHash("Base Layer.disappear")){
				animator.SetBool("pop", false );
			}
		}	

		// Mobile touched input
		if (Input.touchCount == 1)
		{
			Vector3 tPos = Input.GetTouch(0).position;
			tPos.z = 10;
			Vector3 wp = Camera.main.ScreenToWorldPoint(tPos);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			// If touched position equals the ninja
			if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
			{
				if(canSmash){
                    GameScene.updateScore();
                    
					GetComponent<Collider2D>().enabled = false;
					canSmash = false;
                    GameScene.showBlam(new Vector2(transform.position.x, transform.position.y));
				}
			}
		}


	}
		
	// Method to pop ninjas out by triggering its animation state
	public void pop(){
		animator.SetBool("pop", true);
		canSmash = true;
		GetComponent<Collider2D>().enabled = true;
		StartCoroutine(popEnds());
	}

	// Method to hide ninja back into hole after a few second
	IEnumerator popEnds(){
		yield return new WaitForSeconds(1.0f);
		GetComponent<Collider2D>().enabled = false;
		canSmash = false;
        GameScene.updateMisses();
	}

	// This is for PC control, when mouse is clicked in ninja, followin method is executed
	// This is same as the earlier method for mobile touch input
	void OnMouseDown() {
		if(canSmash){
            GameScene.updateScore();
            
            canSmash = false;
			GetComponent<Collider2D>().enabled = false;
            GameScene.showBlam(new Vector2(transform.position.x, transform.position.y));
		}
	}

}






