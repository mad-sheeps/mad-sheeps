using UnityEngine;

public class R1_GroundSpawner : MonoBehaviour
{
    [Header("Settings")]
    public float spawnDistance = 15f; // 땅 생성 거리
    public float destroyDistance = 20f; // 땅 제거 거리
    public float spawnInterval = 2f;
    [Header("References")]
    public GameObject[] groundPrefabs; // 땅 프리팹 배열
    public Transform player; // 플레이어
    public Transform spawnPoint;
    public R1_BackgroundScroll backgroundScroll;

    //private Vector3 lastSpawnPosition;

    void Start()
    {
        //lastSpawnPosition = new Vector3(0, -5.02f, 0);
        //nextSpawnX = Camera.main.transform.position.x + spawnDistance;
        
        StartCoroutine(SpawnGroundsPeriodically());
    }

    void Update()
    {
        DestroyOldGround();
    }

    private System.Collections.IEnumerator SpawnGroundsPeriodically()
    {
        while (true)
        {
            SpawnNextGround(); // 땅 생성
            yield return new WaitForSeconds(spawnInterval); // 지정된 시간만큼 대기
        }
    }

    void SpawnNextGround()
    {
        GameObject ground = Instantiate(
            groundPrefabs[Random.Range(0, groundPrefabs.Length)],
            spawnPoint.position,
            Quaternion.identity,
            transform
        );

        R1_ScrollableObject scrollable = ground.AddComponent<R1_ScrollableObject>();
        scrollable.SetScrollSpeed(backgroundScroll);
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
