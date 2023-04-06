using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void NewGame()
    {
        SceneManager.LoadScene("EasyLevelScene");
    }

    public void AdvanceGame()
    {
        SceneManager.LoadScene("GamePlayScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
