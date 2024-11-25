using UnityEngine;

public class Node
{
    public bool walkable; // 이동 가능한지 여부
    public Vector3 worldPosition; // 월드 좌표
    public int gridX, gridY; // 노드의 격자 좌표

    public int gCost, hCost; // A* 비용 계산
    public Node parent; // 경로 추적용 부모 노드

    public Node(bool walkable, Vector3 worldPosition, int gridX, int gridY)
    {
        this.walkable = walkable;
        this.worldPosition = worldPosition;
        this.gridX = gridX;
        this.gridY = gridY;
    }

    public int fCost => gCost + hCost; // 총 비용
}
