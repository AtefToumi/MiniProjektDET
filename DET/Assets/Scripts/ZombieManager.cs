using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
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

        while (zombieCount < 10)
        {
            xPos = Random.Range(-13, 13);
            zPos = Random.Range(-14, 9);
            Instantiate(zombie, new Vector3(xPos, 0 , zPos), Quaternion.identity);
            yield return new WaitForSeconds(2f);
            zombieCount += 1;
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
