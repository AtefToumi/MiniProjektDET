using UnityEngine;
using UnityEngine.SceneManagement;

public class characterSelektion : MonoBehaviour
{
    private GameObject[] characterList;
    private int index;
    private string selectedCharacterDataName = "selectedCharacter";

    void Start()
    {
        
        characterList = new GameObject[transform.childCount];

        // Fill Array with models
        for (int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }

        characterList[0].SetActive(true);
        characterList[1].SetActive(false);
        
    }

    public void TroggleLeft()
    {
        characterList[index].SetActive(false);
        index--;

        if (index < 0)
        {
            index = characterList.Length - 1;
        }
        characterList[index].SetActive(true);
    }

    public void TroggleRight()
    {
        characterList[index].SetActive(false);
        index++;

        if (index == characterList.Length )
        {
            index = 0 ;
        }
        characterList[index].SetActive(true);
    }

   public void Select()
    {
        PlayerPrefs.SetInt(selectedCharacterDataName, index);
        SceneManager.LoadScene("Level01");
    }
 
}
