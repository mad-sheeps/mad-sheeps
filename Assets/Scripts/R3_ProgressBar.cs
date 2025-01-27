using UnityEngine;
using UnityEngine.SceneManagement;

public class R3_ProgressBar : MonoBehaviour
{
    [Header("References")]
    public RectTransform overlayRectTransform; // 흰색 오버레이 RectTransform
    public R3_WolfHurt wolf;
    private Rigidbody2D wolfRigidbody;
    public R3_Mover moveScript;

    [Header("Settings")]
    public float maxWidth = 214.83f; // 프로그래스 바의 최대 길이 (빨간 배경의 총 길이)
    private float currentOverlayWidth = 0f; // 흰색 오버레이의 현재 길이
    public Vector2 jumpForce = new Vector2(-10f, 10f);
    public bool isGameOver = false; // 게임 종료 상태


    [Header("Time")]
    private float startTime; // 게임 시작 시간
    private float playTime; //게임 종료 시간

    void Start()
    {
        if (overlayRectTransform == null)
        {
            Debug.LogError("Overlay RectTransform not assigned in Inspector!");
        }
        UpdateOverlay();

        wolf = Object.FindAnyObjectByType<R3_WolfHurt>();
        wolfRigidbody = wolf.GetComponent<Rigidbody2D>();

        startTime = Time.time;
    }

    // 흰색 오버레이 값 설정
    public void SetOverlayWidth(float progress)
    {
        if (isGameOver) return;
        // 프로그레스 값을 0~1로 제한
        float clampedProgress = Mathf.Clamp01(progress);
        currentOverlayWidth = maxWidth * clampedProgress; // 길이를 비율로 계산
        UpdateOverlay();

        if (currentOverlayWidth >= maxWidth)
        {
            int miss = moveScript.miss;
            playTime = Time.time - startTime;   //총 시간
            int score = Mathf.RoundToInt((100 - miss) / playTime * 50);
            PlayerPrefs.SetInt("Round3", score);
            PlayerPrefs.Save();

            int round3 = PlayerPrefs.GetInt("Round3");
            Debug.Log("round3 total score : " + round3);
            MoveWolf();
            //Invoke("LoadScoreScene", 1f); // 1.5초 뒤에 씬 전환
        }
    }

    // 흰색 오버레이 너비 증가
    public void IncreaseOverlayWidth(float amount)
    {
        // 현재 길이에 증가값 추가
        Debug.Log("현재 길이:" + currentOverlayWidth);
        SetOverlayWidth((currentOverlayWidth + amount) / maxWidth);
    }

    // 흰색 오버레이 업데이트
    private void UpdateOverlay()
    {
        if (overlayRectTransform != null)
        {
            // 흰색 오버레이의 너비 조정
            overlayRectTransform.sizeDelta = new Vector2(currentOverlayWidth, overlayRectTransform.sizeDelta.y);
        }
    }

    public void MoveWolf()
    {
        if (wolf != null)
        {   //늑대 날아감
            wolfRigidbody.AddForce(jumpForce, ForceMode2D.Impulse); 
        }

        Destroy(gameObject);
        GameObject otherProgressBar = GameObject.Find("progress_bar");
        if (otherProgressBar != null)
        {
            Destroy(otherProgressBar);
        }
    }

    private void LoadScoreScene()
    {
        Debug.Log("Scene change");
        SceneManager.LoadScene("Score_Scene"); // Score_Scene으로 전환
    }

    // 현재 프로그레스 값을 가져오기
    public float GetCurrentProgress()
    {
        return currentOverlayWidth / maxWidth; // 현재 길이를 비율로 반환
    }
}