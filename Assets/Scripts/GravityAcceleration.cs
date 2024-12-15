using UnityEngine;

public class GravityAcceleration : MonoBehaviour
{
    public float additionalForce = 20f; // Дополнительная сила, которая будет добавляться вниз
    private Rigidbody rb;

    void Start()
    {
        // Получаем компонент Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Применяем дополнительную силу вниз
        Vector3 forceDirection = Vector3.down; // Направление силы (вниз)
        rb.AddForce(forceDirection * additionalForce, ForceMode.Acceleration);
    }
}