using UnityEngine;
using UnityEngine.Audio;
using System.Collections;


public class AudioManager : MonoBehaviour
{
    [Header("Settings")]
    public AudioSource audioSource;
    private const int sampleWindow = 1024;
    private bool isMicrophoneReady = false;

    void Start(){
        // AudioSource를 동적으로 추가
        if (audioSource == null)
        {
            Debug.Log("AudioSource is null. Adding dynamically...");
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.mute = false;
        }
        if (!Application.HasUserAuthorization(UserAuthorization.Microphone))
        {
            StartCoroutine(RequestMicrophonePermission());
        }
        else
        {
            InitializeMicrophone();
        }
        
    }

    private IEnumerator RequestMicrophonePermission()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.Microphone);
        if (!Application.HasUserAuthorization(UserAuthorization.Microphone))
        {
            Debug.LogError("Microphone permission denied!");
            yield break;
        }
        InitializeMicrophone();
    }

    public void InitializeMicrophone()
    {
        if (Microphone.devices.Length > 0)
        {
            string micName = Microphone.devices[0];
            Debug.Log($"Microphone detected: {micName}");
            
            audioSource.clip = Microphone.Start(micName, true, 10, 44100);
            audioSource.loop = true;

            StartCoroutine(WaitForMicrophone(micName)); // 코루틴 시작
        }
        else
        {
            Debug.LogError("No Microphone detected!");
        }
    }

    private IEnumerator WaitForMicrophone(string micName)
    {
        while (!(Microphone.GetPosition(micName) > 0))
        {
            Debug.Log("Waiting for microphone...");
            yield return null; // 다음 프레임까지 대기
        }
        Debug.Log("Microphone is ready.");
        audioSource.Play(); // 마이크 데이터 재생 시작

        if (audioSource.isPlaying)
        {
            Debug.Log("AudioSource is playing..");
            isMicrophoneReady = true;
        }
        else
        {
            Debug.LogError("AudioSource is not playing!");
        }
    }

    public float GetPitch()
    {
        if (!isMicrophoneReady || audioSource.clip == null)
        {
            Debug.LogWarning("Microphone not ready or AudioSource.clip is null.");
            return 0f;
        }

        float[] spectrumData = new float[sampleWindow];
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);
        Debug.Log($"Spectrum Data: {string.Join(", ", spectrumData)}");

        float maxFreq = 0f;
        float maxAmplitude = 0f;

        for (int i = 0; i < spectrumData.Length; i++)
        {
            if (spectrumData[i] > maxAmplitude && spectrumData[i] > 0.01f)
            {
                maxAmplitude = spectrumData[i];
                maxFreq = i * (AudioSettings.outputSampleRate / 2f) / spectrumData.Length;
            }
        }
        Debug.Log($"Get Pitch - maxFreq: {maxFreq}");
        return maxFreq;
    }
}
