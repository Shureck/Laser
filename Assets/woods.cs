using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class woods : MonoBehaviour
{
    public bool isHolding;
    // Start is called before the first frame update
    void Start()
    {
        
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
            transform.position = new Vector3(3.255f, 1.5f, 10.026f);
            Vector3 euler = new Vector3(0, 90, 90);
            Quaternion rotation = Quaternion.Euler(euler);
            transform.rotation = rotation;
        }
    }


    // Update is called once per frame
    void Update()
    {
    }
}
