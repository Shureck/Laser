using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mina : MonoBehaviour
{
    public bool isHolding;
    private bool isGrounded = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Kruk") && !isHolding)
        {
            Transform targetTransform = other.transform;
            transform.position = new Vector3(4.12f, 1.403f, 24.029f);
            Vector3 euler = new Vector3(0, 90, 0);
            Quaternion rotation = Quaternion.Euler(euler);
            transform.rotation = rotation;

            Rigidbody rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
        }
        if (other.CompareTag("Zombie") && !isHolding)
        {
            Destroy(gameObject, 15f);
        }

    }
        // Update is called once per frame
    void Update()
    {
    }
}
