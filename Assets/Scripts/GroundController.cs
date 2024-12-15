using UnityEngine;

public class GroundController : MonoBehaviour
{
    public SpeedController speedController;
    private float speed;
    [SerializeField]
    private Collider trigger;
    public Vector3 targetPosition;

    private void Start()
    {
        // ��������� ������� �������
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        transform.position = newPosition;

        // ��� ������� (�����������)
        Debug.Log($"������ {gameObject.name} ��������� �� ����� �������: {newPosition}");

        if (speedController == null)
        {
            Debug.LogWarning("SpeedController �� ��������!(�����)"); // �������� ��������������, ���� speedController ����� null
        }
    }

    void FixedUpdate()
    {
        if (speedController != null)
        {
            speed = speedController.currentSpeed; // ��������� �������� ��� ������ �����
        }

        transform.position += Vector3.left * speed * Time.fixedDeltaTime; // ������������ Time.fixedDeltaTime � FixedUpdate
        // Debug.Log("Current Speed: " + speed); // ���������������, ���� �� �����
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("���-�� ����� � �������(�����)");
        if (other.gameObject.CompareTag("TriggerWall"))
        {
            Debug.Log("������������ � ���������!(�����)");
            TeleportObject(); // �������� ������������
        }
    }

    public void TeleportObject()
    {
        transform.localPosition = targetPosition; // ������������� ������� �� targetPosition
        Debug.Log("����� ������� ��� ������������(�����): " + targetPosition.ToString());
    }
}