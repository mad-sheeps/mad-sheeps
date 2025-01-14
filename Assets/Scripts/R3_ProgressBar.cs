using UnityEngine;

public class R3_ProgressBar : MonoBehaviour
{
    [Header("References")]
    public RectTransform overlayRectTransform; // 흰색 오버레이 RectTransform

    [Header("Settings")]
    public float maxWidth = 214.83f; // 프로그래스 바의 최대 길이 (빨간 배경의 총 길이)

    private float currentOverlayWidth = 0f; // 흰색 오버레이의 현재 길이

    void Start()
    {
        if (overlayRectTransform == null)
        {
            Debug.LogError("Overlay RectTransform not assigned in Inspector!");
        }
        UpdateOverlay();
    }

    // 흰색 오버레이 값 설정
    public void SetOverlayWidth(float progress)
    {
        // 프로그레스 값을 0~1로 제한
        float clampedProgress = Mathf.Clamp01(progress);
        currentOverlayWidth = maxWidth * clampedProgress; // 길이를 비율로 계산
        UpdateOverlay();
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

    // 현재 프로그레스 값을 가져오기
    public float GetCurrentProgress()
    {
        return currentOverlayWidth / maxWidth; // 현재 길이를 비율로 반환
    }
}