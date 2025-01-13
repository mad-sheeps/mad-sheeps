using UnityEngine;

public class R1_BackgroundScroll : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 5f; // 배경 이동 속도

    [Header("References")]
    public GameObject[] groundObjects;
    public GameObject waterObject;
    public GameObject skyObject;
    public Transform player; // 양

    private Vector3 lastPlayerPosition; // 양의 이전 위치

    void Start()
    {
        // 초기 양 위치
        lastPlayerPosition = player.position;
    }

    void Update()
    {
        // 양의 현재 위치와 이전 위치 차이 계산
        Vector3 playerDelta = player.position - lastPlayerPosition;
        Vector3 backgroundMovement = new Vector3(-playerDelta.x, 0, 0);

        // // 배경 이동 적용
        // foreach (GameObject ground in groundObjects)
        // {
        //     ground.transform.Translate(backgroundMovement, Space.World);
        // }

        waterObject.transform.Translate(backgroundMovement, Space.World);
        skyObject.transform.Translate(backgroundMovement, Space.World);

        lastPlayerPosition = player.position;
    }
}
