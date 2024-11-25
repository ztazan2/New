using UnityEngine;

public class Node
{
    public bool walkable; // �̵� �������� ����
    public Vector3 worldPosition; // ���� ��ǥ
    public int gridX, gridY; // ����� ���� ��ǥ

    public int gCost, hCost; // A* ��� ���
    public Node parent; // ��� ������ �θ� ���

    public Node(bool walkable, Vector3 worldPosition, int gridX, int gridY)
    {
        this.walkable = walkable;
        this.worldPosition = worldPosition;
        this.gridX = gridX;
        this.gridY = gridY;
    }

    public int fCost => gCost + hCost; // �� ���
}
