using UnityEngine;

public class LiftObject : MonoBehaviour
{
    public float liftSpeed = 1.0f; // �������� �������� �������
    private bool isLifting = true; // ����, �����������, ��� ������ �����������

    // ����� Update() ���������� ������ ����
    void Update()
    {
    }

    // ����� FixedUpdate() ���������� ������ ������������� ����
    void FixedUpdate()
    {
        // ���� ���� isLifting ���������� � true, �� ��������� ������
        if (isLifting)
        {
            transform.position += Vector3.up * liftSpeed * Time.fixedDeltaTime;
        }
    }
}