using UnityEngine;

public class R1_GroundSpawner : MonoBehaviour
{
    [Header("Settings")]
    public float groundSpawnInterval = 7f; // 땅 간격 (가로 길이)
    public float destroyDistance = 20f; // 땅 제거 거리
    //public float spawnInterval = 2f;
    [Header("References")]
    public GameObject[] groundPrefabs; // 땅 프리팹 배열
    private float nextSpawnX= 1.55f;
    public Transform player; // 플레이어

    //private Vector3 lastSpawnPosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // 초기 땅 5개 생성
        for (int i = 0; i < 5; i++)
        {
            SpawnNextGround();
        }
    }

    void Update()
    {
        // 땅 생성 조건 확인 (플레이어 위치 기준 앞으로 일정 거리 유지)
        while (nextSpawnX < player.position.x + groundSpawnInterval * 5f)
        {
            SpawnNextGround();
        }
        DestroyOldGround();
    }

    // private System.Collections.IEnumerator SpawnGroundsPeriodically()
    // {
    //     while (true)
    //     {
    //         SpawnNextGround(); // 땅 생성
    //         yield return new WaitForSeconds(spawnInterval); // 지정된 시간만큼 대기
    //     }
    // }

    void SpawnNextGround()
    {
        GameObject ground = Instantiate(groundPrefabs[Random.Range(0, groundPrefabs.Length)], transform);
        ground.transform.position = new Vector3(nextSpawnX, -5.02f, 0);
        nextSpawnX += groundSpawnInterval;
    }
    void DestroyOldGround()
    {
        foreach (Transform child in transform)
        {
            // 장애물이 카메라 뒤쪽으로 일정 거리 벗어났을 경우 제거
            if (child.position.x < Camera.main.transform.position.x - destroyDistance)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
