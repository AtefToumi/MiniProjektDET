using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManagerLevel1 : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject zombie;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    public int xPos;
    public int zPos;
    public int zombieCount;
    public int zombieCountLimit = 15 ;

    IEnumerator ZombieSpawn()
    {
        yield return new WaitForSeconds(2f);

        
        while(zombieCount < zombieCountLimit )
        {
            if(ScoreManager.score <= 150)
            {
                xPos = Random.Range(150, 170);
                zPos = Random.Range(80, 170);
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
