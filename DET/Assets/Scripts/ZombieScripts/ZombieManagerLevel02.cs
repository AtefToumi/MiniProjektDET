using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManagerLevel02 : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject zombie;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    public int xPos;
    public int zPos;
    public int zombieCount;

     IEnumerator ZombieSpawn()
    {
        if(playerHealth.currentHealth <= 0)
        {
            yield break;
        }
        while(zombieCount < 40)
        {
            if(ScoreManager.score <= 200)
            {
                xPos = Random.Range(-7, 4);
                zPos = Random.Range(-17, 6);
                Instantiate(zombie, new Vector3(xPos, 0 , zPos), Quaternion.identity);
             
                yield return new WaitForSeconds(2f);
                zombieCount += 1;
            }
            else
            {
                xPos = Random.Range(-9, 2);
                zPos = Random.Range(41, 12);
                Instantiate(zombie, new Vector3(xPos, 0 , zPos), Quaternion.identity);
                yield return new WaitForSeconds(2f);
                zombieCount += 1;
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
       //InvokeRepeating("Spawn", spawnTime, spawnTime); 
       StartCoroutine(ZombieSpawn());
    }

    void Spawn()
    {
        if(playerHealth.currentHealth <=0)
         {
             return;
         }
         int spawnPointIndex = Random.Range(0, spawnPoints.Length);
         Instantiate(zombie, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
    
}
