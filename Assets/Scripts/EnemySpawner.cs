using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // ������ �� ������
    public Transform targetBase;   // ���� �̵��� ��ǥ ������ Transform
    public float spawnInterval = 3.0f; // �� ���� ���� (�� ����)

    private Vector3 spawnPosition = new Vector3(-12f, 1f, -9f); // ���� ���� ���� ��ġ
    private Pathfinding pathfinding; // A* ��� Ž���� ���� Pathfinding ��ũ��Ʈ ����

    private void Start()
    {
        // Pathfinding ��ũ��Ʈ �ʱ�ȭ
        pathfinding = FindObjectOfType<Pathfinding>();
        if (pathfinding == null)
        {
            // Pathfinding ��ũ��Ʈ�� ������ ���� ���
            Debug.LogError("���� Pathfinding ��ũ��Ʈ�� �����ϴ�. ������ GameObject�� Pathfinding ��ũ��Ʈ�� �߰��ϼ���.");
            return;
        }

        // ������ �ð� �������� �� ����
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab != null && targetBase != null)
        {
            // �� �������� ����
            GameObject enemyObj = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            Enemy enemyScript = enemyObj.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                // �� �ʱ�ȭ: ��ǥ ������ Pathfinding ����
                enemyScript.Initialize(targetBase, pathfinding);
            }
        }
        else
        {
            // �� �������̳� ��ǥ ������ �������� �ʾ��� ��� ���� ���
            Debug.LogError("EnemyPrefab �Ǵ� TargetBase�� EnemySpawner�� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }
}
