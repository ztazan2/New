using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab; // ������ ť�� ������
    public Camera mainCamera;     // Ŭ�� ������ ���� ī�޶�
    public float fixedY = 0.6f;   // ������ ť���� y�� (Inspector���� ���� ����)

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���� ���콺 Ŭ�� ����
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); // ȭ�� ��ǥ -> ���� ����
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // ���̰� �浹�� ��ġ
            {
                // �浹 ������ ť�� ����
                GameObject newCube = Instantiate(cubePrefab, hit.point, Quaternion.identity);

                // ������ ť���� y ���� Inspector���� ������ ������ ����
                Vector3 fixedPosition = newCube.transform.position;
                fixedPosition.y = fixedY; // y�� ����
                newCube.transform.position = fixedPosition;
            }
        }
    }
}
