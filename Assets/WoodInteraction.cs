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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wood"))
        {
            if (!isHoldingWood)
            {
                isHoldingWood = true;
                heldWood = other.gameObject;
                heldWood.GetComponent<woods>().isHolding = true;
                heldWood.transform.rotation *= Quaternion.Euler(0f, rotationAngle, rotationAngle);
                heldWoodRigidbody = heldWood.GetComponent<Rigidbody>();
                heldWoodRigidbody.useGravity = false;
                heldWoodRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            }
            else
            {
                isHoldingWood = false;
                heldWoodRigidbody.useGravity = true;
                heldWoodRigidbody.constraints = RigidbodyConstraints.None;
                heldWood.GetComponent<woods>().isHolding = false;
                heldWood = null;
            }
        }
            
    }

    private void Update()
    {
        if (Keyboard.current.eKey.isPressed)
        {
            secondCollider.enabled = true;
            
        }
        else
        {
            secondCollider.enabled = false;
        }

        if (isHoldingWood && heldWood != null)
        {
            heldWood.transform.position = transform.position + transform.forward;
            heldWood.transform.rotation = transform.rotation;
            heldWoodRigidbody.velocity = transform.forward * moveSpeed;
        }
    }
}