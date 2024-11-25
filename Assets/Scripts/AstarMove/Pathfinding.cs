using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private CustomGrid grid; // CustomGrid ��ũ��Ʈ ����

    void Awake()
    {
        // Grid ��ũ��Ʈ�� �������� ã��
        grid = FindObjectOfType<CustomGrid>();
        if (grid == null)
        {
            Debug.LogError("���� CustomGrid ��ũ��Ʈ�� �����ϴ�. ������ GameObject�� CustomGrid ��ũ��Ʈ�� �߰��ϼ���.");
        }
    }

    public List<Node> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        // grid�� null�̸� ��� Ž�� �ߴ�
        if (grid == null)
        {
            Debug.LogError("CustomGrid�� �ʱ�ȭ���� �ʾҽ��ϴ�. CustomGrid ��ũ��Ʈ�� Ȯ���ϼ���.");
            return null;
        }

        // ���� ���� ��ǥ ��带 ã��
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        // ���� ��� ���հ� ���� ��� ���� ����
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                // ���� ���� ���Ͽ� ���(fCost)�� �� ���� ��带 ����
                if (openSet[i].fCost < currentNode.fCost || (openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost))
                {
                    currentNode = openSet[i];
                }
            }

            // ���� ��带 ���� ���տ��� �����ϰ� ���� ���տ� �߰�
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            // ��ǥ ��忡 ������ ��� ��θ� ��ȯ
            if (currentNode == targetNode)
            {
                return RetracePath(startNode, targetNode);
            }

            // ���� ����� �̿� ��� �˻�
            foreach (Node neighbor in grid.GetNeighbors(currentNode))
            {
                // �̵� �Ұ����� ����̰ų� �̹� ���� ���տ� �ִ� ��� �ǳʶ�
                if (!neighbor.walkable || closedSet.Contains(neighbor))
                    continue;

                // ���� ��带 ���� �̿� ���� ���� ��� ���
                int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    // ��� ����
                    neighbor.gCost = newMovementCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, targetNode);
                    neighbor.parent = currentNode; // �θ� ��� ����

                    // ���� ���տ� �߰� (�ߺ� ����)
                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        return null; // ��θ� ã�� ���� ��� null ��ȯ
    }

    private List<Node> RetracePath(Node startNode, Node endNode)
    {
        // ���� ������ ��ǥ �������� ��θ� ����
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode); // ���� ��带 ��ο� �߰�
            currentNode = currentNode.parent; // �θ� ���� �̵�
        }
        path.Reverse(); // ��θ� �������� ���� (���� ������ ��ǥ ������)

        grid.path = path; // Grid�� ��� ���� (������)
        return path;
    }

    private int GetDistance(Node nodeA, Node nodeB)
    {
        // �� ��� ���� �Ÿ� ���
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        // �밢�� �̵� ���(14)�� ���� �̵� ���(10) ���
        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
