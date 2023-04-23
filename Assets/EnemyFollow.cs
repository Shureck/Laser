using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    private NavMeshAgent enemy;
    public Transform player;
    private GameObject catc;
    private bool _catched;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Catch"))
        {
            catc = other.transform.gameObject;
            Debug.Log("IsCathced");
            // Устанавливаем скорость агента в 0
            enemy.speed = 0f;
            _catched = true;
            // Запускаем корутину для восстановления скорости агента через 30 секунд
            StartCoroutine(ResumeAgentSpeed(15f));
        }
    }

    private IEnumerator ResumeAgentSpeed(float time)
    {
        yield return new WaitForSeconds(time);
        // Восстанавливаем скорость агента после заданного времени
        enemy.speed = 1.5f;
        _catched = false;
        //Destroy(catc);
        //Destroy(gameObject);
    }
}
