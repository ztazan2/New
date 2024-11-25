using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // 생성할 적 프리팹
    public Transform targetBase;   // 적이 이동할 목표 기지의 Transform
    public float spawnInterval = 3.0f; // 적 생성 간격 (초 단위)

    private Vector3 spawnPosition = new Vector3(-12f, 1f, -9f); // 적의 고정 생성 위치
    private Pathfinding pathfinding; // A* 경로 탐색을 위한 Pathfinding 스크립트 참조

    private void Start()
    {
        // Pathfinding 스크립트 초기화
        pathfinding = FindObjectOfType<Pathfinding>();
        if (pathfinding == null)
        {
            // Pathfinding 스크립트가 없으면 오류 출력
            Debug.LogError("씬에 Pathfinding 스크립트가 없습니다. 적절한 GameObject에 Pathfinding 스크립트를 추가하세요.");
            return;
        }

        // 일정한 시간 간격으로 적 생성
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab != null && targetBase != null)
        {
            // 적 프리팹을 생성
            GameObject enemyObj = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            Enemy enemyScript = enemyObj.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                // 적 초기화: 목표 기지와 Pathfinding 전달
                enemyScript.Initialize(targetBase, pathfinding);
            }
        }
        else
        {
            // 적 프리팹이나 목표 기지가 설정되지 않았을 경우 오류 출력
            Debug.LogError("EnemyPrefab 또는 TargetBase가 EnemySpawner에 할당되지 않았습니다.");
        }
    }
}
