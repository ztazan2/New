using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private List<Node> path; // A* �˰������� ���� ���
    private int pathIndex; // ���� ��ο��� �̵� ���� ����� �ε���
    private Transform targetBase; // ���� ��ǥ�� ��� ������ Transform
    private Pathfinding pathfinding; // A* �˰��� ��� Ž���� ����ϴ� Pathfinding Ŭ���� ����
    public float moveSpeed = 3.5f; // ���� �̵� �ӵ�

    // Enemy�� �ʱ�ȭ �޼���. ��ǥ ������ Pathfinding ������Ʈ�� �����մϴ�.
    // baseTransform: ��ǥ ���� Transform
    // pathfindingComponent: Pathfinding ������Ʈ ����
    public void Initialize(Transform baseTransform, Pathfinding pathfindingComponent)
    {
        targetBase = baseTransform; // ��ǥ ���� ����
        pathfinding = pathfindingComponent; // Pathfinding ������Ʈ ����

        // Pathfinding�� TargetBase�� ���������� �ʱ�ȭ�Ǿ����� Ȯ��
        if (pathfinding != null && targetBase != null)
        {
            // A* �˰����� ����Ͽ� ��θ� ���
            path = pathfinding.FindPath(transform.position, targetBase.position);
            pathIndex = 0; // ����� ���� �ε����� 0���� ����
        }
        else
        {
            // �ʱ�ȭ ���� �� ���� �޽��� ���
            Debug.LogError("Pathfinding or TargetBase is not properly initialized in Enemy.");
        }
    }

    // �� �����Ӹ��� ȣ��Ǿ� ���� �̵���Ű�� �޼���
    private void Update()
    {
        // ��ΰ� �����ϰ�, ���� ��θ� ���󰡴� ������ Ȯ��
        if (path != null && pathIndex < path.Count)
        {
            // ���� �̵��ؾ� �� ����� ��ġ
            Vector3 targetPos = path[pathIndex].worldPosition;

            // MoveTowards�� ����Ͽ� ���� ��ġ���� ��ǥ ��ġ�� �̵�
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            // ��ǥ ��ġ�� �����ϸ� ���� ���� �̵�
            if (Vector3.Distance(transform.position, targetPos) < 0.1f)
            {
                pathIndex++; // ���� ���� �̵�
            }
        }
    }
}
