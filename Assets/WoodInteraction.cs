using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WoodInteraction : MonoBehaviour
{
    public float maxDistance = 50f;
    public float rotationAngle = 90f;
    public float moveSpeed = 5f;

    private bool isHoldingWood = false;
    private GameObject heldWood;
    private Rigidbody heldWoodRigidbody;
    public BoxCollider secondCollider;
    private int flag = 0;

    private IEnumerator TakeWood(Collider other)
    {

        isHoldingWood = true;
        heldWood = other.gameObject;
        if (other.CompareTag("Mina"))
        {
            heldWood.GetComponent<Mina>().isHolding = true;
        }
        if(other.CompareTag("Wood")){
            heldWood.GetComponent<woods>().isHolding = true;
        }
        heldWood.transform.rotation *= Quaternion.Euler(0f, rotationAngle, rotationAngle);
        heldWoodRigidbody = heldWood.GetComponent<Rigidbody>();
        heldWoodRigidbody.useGravity = false;
        heldWoodRigidbody.isKinematic = false;
        heldWoodRigidbody.constraints = RigidbodyConstraints.FreezeAll;

        heldWood.transform.position = transform.position + transform.forward * 0.1f;

        heldWood.transform.rotation = transform.rotation;
        heldWoodRigidbody.velocity = transform.forward * moveSpeed;
        yield return new WaitForSeconds(0.2f);
        flag = 1;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wood") || other.CompareTag("Mina"))
        {
            if (Keyboard.current.eKey.isPressed && !isHoldingWood)
            {
                StartCoroutine(TakeWood(other));
            }
            else if (Keyboard.current.eKey.isPressed && isHoldingWood && heldWood == other.gameObject && flag==1)
            {
                heldWoodRigidbody.useGravity = true;
                heldWoodRigidbody.constraints = RigidbodyConstraints.None;
                if (other.CompareTag("Mina"))
                {
                    heldWood.GetComponent<Mina>().isHolding = false;
                }
                if(other.CompareTag("Wood"))
                {
                    heldWood.GetComponent<woods>().isHolding = false;
                }
                isHoldingWood = false;
                heldWood = null;
                flag = 0;
            }
        }
    }

    private void Update()
    {
        if (Keyboard.current.eKey.isPressed)
        {
            secondCollider.enabled = true;
        }
        if (Keyboard.current.eKey.wasReleasedThisFrame)
        {
            secondCollider.enabled = false;
        }

        if (isHoldingWood && heldWood != null)
        {
            heldWood.transform.position = transform.position + transform.forward*0.1f;
            heldWood.transform.rotation = transform.rotation;
            heldWoodRigidbody.velocity = transform.forward * moveSpeed;
        }
    }
}