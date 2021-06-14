using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void TryAgain() {
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      
    }
}
