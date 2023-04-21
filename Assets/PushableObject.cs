using UnityEngine;

public class PushableObject : MonoBehaviour
{
    [SerializeField] private float _pushForce = 10f;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log("Push");
        if (collision.gameObject.CompareTag("Player"))
        {
            // Calculate the direction in which the player is pushing the object
            Vector3 pushDirection = collision.transform.forward;

            // Apply a force to the object in the push direction
            _rigidbody.AddForce(pushDirection * _pushForce, ForceMode.Impulse);
        }
    }
}