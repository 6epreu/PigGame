using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;

public class CameraScript : MonoBehaviour {

    public Text debugText;

    // Use this for initialization
    void Start () {}
	
	// Update is called once per frame
	void Update () {}

    public void loginClick()
    {
        debugText.text = "clicked";
        Debug.Log("cliekced");

    }
}
