using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class completeLevel01 : MonoBehaviour
{
     NEXTLEVELUI nextlevel ;
    public void loadNextLevel()
    {
       SceneManager.LoadScene(2);
    }
}
