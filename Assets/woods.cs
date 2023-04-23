using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class woods : MonoBehaviour
{
    public AudioSource audioSource;
    public float repeatRate = 1f;

    public bool isHolding;
    private int flag = 0; 
    // Start is called before the first frame update
    void Start()
    { 
        InvokeRepeating("PlayAudio", repeatRate, repeatRate);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Huy");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Kruk") && !isHolding)
        {
            Transform targetTransform = other.transform;
            transform.position = other.gameObject.GetComponent<positions_desk>().pos;
            Vector3 euler = new Vector3(0, 90, 90);
            Quaternion rotation = Quaternion.Euler(euler);
            transform.rotation = rotation;

            Rigidbody rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
        }
        if (other.CompareTag("Zombie") && !isHolding)
        {
            if (flag == 0)
            {
                audioSource.Play();
                flag = 1;
            }
            Destroy(gameObject, 10f);
        }
    }

    private void PlayAudio()
    {
        // Воспроизводим аудио
        if(flag == 1)
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
