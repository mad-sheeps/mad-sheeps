using UnityEngine;

public class R1_GroundSpawner : MonoBehaviour
{
    [Header("Settings")]
    public float spawnDistance = 15f; // 땅 생성 거리
    public float destroyDistance = 20f; // 땅 제거 거리

    [Header("References")]
    public GameObject[] groundPrefabs; // 땅 프리팹 배열
    public Transform player; // 플레이어

    private Vector3 lastSpawnPosition;

    void Start()
    {
        lastSpawnPosition = new Vector3(0, -5.02f, 0);
        SpawnNextGround();
    }

    void Update()
    {
        // 땅 생성
        if (Vector3.Distance(player.position, lastSpawnPosition) < spawnDistance)
        {
            SpawnNextGround();
        }
    }

    void SpawnNextGround()
    {
        GameObject ground = Instantiate(groundPrefabs[Random.Range(0, groundPrefabs.Length)], transform);
        ground.transform.position = lastSpawnPosition + new Vector3(Random.Range(3f, 4f), 0, 0);
        lastSpawnPosition = ground.transform.position;
    }
}
