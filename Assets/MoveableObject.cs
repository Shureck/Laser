/*using UnityEngine;
using UnityEngine.InputSystem;

public class MoveableObject : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _distanceToPlayer = 3f;
    [SerializeField] private float _rotationSpeed = 3f;

    private Rigidbody _rigidbody;
    private Transform _playerTransform;
    private bool _isMoving;
    private Vector3 _targetPosition;


    public GameObject player;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerTransform = player.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerTransform = null;
            _isMoving = false;
        }
    }

    private void Update()
    {
        if (_playerTransform != null && Keyboard.current.eKey.isPressed)
        {
            _isMoving = true;

            // Get the direction and speed of the player's movement
            Vector3 playerMovement = _playerTransform.GetComponent<Rigidbody>().velocity.normalized;
            float playerSpeed = _playerTransform.GetComponent<Rigidbody>().velocity.magnitude;

            // Calculate the target position for the cart based on the player's movement direction and speed
            _targetPosition = _playerTransform.position - playerMovement * _distanceToPlayer;

            // Rotate the cart towards the player's movement direction
            Quaternion targetRotation = Quaternion.LookRotation(playerMovement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
        }
        else
        {
            _isMoving = false;
        }
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            // Move the cart towards the target position
            Vector3 direction = (_targetPosition - transform.position).normalized;
            direction = new Vector3(direction.x, 0, direction.z);
            _rigidbody.MovePosition(transform.position - direction * _moveSpeed * Time.fixedDeltaTime);
        }
    }
}
*/


using UnityEngine;
using UnityEngine.InputSystem;

public class MoveableObject : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _distanceToPlayer = 2f;
    [SerializeField] private float _rotationSpeed = 5f;

    private Rigidbody _rigidbody;
    private Transform _playerTransform;
    private bool _isMoving;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true; // make sure the object doesn't fall when moving
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerTransform = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerTransform = null;
            _isMoving = false;
        }
    }

    private void Update()
    {
        if (_playerTransform != null && Keyboard.current.eKey.isPressed)
        {
            // Set the object's position to be at the player's distance plus the offset
            Vector3 direction = (_playerTransform.position - transform.position).normalized;
            Vector3 targetPosition = _playerTransform.position - direction * _distanceToPlayer;
            transform.position = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);

            // Set the object's rotation to look at the player
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Quaternion yRotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y+90, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, yRotation, _rotationSpeed * Time.deltaTime);

            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
    }
}

/*
 using UnityEngine;
using UnityEngine.InputSystem;

public class MoveableObject : MonoBehaviour
{
    [SerializeField] private float _grabDistance = 2f;
    [SerializeField] private float _rotationSpeed = 5f;

    private Rigidbody _rigidbody;
    private Transform _playerTransform;
    private bool _isGrabbed;
    private Vector3 _grabOffset;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerTransform = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerTransform = null;
            _isGrabbed = false;
        }
    }

    private void FixedUpdate()
    {
        if (_isGrabbed && _playerTransform != null)
        {
            // Move the object towards the player
            Vector3 direction = (_playerTransform.position - transform.position).normalized;
            _rigidbody.MovePosition(transform.position + direction * _grabDistance * Time.fixedDeltaTime);

            // Rotate the object towards the player
            Vector3 playerDirection = _playerTransform.position - transform.position;
            playerDirection.y = 0f;
            Quaternion targetRotation = Quaternion.LookRotation(playerDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }

    private void Update()
    {
        if (_playerTransform != null && Keyboard.current.eKey.isPressed)
        {
            RaycastHit hit;
            if (Physics.Raycast(_playerTransform.position, _playerTransform.forward, out hit, _grabDistance))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    _isGrabbed = true;
                    _grabOffset = transform.position - hit.point;
                }
            }
        }
        else if (_isGrabbed && !Keyboard.current.eKey.isPressed)
        {
            _isGrabbed = false;
        }

        if (_isGrabbed)
        {
            // Move the object to the position of the grab point
            Vector3 grabPoint = _playerTransform.position + _playerTransform.forward * _grabDistance + _grabOffset;
            _rigidbody.MovePosition(grabPoint);
        }
    }
}
*/