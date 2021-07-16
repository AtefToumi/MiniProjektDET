using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    bool isFlickering = false;
    float delayTime;


    // Update is called once per frame
    void Update()
    {
        if(isFlickering == false)
        {
            StartCoroutine(flickeringLight());
        }
    }
     IEnumerator flickeringLight()
    {
        isFlickering = true;
        this.gameObject.GetComponent<Light>().enabled = false;
        delayTime = Random.Range(0.01f, 0.2f);
        yield return new WaitForSeconds(delayTime);
        this.gameObject.GetComponent<Light>().enabled = true;
        delayTime = Random.Range(0.01f, 0.2f);
        yield return new WaitForSeconds(delayTime);
        isFlickering = false;
    }
}
