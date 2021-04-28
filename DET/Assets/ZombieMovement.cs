using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
   Transform player;

   NavMeshAgent nav;

   void Awake() {
       player = GameObject.FindGameObjectWithTag("Player").transform;
       nav = GetComponent<NavMeshAgent>();
   }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(player.position);
    }
}
