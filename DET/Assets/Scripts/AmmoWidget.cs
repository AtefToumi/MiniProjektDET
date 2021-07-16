using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoWidget : MonoBehaviour
{
    public TMPro.TMP_Text ammoText;
    public void Refresh(int ammoCount){
        if(ammoCount >= 0){
            ammoText.text = ammoCount.ToString();
        }
        
    }
}

