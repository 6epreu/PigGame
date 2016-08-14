using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {


    // Use this for initialization
    void Start()
    {
        AppGlobal.isContinious = false;
        AppGlobal.totalScore = 0f;
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("LoginScene");
        }
    }

    public void clickJumper()
    {
		AppGlobal.totalScore = 0f;
        SceneManager.LoadScene("Level1");
    }

    public void clickWheel()
    {
		AppGlobal.totalScore = 0f;
        SceneManager.LoadScene("QuizWheel");
    }

    public void clickNinja()
    {
		AppGlobal.totalScore = 0f;
        SceneManager.LoadScene("GameScene");
        //Application.LoadLevel(Levels.TimerMode);
    }

    public void clickmemory()
    {
		AppGlobal.totalScore = 0f;
		SceneManager.LoadScene("MemoryGame", LoadSceneMode.Single);
    }

    public void clickStartFullGame()
    {
        Debug.Log("start new game");
        AppGlobal.isContinious = true;
        AppGlobal.totalScore = 0f;
        SceneManager.LoadScene("GameScene");
    }

    public void clickStatistic()
    {
        //
    }

}

