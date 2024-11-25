using UnityEngine;

public class Node
{
    public bool walkable; // ��尡 �̵� �������� ���θ� ��Ÿ��
    public Vector3 worldPosition; // ����� ���� ��ǥ
    public int gridX, gridY; // ����� ���� ��ǥ (�׸��� �� ��ġ)

    public int gCost, hCost; // A* �˰��򿡼� ����ϴ� ���
    public Node parent; // ��θ� �����ϱ� ���� �θ� ���

    // ��� ������: �̵� ���� ����, ���� ��ǥ, ���� ��ǥ�� ����
    public Node(bool walkable, Vector3 worldPosition, int gridX, int gridY)
    {
        this.walkable = walkable; // �̵� ���� ���� ����
        this.worldPosition = worldPosition; // ���� ��ǥ ����
        this.gridX = gridX; // ���� X ��ǥ ����
        this.gridY = gridY; // ���� Y ��ǥ ����
    }

    // �� ��� ���: gCost(��������� ���) + hCost(���� ���)
    public int fCost => gCost + hCost;
}
