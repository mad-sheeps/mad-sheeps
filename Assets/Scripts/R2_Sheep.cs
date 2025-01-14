using UnityEngine;
using System.Collections.Generic;
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

    [Header("Game Over Settings")]
    public Transform ground;


    void Start()
    {
        if (audioManager != null) {
            audioManager.InitializeMicrophone();
            Debug.Log("AudioManager initialized successfully.");
        } else {
            Debug.LogError("AudioManager reference is missing!");
        }

        UpdateSheepCountText();
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
                    //Debug.Log($"Sheep dropped for pitch: {pitch:F2}");
                    lastDropTime = Time.time; // 마지막 양 드롭 시간 갱신
                    previousPitch = pitch;   // 기준 pitch 업데이트
                } else if(pitch < previousPitch && Time.time - lastDropTime >= cooldownTime){
                    Debug.Log($"pitchlife: {pitchlife:F2}");
                    pitchlife -= 1;
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
            } else if (pitchDelta>=200f){
                spawnXPosition = 0f;
            } else if (pitchDelta>=100){
                spawnXPosition = 0.4f;
            } else if (pitchDelta>= 50f){
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
                GameOver();
                break;
            }
        }
    }

    void GameOver()
    {
        // Round3_Scene으로 전환
        Debug.Log("Transitioning to Game Over scene...");
        SceneManager.LoadScene("Round3_Scene");
    }
}
