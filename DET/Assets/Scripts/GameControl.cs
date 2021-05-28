using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject[] characterList;
    private int index;
    private string selectedCharacterDataName = "selectedCharacter";

    public Transform PlayerStartPosition;
    public GameObject player;
    public GameObject playerTwo;

    // Start is called before the first frame update
    void Start()
    {
        
        index = PlayerPrefs.GetInt(selectedCharacterDataName, 0);
        
        if(index == 1)
        {
            player.SetActive(true);
        }
        else
        {
            playerTwo.SetActive(true);
        }
      
       
        
        //player = Instantiate(characterList[index], PlayerStartPosition.position, characterList[index].transform.rotation);
    }

   
}
