using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;


public class R2_Sheeps : MonoBehaviour
{
    [Header("Settings")]
    public float previousPitch = 0f;
    public float pitch = 0f;
    public int sheepCount = 0;
    public int pitchlife = 3;

    [Header("Cooldown Settings")]
    public float cooldownTime = 1f; // 양이 떨어지는 최소 간격 (초)
    private float lastDropTime = 0f;  // 마지막으로 양이 떨어진 시간 기록

    [Header("References")]
    public AudioManager audioManager; // AudioManager 연결
    public GameObject[] gameObjects;
    public TextMeshProUGUI sheepCountText;
    public TextMeshProUGUI gameOverText; // 게임 오버 텍스트

    [Header("Heart")]
    public GameObject[] hearts; 
    public Sprite heartOnSprite;
    public Sprite heartOffSprite;

    [Header("Sound")]
    public AudioClip collisionSound; // 충돌 효과음
    private AudioSource audioSource;

    void Start()
    {
        if (audioManager != null) {
            audioManager.InitializeMicrophone();
            Debug.Log("AudioManager initialized successfully.");
        } else {
            Debug.LogError("AudioManager reference is missing!");
        }

        UpdateSheepCountText();
        UpdateHearts();
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (audioManager != null && audioManager.audioSource != null ) {
            pitch = audioManager.GetPitch();

            // Pitch가 유효한 범위에 있는지 확인
            if (pitch > 0f) {
                // 처음 pitch를 초기화
                if (previousPitch == 0f){
                    previousPitch = pitch;
                    DropRandomSheep();
                    lastDropTime = Time.time;
                }
                // 현재 pitch가 이전 pitch보다 높고, 쿨타임이 지났을 때
                else if (pitch > previousPitch && Time.time - lastDropTime >= cooldownTime) {
                    DropRandomSheep();
                    lastDropTime = Time.time; // 마지막 양 드롭 시간 갱신
                    previousPitch = pitch;   // 기준 pitch 업데이트
                } else if(pitch < previousPitch && Time.time - lastDropTime >= cooldownTime){
                    Debug.Log($"pitchlife: {pitchlife:F2}");
                    pitchlife -= 1;
                    UpdateHearts(); 
                    DropRandomSheep();
                    lastDropTime = Time.time; // 마지막 양 드롭 시간 갱신
                    previousPitch = pitch;   // 기준 pitch 업데이트
                }
            }
        } else {
            Debug.LogError("AudioManager reference is null.");
        }
        UpdateSheepCountText();
        CheckGameOver();
        if(pitchlife == 0){
            GameOver();
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            SpriteRenderer heartRenderer = hearts[i].GetComponent<SpriteRenderer>();
            if (i < pitchlife)
            {
                heartRenderer.sprite = heartOnSprite;
            }
            else
            {
                heartRenderer.sprite = heartOffSprite;
            }
        }
    }

    void DropRandomSheep()
    {
        GameObject sheep = gameObjects[Random.Range(0, gameObjects.Length)];
        if (sheep != null)
        {
            float spawnYPosition = Camera.main.transform.position.y + 2.6f;

            // 피치 변화량 계산
            float pitchDelta = pitch - previousPitch; // 현재 피치 - 이전 피치
            Debug.Log($"pitchDelta: {pitchDelta:F2}");
            // x좌표 계산 (조건에 따라 변화)
            float spawnXPosition = 0f;
            if(sheepCount==0){
                spawnXPosition = 0f;
            } else if (pitchDelta>=100f){
                spawnXPosition = 0f;
            } else if (pitchDelta>=50){
                spawnXPosition = 0.4f;
            } else if (pitchDelta>= 30f){
                spawnXPosition = 0.6f;
            } else if (pitchlife != 3){
                spawnXPosition = -0.4f;
            }

            // 양 생성 위치 계산
            Vector3 spawnPosition = new Vector3(spawnXPosition, spawnYPosition, 0);
            GameObject newSheep = Instantiate(sheep, spawnPosition, Quaternion.identity);

            sheepCount++;
            UpdateSheepCountText(); // 텍스트 업데이트

            if (sheepCount >= 3)
            {
                CameraController cameraController = Camera.main.GetComponent<CameraController>();
                if (cameraController != null)
                {
                    cameraController.target = newSheep.transform;
                }
            }

            PlayCollisionSound();
        }
    }

    void UpdateSheepCountText()
    {
        if (sheepCountText != null)
        {
            sheepCountText.text = $"양 {sheepCount} 마리";
        }
    }

    void CheckGameOver()
    {
        // 모든 양 오브젝트를 가져옴
        GameObject[] allSheeps = GameObject.FindGameObjectsWithTag("Sheep");
        foreach (GameObject sheep in allSheeps)
        {
            // 양의 월드 좌표를 뷰포트 좌표로 변환
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(sheep.transform.position);

            // 뷰포트 좌표가 0~1 범위를 벗어나면 카메라 밖
            if (viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1)
            {
                Debug.Log("Game Over! Sheep went out of camera bounds.");
                PlayerPrefs.SetInt("Round2", sheepCount);
                PlayerPrefs.Save();

                int round2 = PlayerPrefs.GetInt("Round2");
                Debug.Log("round2 total score : " + round2);
                GameOver();
                break;
            }
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        audioManager.audioSource.Stop();

        // 게임 오버 텍스트 표시
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
        }

        // 일정 시간 후 씬 전환
        StartCoroutine(GameOverTransition());
    }

    IEnumerator GameOverTransition()
    {
        yield return new WaitForSeconds(4f); // 2초 대기
        SceneManager.LoadScene("Round3_Scene");
    }

    void PlayCollisionSound()
    {
        // AudioClip이 설정되어 있으면 코루틴 호출
        if (collisionSound != null)
        {
            StartCoroutine(PlaySoundWithDelay(0.6f)); // 0.5초 딜레이 후 재생
        }
    }

    IEnumerator PlaySoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 0.5초 대기
        audioSource.PlayOneShot(collisionSound);
    }

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 오브젝트가 sheep 태그를 가지고 있는지 확인
        if (collision.gameObject.CompareTag("sheep")) // 태그 대소문자 확인
        {
            Debug.Log("효과음 재생!!!!");
            PlayCollisionSound();
        }
    }

}
