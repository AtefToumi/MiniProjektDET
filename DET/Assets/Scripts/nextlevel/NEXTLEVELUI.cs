using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NEXTLEVELUI : MonoBehaviour
{
    public GameObject Level2LoadingUI;


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("collision detected");

               completeLevel01();

        
        }
    }

   public void completeLevel01 ()
    {
        Level2LoadingUI.SetActive(true);
       
    }

}
