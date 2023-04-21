using UnityEngine;

public class LiftObject : MonoBehaviour
{
    public float liftSpeed = 1.0f; // скорость поднятия объекта
    private bool isLifting = true; // флаг, указывающий, что объект поднимается

    // Метод Update() вызывается каждый кадр
    void Update()
    {
    }

    // Метод FixedUpdate() вызывается каждый фиксированный кадр
    void FixedUpdate()
    {
        // Если флаг isLifting установлен в true, то поднимаем объект
        if (isLifting)
        {
            transform.position += Vector3.up * liftSpeed * Time.fixedDeltaTime;
        }
    }
}