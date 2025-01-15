using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
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
    public TextMeshProUGUI nextStageText;
    public Camera mainCamera; // 메인 카메라 참조
    public Transform sheepTransform;

    [Header("Sound")]
    public AudioClip jumpSound; // 점프 효과음
    private AudioSource audioSource;

    private bool isGameOver = false;
    private float startTime; // 게임 시작 시간
    private float playTime; //게임 종료 시간
    private float totalDistance;    // 총 이동거리

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
        if (nextStageText != null)
        {
            nextStageText.gameObject.SetActive(false);
        }
        // 메인 카메라 자동 설정
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        startTime = Time.time;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (isGameOver) return;
        if (audioManager != null)
        {
            // 소리 크기를 가져옴
            amplitude = audioManager.GetJumpAmplitude();

            // 소리 크기가 특정 임계값 이상일 때 점프 amplitude > 0.1f
            if (isGrounded && amplitude > 0.03f)
            {
                PlayCollisionSound();
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

        CheckOutOfBounds();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 땅과 충돌했을 때만 isGrounded를 true로 변경
        if (collision.gameObject.tag == "Ground" || collision.gameObject.name == "firstGround")
        {
            isGrounded = true; // 땅에 닿음
            backgroundScroll.StopScroll(); // 배경 이동 멈춤
        }

        //장애물 충돌
        if (collision.gameObject.name == "sharp" || collision.gameObject.tag == "spin")
        {
            if(sheepTransform.position.x > -1.23f){
                totalDistance = sheepTransform.position.x + 1.23f;  //총 움직인 거리
                playTime = Time.time - startTime;   //총 시간
                //점수 저장
                int score = Mathf.RoundToInt(totalDistance / playTime * 100);
                PlayerPrefs.SetInt("Round1", score);
                PlayerPrefs.Save();

                int round1 = PlayerPrefs.GetInt("Round1");
                Debug.Log("round1 total score : " + round1);
                PlayerPrefs.Save();

                Debug.Log("장애물 충돌! 다음 스테이지로 이동...");
                StartNextStage();
            }
        }
    }
    private void CheckOutOfBounds()
    {
        Debug.Log("Checking if sheep is out of bounds...");
        // 양의 월드 좌표를 카메라 뷰포트 좌표로 변환
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);

        // 뷰포트 좌표가 카메라 밖으로 벗어났는지 체크
        if (viewportPos.y < -0.1f || viewportPos.y > 1.1f || viewportPos.x < -0.1f || viewportPos.x > 1.1f)
        {
            Debug.Log("Sheep is out of bounds!");
            //점수 저장
            totalDistance = sheepTransform.position.x + 1.23f;  //총 움직인 거리
            playTime = Time.time - startTime;   //총 시간
            int score = Mathf.RoundToInt(totalDistance / playTime);
            PlayerPrefs.SetInt("Round1", score);
            PlayerPrefs.Save();

            int round1 = PlayerPrefs.GetInt("Round1");
            Debug.Log("round1 total score : " + round1);
            PlayerPrefs.Save();

            Debug.Log("양이 카메라 밖으로 벗어났습니다!");
            StartNextStage(); // 게임 오버 처리
        }
    }

    void StartNextStage()
    {
        isGameOver = true;
        audioManager.audioSource.Stop();
        backgroundScroll.StopScroll();
        if (nextStageText != null)
        {
            nextStageText.gameObject.SetActive(true);
        }

        StartCoroutine(TransitionToNextScene());
    }

    System.Collections.IEnumerator TransitionToNextScene()
    {
        yield return new WaitForSeconds(4f); // 2초 대기
        SceneManager.LoadScene("R2_Intro"); // Scene2로 전환
    }

     void PlayCollisionSound()
    {
        // AudioClip이 설정되어 있으면 재생
        if (jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }
}
