using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public float initialSpeed = 15.0f; // ��������� ��������
    public float acceleration = 1.0f;  // ���������� �������� �� �������
    public float maxSpeed = 30.0f;     // ������������ ��������

    public float currentSpeed;         // ������� ��������

    void Start()
    {
        currentSpeed = initialSpeed; // ������������� ��������� ��������
        Debug.Log("��������� �������� �����������: " + currentSpeed);
    }

    void FixedUpdate()
    {
        // ����������� ��������
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += acceleration * Time.fixedDeltaTime; // ����������� �������� � �������� �������
            //Debug.Log("Initial Speed: " + currentSpeed);
        }

        // ���������� �������� � �������
        //transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }
}