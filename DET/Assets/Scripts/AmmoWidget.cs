using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoWidget : MonoBehaviour
{
    public TMPro.TMP_Text ammoText;
    public TMPro.TMP_Text clipText;

    public void RefreshAmmo(int ammoCount, int index){
        if(ammoCount >= 0 && index == 0){
            ammoText.text = ammoCount.ToString();
        }
        if(ammoCount >=0 && index == 1)
        {
            ammoText.text = ammoCount.ToString();
        }
    }

    public void RefreshClip(int clipCount, int index)
    {
        if (clipCount > 0 && index == 0)
        {
            clipText.text = (clipCount * 30).ToString();
        }
        if (clipCount >= 0 && index == 1)
        {
            clipText.text = (clipCount * 8).ToString();
        }
    }
}

