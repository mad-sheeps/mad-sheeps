using UnityEngine;

public class R2_Sheeps : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Settings")]
    public AudioClip micInput; // 마이크에서 입력받은 오디오
    private AudioSource aud;
    private bool isMicInitialized = false;
    private const int sampleWindow = 1024; // 샘플 크기
    private float previousPitch = 0f; // 이전 음 높이

    [Header("References")]
    public GameObject[] gameObjects;

    void Start()
    {
        //InitializeMicrophone();
        Debug.Log($"게임 시작!");
        DropRandomSheep();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log($"스페이스바 눌러짐 ^.^");
             DropRandomSheep();
        }

        // if (isMicInitialized)
        // {
        //     float pitch = GetPitch();
        //     // 디버깅 로그 출력
        //     Debug.Log($"Detected Pitch: {pitch:F2}");
        //     Debug.Log($"Previous Pitch: {previousPitch:F2}");

        //     if (pitch > previousPitch + 10) // 이전 음보다 10Hz 높은 경우
        //     {
        //         DropRandomSheep();
        //         previousPitch = pitch; // 새로운 음 높이로 업데이트
        //     }
        // }
    }

        void InitializeMicrophone()
    {
        if (Microphone.devices.Length > 0)
        {
            Debug.Log($"Microphone detected: {Microphone.devices[0]}");
            micInput = Microphone.Start(Microphone.devices[0].ToString(), true, 3, 44100);
            isMicInitialized = true;
        }
        else
        {
            Debug.LogError("No Microphone detected!");
        }
    }

    float GetPitch()
    {
        float[] data = new float[sampleWindow];
        micInput.GetData(data, 0);

        float sum = 0f;
        for (int i = 0; i < sampleWindow; i++)
        {
            sum += data[i] * data[i];
        }

        float rmsValue = Mathf.Sqrt(sum / sampleWindow); // Root Mean Square 계산
        Debug.Log($"RMS Value: {rmsValue:F6}");

        // 간단한 피치 계산
        float pitch = Mathf.Log10(rmsValue + 1) * 1000; // 간단한 음 높이 추정
        Debug.Log($"Calculated Pitch: {pitch:F2}");

        return pitch;
    }
    void DropRandomSheep()
    {

        GameObject sheep = gameObjects[Random.Range(0, gameObjects.Length)];
        if (sheep != null)
        {
            Debug.Log("Dropping a sheep!");
            Instantiate(sheep, transform.position, Quaternion.identity);
        }else
        {
            Debug.LogWarning("No sheep object found to drop.");
        }
    }

    void OnDestroy()
    {
        if (isMicInitialized)
        {
            Microphone.End(null); // 마이크 종료
        }
    }
}

