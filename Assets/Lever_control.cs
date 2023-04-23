using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever_control : MonoBehaviour
{
    public float liftSpeed = 1.0f; // �������� �������� �������
    public bool isLifting = false;
    public GameObject lift;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isLifting)
            {
                Vector3 euler = new Vector3(32.635f, 0, 0);
                Quaternion rotation = Quaternion.Euler(euler);
                transform.rotation = rotation;
                isLifting = true;
            }
        }
    }

    void FixedUpdate()
    {
        // ���� ���� isLifting ���������� � true, �� ��������� ������
        if (isLifting)
        {
            lift.transform.position += Vector3.up * liftSpeed * Time.fixedDeltaTime;
        }
    }
}
