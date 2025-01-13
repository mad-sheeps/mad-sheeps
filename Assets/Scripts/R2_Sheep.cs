using UnityEngine;
using System.Collections.Generic;

public class R2_Sheeps : MonoBehaviour
{
    [Header("Settings")]
    public float previousPitch = 0f;
    public float pitch = 0f;
    private int sheepCount = 0;

    [Header("Cooldown Settings")]
    public float cooldownTime = 1f; // 양이 떨어지는 최소 간격 (초)
    private float lastDropTime = 0f;  // 마지막으로 양이 떨어진 시간 기록

    [Header("References")]
    public AudioManager audioManager; // AudioManager 연결
    public GameObject[] gameObjects;


    void Start()
    {
        if (audioManager != null) {
            audioManager.InitializeMicrophone();
            Debug.Log("AudioManager initialized successfully.");
        } else {
            Debug.LogError("AudioManager reference is missing!");
        }
    }

    void Update()
    {
        if (audioManager != null && audioManager.audioSource != null ) {
            pitch = audioManager.GetPitch();

            // Pitch가 유효한 범위에 있는지 확인
            if (pitch >0f) {
                // 처음 pitch를 초기화
                if (previousPitch == 0f){
                    previousPitch = pitch;
                    Debug.Log($"Initial Pitch Set: {previousPitch:F2}");
                }
                // 현재 pitch가 이전 pitch보다 높고, 쿨타임이 지났을 때
                else if (pitch > previousPitch && Time.time - lastDropTime >= cooldownTime) {
                    DropRandomSheep();
                    Debug.Log($"Sheep dropped for pitch: {pitch:F2}");
                    lastDropTime = Time.time; // 마지막 양 드롭 시간 갱신
                    previousPitch = pitch;   // 기준 pitch 업데이트
                }
            }
        } else {
            Debug.LogError("AudioManager reference is null.");
        }
    }

    void DropRandomSheep()
    {
        GameObject sheep = gameObjects[Random.Range(0, gameObjects.Length)];
        if (sheep != null)
        {
            float spawnYPosition = Camera.main.transform.position.y + 2.6f;
            Vector3 spawnPosition = new Vector3(transform.position.x, spawnYPosition, 0);

            GameObject newSheep = Instantiate(sheep, spawnPosition, Quaternion.identity);
            sheepCount++;

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
}
