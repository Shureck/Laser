using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

[RequireComponent(typeof(LineRenderer))]
public class Scanner : MonoBehaviour
{
    private List<Vector3> _positionsList = new();
    private List<VisualEffect> _vfxList = new();
    private VisualEffect _currentVFX;
    private Texture2D _texture;
    private Color[] _positions;
    private bool _createNewVFX;
    public CameraControllerPanel cam;
    //private int _particleAmount;
    private LineRenderer _lineRenderer;

    private const string PLAYER_TAG = "Player";
    private const string TEXTURE_NAME = "PositionsTexture";
    private const string RESOLUTION_PARAMETER_NAME = "Resolution";
    //private const string PARTICLE_AMOUNT_PARAMETER_NAME = "ParticleAmount";
    //private const string PARTICLES_PER_SCAN_PARAMETER_NAME = "ParticlesPerScan";

    public GameObject greenPrefab;
    public GameObject RedPrefab;

    public bool isWeb = false;

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private VisualEffect _vfxPrefab;
    [SerializeField] private GameObject _vfxContainer;
    [SerializeField] private Transform _castPoint;
    [SerializeField] private float _radius = 10f;
    [SerializeField] private float _maxRadius = 10f;
    [SerializeField] private float _minRadius = 1f;
    [SerializeField] private int _pointsPerScan = 100;
    [SerializeField] private float _range = 10f;

    [SerializeField] private int resolution = 16000;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = false;
        _createNewVFX = true;
        CreateNewVisualEffect();
        ApplyPositions();
    }

    private void FixedUpdate()
    {
        Scan();
    }

    private void Update()
    {
        _radius += -Mouse.current.scroll.ReadValue().y / 500;
        _radius = Mathf.Clamp(_radius, 0.3f, 10f);
    }

    private void ApplyPositions()
    {
        // create array from list
        Vector3[] pos = _positionsList.ToArray();


        // cache position for offset
        Vector3 vfxPos = _currentVFX.transform.position;

        // cache transform position
        Vector3 transformPos = transform.position;

        // cache some more stuff for faster access
        int loopLength = _texture.width * _texture.height;
        int posListLen = pos.Length;

        for (int i = 0; i < loopLength; i++)
        {
            Color data;

            if (i < posListLen - 1)
            {
                data = new Color(pos[i].x - vfxPos.x, pos[i].y - vfxPos.y, pos[i].z - vfxPos.z, 1);
            }
            else
            {
                data = new Color(0, 0, 0, 0);
            }
            _positions[i] = data;
        }

        // apply to texture
        _texture.SetPixels(_positions);
        _texture.Apply();

        // apply to VFX
        _currentVFX.SetTexture(TEXTURE_NAME, _texture);
        _currentVFX.Reinit();
    }

    private void CreateNewVisualEffect() // this is fucking performance heavy help
    {
        // make sure it only gets called once
        if (!_createNewVFX) return;

        // add old VFX to list
        _vfxList.Add(_currentVFX);
        _currentVFX = Instantiate(_vfxPrefab, transform.position, Quaternion.identity, _vfxContainer.transform);
        _currentVFX.SetUInt(RESOLUTION_PARAMETER_NAME, (uint)resolution);
        // create new VFX
        //_currentVFX = Instantiate(_vfxPrefab, transform.position, Quaternion.identity, _vfxContainer.transform);
        //_currentVFX.SetUInt(RESOLUTION_PARAMETER_NAME, (uint)resolution);
        //_currentVFX.SetInt(PARTICLES_PER_SCAN_PARAMETER_NAME, _pointsPerScan);

        // create texture
        _texture = new Texture2D(resolution, resolution, TextureFormat.RGBAFloat, false);

        // create color array for positions
        _positions = new Color[resolution * resolution];

        // clear list
        _positionsList.Clear();

        // set particle amount to 0
        //_particleAmount = 0;
        //_currentVFX.SetInt(PARTICLE_AMOUNT_PARAMETER_NAME, _particleAmount);

        _createNewVFX = false;
    }

    private void Scan()
    {
        // only call if button is pressed
        if (Mouse.current.leftButton.isPressed || cam.pressed)
        {
            for (int i = 0; i < _pointsPerScan; i++)
            {
                // generate random point
                Vector3 randomPoint = Random.insideUnitSphere * _radius;
                randomPoint += _castPoint.position;

                // calculate direction to random point
                Vector3 dir = (randomPoint - transform.position).normalized;

                // cast ray
                if (Physics.Raycast(transform.position, dir, out RaycastHit hit, _range, _layerMask))
                {
                    Debug.DrawRay(transform.position, dir * hit.distance, Color.green);
                    // only add point if the particle count limit is not reached
                    if (hit.transform.CompareTag("Lift") || hit.transform.CompareTag("Catch"))
                    {
                        GameObject parentObj = hit.transform.gameObject;
                        GameObject childObj = Instantiate(greenPrefab, hit.point, Quaternion.identity);
                        childObj.transform.parent = parentObj.transform;
                        childObj.SetActive(true);
                        Destroy(childObj, 30f);
                    }
                    else if (isWeb) {
                        GameObject parentObj = hit.transform.gameObject;
                        GameObject childObj = Instantiate(RedPrefab, hit.point, Quaternion.identity);
                        childObj.transform.parent = parentObj.transform;
                        childObj.SetActive(true);
                        Destroy(childObj, 30f);
                    }
                    if (!isWeb)
                    {
                        if (_positionsList.Count < resolution * resolution)
                        {
                            if (hit.collider.CompareTag("Zombie")) continue;
                            if (hit.collider.CompareTag("Lift")) continue;
                            if (hit.collider.CompareTag("Catch")) continue;
                            _positionsList.Add(hit.point);
                            _lineRenderer.enabled = true;
                            _lineRenderer.SetPositions(new[]
                            {
                            transform.position,
                            hit.point
                        });
                            //_particleAmount++;
                            //_currentVFX.SetInt(PARTICLE_AMOUNT_PARAMETER_NAME, _particleAmount);
                        }
                        // create new VFX if the particle count limit is reached
                        else
                        {
                            _createNewVFX = true;
                            CreateNewVisualEffect();
                            break;
                        }
                    }
                } // raycast
                else
                {
                    Debug.DrawRay(transform.position, dir * _range, Color.red);
                }
            } // for loop
            ApplyPositions();
        } // button press
        else
        {
            _lineRenderer.enabled = false;
        }
    }
}