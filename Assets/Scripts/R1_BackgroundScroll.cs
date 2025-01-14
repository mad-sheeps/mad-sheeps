using UnityEngine;

public class R1_BackgroundScroll : MonoBehaviour
{
    [Header("Settings")]
    public float baseScrollSpeed = 3f; // 기본 배경 이동 속도
    public float maxScrollSpeedMultiplier = 2f; // 최대 배경 속도 비율
    public bool isMoving = false; // 배경 이동 중인지 여부
    public float currentScrollSpeed; // 현재 배경 이동 속도

    [Header("References")]
    //public GameObject[] groundObjects; // 땅 오브젝트 배열
    public Material waterMaterial; // 물 오브젝트
    public Material skyMaterial; // 하늘 오브젝트

    private Vector2 skyOffset;
    private Vector2 waterOffset;


    void Update()
    {
        if (isMoving)
        {
            // 배경 이동
            // foreach (GameObject ground in groundObjects)
            // {
            //     ground.transform.Translate(new Vector3(-currentScrollSpeed * Time.deltaTime, 0, 0), Space.World);
            // }
            
            skyOffset.x += currentScrollSpeed * Time.deltaTime * 0.1f;
            skyMaterial.mainTextureOffset = skyOffset;

            waterOffset.x += currentScrollSpeed * Time.deltaTime * 0.1f;
            waterMaterial.mainTextureOffset = skyOffset;
        }
    }

    public void StartScroll()
    {
        isMoving = true; // 배경 이동 시작
        currentScrollSpeed = baseScrollSpeed; // 초기 속도 설정
    }

    public void UpdateScroll(float verticalVelocity)
    {
        if (isMoving)
        {
            // 점프 속도에 따라 배경 이동 속도 조정
            currentScrollSpeed = baseScrollSpeed + Mathf.Abs(verticalVelocity) * maxScrollSpeedMultiplier;
        }
    }

    public void StopScroll()
    {
        isMoving = false; // 배경 이동 멈춤
    }
    public float GetCurrentScrollSpeed()
    {
        return isMoving ? currentScrollSpeed : 0f; // 현재 이동 중일 경우 속도를 반환
    }
}
