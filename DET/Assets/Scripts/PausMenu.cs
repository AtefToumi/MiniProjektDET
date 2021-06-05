using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausMenu : MonoBehaviour
{
    public GameObject pauseCanvse;
    public bool IsPaused = false;
    Component movementScript;

    public GameObject[] characterList;
    private int index;
    private string selectedCharacterDataName = "selectedCharacter";

    //  GameObject audioManager;


    private void Start()
    {
       
        //  audioManager = GameObject.FindGameObjectWithTag("AudioManager");
        index = PlayerPrefs.GetInt(selectedCharacterDataName, 0);
       
    


    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if ( IsPaused )
            {
                resume();
            }
            else
            {
                Debug.Log("pause wird gedrükt");
                pause();
               
            }
            
        }

    }
    public void pause()
    {
        pauseCanvse.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
        characterList[index].GetComponent<PlayerMovement>().enabled = false;
        // audioManager.GetComponent<AudioManager>().enabled = false;


    }
    public void resume()
    {
        pauseCanvse.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
        characterList[index].GetComponent<PlayerMovement>().enabled = true;

    }

    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
