using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LeaderBoardController : MonoBehaviour
{
	private GameObject panel;

	void addRow ()
	{
		GameObject tableId = GameObject.Find ("table_id");
		GameObject rowId = GameObject.Instantiate (tableId);
//		Vector3 temp = new Vector3 (0, size.y, 0);
//		rowId.transform.position += temp;
	}

	// Use this for initialization
	void Start ()
	{
		panel = GameObject.Find ("Panel");

		addRow ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
