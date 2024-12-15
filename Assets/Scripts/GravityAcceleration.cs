using UnityEngine;

public class GravityAcceleration : MonoBehaviour
{
    public float additionalForce = 20f; // �������������� ����, ������� ����� ����������� ����
    private Rigidbody rb;

    void Start()
    {
        // �������� ��������� Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // ��������� �������������� ���� ����
        Vector3 forceDirection = Vector3.down; // ����������� ���� (����)
        rb.AddForce(forceDirection * additionalForce, ForceMode.Acceleration);
    }
}