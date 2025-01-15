using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    [Header("Settings")]
    public GameObject[] sheepPrefabs; // 양 프리팹 배열
    public Transform spawnPoint; // 양 생성 위치
    public float spawnInterval = 0.5f; // 양 생성 간격
    public float sheepSpeed = 2f; // 양 이동 속도
    public float jumpForce = 3f; // 양 점프 힘

    private float nextSpawnTime = 0f;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnSheep();
            nextSpawnTime = Time.time + spawnInterval;
        }
        if (transform.position.x > 4f) // 화면 오른쪽 바깥으로 나가면 삭제
        {
            Destroy(gameObject);
        }
    }

    void SpawnSheep()
    {
        // 랜덤 양 생성
        GameObject randomSheep = sheepPrefabs[Random.Range(0, sheepPrefabs.Length)];
        GameObject spawnedSheep = Instantiate(randomSheep, spawnPoint.position, Quaternion.identity);

        // 양의 이동 및 점프 로직 추가
        Rigidbody2D rb = spawnedSheep.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0.5f; // 중력 설정
        spawnedSheep.AddComponent<SheepMovement>().Initialize(sheepSpeed, jumpForce);
    }
}
