using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private List<Node> path; // A* 알고리즘으로 계산된 경로
    private int pathIndex; // 현재 경로에서 이동 중인 노드의 인덱스
    private Transform targetBase; // 적이 목표로 삼는 기지의 Transform
    private Pathfinding pathfinding; // A* 알고리즘 경로 탐색을 담당하는 Pathfinding 클래스 참조
    public float moveSpeed = 3.5f; // 적의 이동 속도

    // Enemy의 초기화 메서드. 목표 기지와 Pathfinding 컴포넌트를 설정합니다.
    // baseTransform: 목표 기지 Transform
    // pathfindingComponent: Pathfinding 컴포넌트 참조
    public void Initialize(Transform baseTransform, Pathfinding pathfindingComponent)
    {
        targetBase = baseTransform; // 목표 기지 설정
        pathfinding = pathfindingComponent; // Pathfinding 컴포넌트 설정

        // Pathfinding과 TargetBase가 정상적으로 초기화되었는지 확인
        if (pathfinding != null && targetBase != null)
        {
            // A* 알고리즘을 사용하여 경로를 계산
            path = pathfinding.FindPath(transform.position, targetBase.position);
            pathIndex = 0; // 경로의 시작 인덱스를 0으로 설정
        }
        else
        {
            // 초기화 실패 시 오류 메시지 출력
            Debug.LogError("Pathfinding or TargetBase is not properly initialized in Enemy.");
        }
    }

    // 매 프레임마다 호출되어 적을 이동시키는 메서드
    private void Update()
    {
        // 경로가 존재하고, 아직 경로를 따라가는 중인지 확인
        if (path != null && pathIndex < path.Count)
        {
            // 현재 이동해야 할 노드의 위치
            Vector3 targetPos = path[pathIndex].worldPosition;

            // MoveTowards를 사용하여 현재 위치에서 목표 위치로 이동
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            // 목표 위치에 도달하면 다음 노드로 이동
            if (Vector3.Distance(transform.position, targetPos) < 0.1f)
            {
                pathIndex++; // 다음 노드로 이동
            }
        }
    }
}
