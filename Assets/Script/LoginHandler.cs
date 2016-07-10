using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System;

public class LoginHandler : MonoBehaviour {

    public const string LOGIN_URI = "http://128.199.56.123:4570/login";
    public const string REGISTRATION_URI = "http://128.199.56.123:4570/register";
    public Text debugText;
    public Text loginText;
    public Text passwordText;
    public Toggle registrationToggle;

    public GameObject dialogPanel;
    public Text dialogText;

    public GameObject progressPanel;

    // Use this for initialization
    void Start() { }

    // Update is called once per frame
    void Update() { }


    // Update is called once per frame
    public void loginClick () {
        showProgress();

        if (registrationToggle.isOn)
            StartCoroutine(executeRegistration());
        else
            StartCoroutine(executeLogin());
    }

    public void closeDialog()
    {
        dialogPanel.SetActive(false);
    }

    public void showDialog( String text )
    {
        dialogPanel.SetActive(true);
        dialogText.text = text;
    }
    

    private IEnumerator executeLogin()
    {
        Debug.Log("clicked");

        AuthData auth = new AuthData(loginText.text, loginText.text);
        String requestString = auth.toJson();
        
        Debug.Log("requestString: " + requestString);

        System.Collections.Generic.Dictionary<string, string> headers = new System.Collections.Generic.Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");

        var encoding = new System.Text.UTF8Encoding();
        WWW request = new WWW(LOGIN_URI, encoding.GetBytes(requestString), headers);
        yield return request;

        LoginResponse jResponse = JsonUtility.FromJson<LoginResponse>(request.text);
        hideProgress();
        // Print the error to the console
        if (request.error != null  )
        {
            Debug.Log("request error: " + request.error);
            showDialog("request error: " + request.error);
        }
        else
        {
            if (!String.IsNullOrEmpty(jResponse.message))
            {
                Debug.Log("request error: " + jResponse.message);
                showDialog(jResponse.message);
            }
            else
            {
                SceneManager.LoadScene("Level1");
            }
        }
    }

    private IEnumerator executeRegistration()
    {
        AuthData auth = new AuthData(loginText.text, loginText.text);
        String requestString = auth.toJson();
        Debug.Log("requestString: " + requestString);

        System.Collections.Generic.Dictionary<string, string> headers = new System.Collections.Generic.Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");

        var encoding = new System.Text.UTF8Encoding();
        WWW request = new WWW(REGISTRATION_URI, encoding.GetBytes(requestString), headers);
        yield return request;

        hideProgress();
        // Print the error to the console
        LoginResponse jResponse = JsonUtility.FromJson<LoginResponse>(request.text);
        if (request.error != null)
        {
            Debug.Log("request error: " + request.error);
            showDialog("request error: " + request.error);
        }
        else
        {
            if (!String.IsNullOrEmpty(jResponse.message))
            {
                Debug.Log("request error: " + jResponse.message);
                showDialog(jResponse.message);
            }
            else
            {
                SceneManager.LoadScene("Level1");
            }
        }
    }

    public void showProgress()
    {
        progressPanel.SetActive(true);
    }

    public void hideProgress()
    {
        progressPanel.SetActive(false);
    }
}
