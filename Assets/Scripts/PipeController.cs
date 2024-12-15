using UnityEngine;

public class PipeController : MonoBehaviour
{
    public SpeedController speedController;
    private float speed;
    [SerializeField]
    private Collider trigger;
    public Vector3 targetPosition;

    private void Start()
    {
        // �������� ������� ������� �������
        Vector3 currentPosition = transform.position;

        // ���������� ��������� �������� �� ��� Y �� -10 �� +10
        float randomY = Random.Range(-10f, 10f);

        // ������� ����� ������� � ���������� Y �����������
        Vector3 newPosition = new Vector3(currentPosition.x, currentPosition.y + randomY, currentPosition.z);

        // ��������� ������� �������
        transform.position = newPosition;

        // ��� ������� (�����������)
        Debug.Log($"������ {gameObject.name} ��������� �� ����� �������: {newPosition}");

        if (speedController == null)
        {
            Debug.LogWarning("SpeedController �� ��������!"); // �������� ��������������, ���� speedController ����� null
        }
    }

    void FixedUpdate()
    {
        if (speedController != null)
        {
            speed = speedController.currentSpeed; // ��������� �������� ��� ������ �����
        }

        transform.position += Vector3.left * speed * Time.fixedDeltaTime;
        //Debug.Log("Current Speed: " + speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("���-�� ����� � �������");
        if (other.gameObject.CompareTag("TriggerWall"))
        {
            Debug.Log("������������ � ���������!");
            TeleportWithRandomY(); // ������ �������� ������ ���� �����
        }
    }

    public void TeleportWithRandomY()
    {
        Debug.Log("������� targetPosition: " + targetPosition.ToString());
        float randomY = Random.Range(targetPosition.y - 12f, targetPosition.y + 12f);
        Vector3 newPosition = new Vector3(targetPosition.x, randomY, targetPosition.z);
        Debug.Log("����� ������� ��� ������������: " + newPosition.ToString());
        TeleportObject(newPosition);
    }

    public void TeleportObject(Vector3 newPosition)
    {
        transform.position = newPosition; // ���������� ��� ���������
    }
}