using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public void clickJumper()
    {
        SceneManager.LoadScene("Level1");
    }

    public void clickWheel()
    {
        SceneManager.LoadScene("QuizWheel");
    }

    public void clickNinja()
    {
        //
    }

    public void clickmemory()
    {
        //
    }
}
