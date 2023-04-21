using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastShoot : MonoBehaviour
{
    public GameObject circlePrefab;
    public GameObject greenPrefab;
    public GameObject gun;
    public float maxDistance = 50f;
    public float radius = 1f;
    public int numPoints = 10;
    public float delayFrames = 6; // 60 кадров = 1 секунда
    private float delayTimer = 0;
    private bool hold = false;

    private void Update()
    {

        // ограничиваем радиус в минимальное и максимальное значение
        radius += -Mouse.current.scroll.ReadValue().y / 1500;
        radius = Mathf.Clamp(radius, 0.05f, 0.7f);

        if (Keyboard.current.eKey.isPressed)
        {
            hold = true;
        }
        else
        {
            hold = false;
        }


        if (Mouse.current.leftButton.isPressed && !hold)
        {
            float angleIncrement = Mathf.PI * 2 / numPoints;

            if (delayTimer > 0)
            {
                delayTimer -= Time.deltaTime * 60; // уменьшаем таймер на количество прошедших кадров
            }
            else
            {
                for (int i = 0; i < numPoints; i++)
                {
                    Vector3 randomSpherePoint = Random.insideUnitSphere.normalized * radius;
                    float x = randomSpherePoint.x;
                    float y = randomSpherePoint.z;

                    Vector3 direction = transform.forward + Vector3.up * x + transform.rotation.normalized * Vector3.right * y;
                    Ray ray = new Ray(transform.position, direction);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, maxDistance))
                    {
                        if (hit.transform.CompareTag("Lift") || hit.transform.CompareTag("Catch"))
                        {
                            GameObject parentObj = hit.transform.gameObject;
                            GameObject childObj = Instantiate(greenPrefab, hit.point, Quaternion.identity);
                            childObj.transform.parent = parentObj.transform;
                            childObj.SetActive(true);
                            Destroy(childObj, 30f);
                        }
                        else
                        {
                           
                        }
                    }

                }
                delayTimer = delayFrames; // сбрасываем таймер
            }
        }
    }
}