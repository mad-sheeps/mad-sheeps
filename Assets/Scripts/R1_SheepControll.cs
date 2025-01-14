using UnityEngine;

public class R1_SheepControll : MonoBehaviour
{
    [Header("Settings")]
    public float jumpForceMultiplier = 10000f; // 점프 힘 증가
    public bool isGrounded = true; // 땅에 닿아있는 상태
    public float amplitude;

    [Header("References")]
    public Rigidbody2D rb; // Rigidbody2D 참조
    public R1_BackgroundScroll backgroundScroll;
    public AudioManager audioManager;

    void Start()
    {
        // AudioManager가 연결되지 않았다면 자동으로 찾기
        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
            if (audioManager == null)
            {
                Debug.LogError("AudioManager not found in the scene!");
            }
        }
    }

    void Update()
    {
        if (audioManager != null)
        {
            // 소리 크기를 가져옴
            amplitude = audioManager.GetJumpAmplitude();

            // 소리 크기가 특정 임계값 이상일 때 점프
            if (isGrounded && amplitude > 0.1f)
            {
                float jumpForce = amplitude * jumpForceMultiplier; // 소리 크기에 비례하여 점프 힘 계산
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // 위쪽으로 점프
                isGrounded = false; // 공중 상태로 변경
                backgroundScroll.StartScroll(); // 배경 이동 시작
                Debug.Log($"Jump triggered with amplitude: {amplitude}, force: {jumpForce}");
            }
        }
        //공중에 있을 때 배경 이동 업데이트
        if (!isGrounded)
        {
            backgroundScroll.UpdateScroll(rb.linearVelocity.y); // 속도 기반으로 배경 이동
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 땅과 충돌했을 때만 isGrounded를 true로 변경
        if (collision.gameObject.name == "GroundSpawner" || collision.gameObject.name == "firstGround")
        {
            isGrounded = true; // 땅에 닿음
            backgroundScroll.StopScroll(); // 배경 이동 멈춤
        }

        //sharp랑 충돌
        if (collision.gameObject.name == "sharp")
        {
            Debug.Log("sharp 충돌! 게임 종료");
        }

        //spin이랑 충돌
        if (collision.gameObject.tag == "spin")
        {
            Debug.Log("spin 충돌! 게임 종료");
        }
    }
}
