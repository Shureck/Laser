using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    private NavMeshAgent enemy;
    public Transform player; 
    // Start is called before the first frame update
    void Start()
    {
        // player = GameObject.FindGameObjectWithTag("PlayerCapsule").transform;
        enemy = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(player.position);
        // transform.LookAt(player.transform);
        // transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
