using UnityEngine;

public class R2_Sheeps : MonoBehaviour
{
    [Header("Settings")]
    public float previousPitch = 0f;
    public float pitch;
    private int sheepCount = 0;

    [Header("References")]
    public AudioManager audioManager; // AudioManager 연결
    public GameObject[] gameObjects;

    void Start()
    {
        if (audioManager != null)
        {
            audioManager.InitializeMicrophone();
            Debug.Log("AudioManager initialized successfully.");
        }
        else
        {
            Debug.LogError("AudioManager reference is missing!");
        }
    }

    void Update()
    {
        if (audioManager != null)
        {
            // Pitch 값을 가져오기 전에 마이크 초기화 여부 확인
            if (audioManager.audioSource != null && audioManager.audioSource.isPlaying)
            {
                pitch = audioManager.GetPitch();
                Debug.Log($"R2_Sheeps - Current Pitch: {pitch:F2}");

                if (pitch > previousPitch + 5) 
                {
                    DropRandomSheep();
                    Debug.Log($"R2_Sheeps - Detected Pitch: {pitch:F2}");
                    previousPitch = pitch;
                }
            }
            else
            {
                Debug.LogWarning("AudioManager is not ready or audioSource is not playing.");
            }
        }
        else
        {
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
