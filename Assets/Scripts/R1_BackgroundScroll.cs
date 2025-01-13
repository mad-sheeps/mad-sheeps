using UnityEngine;

public class R1_BackgroundScroll : MonoBehaviour
{
    [Header("Settings")]
    public float scrollSpeedMultiplier = 0.5f; // 점프 속도에 따른 배경 이동 속도 비율

    [Header("References")]
    public GameObject[] groundObjects; // 땅 오브젝트 배열
    public GameObject waterObject; // 물 오브젝트
    public GameObject skyObject; // 하늘 오브젝트

    private bool isMoving = false; // 배경 이동 중인지 여부

    void Update()
    {
        if (isMoving)
        {
            // 배경 이동
            foreach (GameObject ground in groundObjects)
            {
                ground.transform.Translate(new Vector3(-scrollSpeedMultiplier * Time.deltaTime, 0, 0), Space.World);
            }
            waterObject.transform.Translate(new Vector3(-scrollSpeedMultiplier * Time.deltaTime, 0, 0), Space.World);
            skyObject.transform.Translate(new Vector3(-scrollSpeedMultiplier * Time.deltaTime, 0, 0), Space.World);
        }
    }

    public void StartScroll()
    {
        isMoving = true; // 배경 이동 시작
    }

    public void UpdateScroll(float verticalVelocity)
    {
        // 점프 속도에 따라 배경 이동 속도 조정
        if (verticalVelocity > 0 || verticalVelocity < 0) // 점프 중일 때만
        {
            scrollSpeedMultiplier = Mathf.Abs(verticalVelocity) * 0.2f; // 속도에 비례해 배경 이동
        }
    }

    public void StopScroll()
    {
        isMoving = false; // 배경 이동 멈춤
    }
}
