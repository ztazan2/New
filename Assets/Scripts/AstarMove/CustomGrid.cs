using System.Collections.Generic;
using UnityEngine;

public class CustomGrid : MonoBehaviour
{
    public LayerMask unwalkableMask; // �̵� �Ұ����� ������ ��Ÿ���� ���̾�
    public Vector2 gridWorldSize;    // �׸����� ���� ũ��
    public float nodeRadius;         // �� ����� ������

    private Node[,] grid; // �׸����� ��� �迭
    private float nodeDiameter; // ����� ���� (������ * 2)
    private int gridSizeX, gridSizeY; // �׸����� X, Y ũ�� (��� ����)

    public List<Node> path; // ���� ��θ� ����

    public bool isLook = false; // ��� ǥ�� ���� 

    // ���� �� �׸��带 �ʱ�ȭ
    void Start()
    {
        nodeDiameter = nodeRadius * 2; // ����� ���� ���
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter); // X ���� ��� ���� ���
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter); // Y ���� ��� ���� ���
        CreateGrid(); // �׸��� ����
    }

    // �׸��� ���� �޼���
    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY]; // ��� �迭 �ʱ�ȭ
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++) // X�� ��ȸ
        {
            for (int y = 0; y < gridSizeY; y++) // Y�� ��ȸ
            {
                // �� ����� ���� ��ǥ ���
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                // ��尡 �̵� �������� Ȯ��
                bool walkable = !Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask);
                grid[x, y] = new Node(walkable, worldPoint, x, y); // ��� ����
            }
        }
    }

    // ���� ��ǥ���� �ش� ��ġ�� ��带 ��ȯ
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        // ���� ��ǥ�� �׸��� ��ǥ�� ��ȯ
        float percentX = Mathf.Clamp01((worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x);
        float percentY = Mathf.Clamp01((worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y]; // �ش� ��� ��ȯ
    }

    // Ư�� ����� �̿� ��� ����Ʈ ��ȯ
    public List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>(); // �̿� ��� ����Ʈ �ʱ�ȭ

        for (int x = -1; x <= 1; x++) // �ֺ� ��带 �˻� (-1, 0, 1)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) // �ڱ� �ڽ��� ����
                    continue;

                int checkX = node.gridX + x; // �˻��� X ��ġ
                int checkY = node.gridY + y; // �˻��� Y ��ġ

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) // �׸��� ���� Ȯ��
                {
                    neighbors.Add(grid[checkX, checkY]); // �̿� ��� �߰�
                }
            }
        }

        return neighbors; // �̿� ��� ����Ʈ ��ȯ
    }

    // �׸��带 �ð������� ǥ��
    void OnDrawGizmos()
    {
        if (isLook == false) // ȭ�鿡 ��� ǥ�� ����
        {// �׸��� ��ü ������ �ڽ��� ǥ��
            Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

            if (grid != null) // �׸��尡 ������ ��� 
            {
                foreach (Node n in grid)
                {
                    // �̵� ���� ���ο� ���� ���� ����
                    Gizmos.color = n.walkable ? Color.white : Color.red;
                    if (path != null && path.Contains(n)) // ��ο� ���Ե� ���� ������
                        Gizmos.color = Color.black;
                    // ��带 �ڽ��� ǥ��
                    Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
                }
            }
        }
    }
}
