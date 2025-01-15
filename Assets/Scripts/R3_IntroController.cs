using UnityEngine;
using UnityEngine.SceneManagement;

public class R3IntroController : MonoBehaviour
{
    [Header("Settings")]
    public float delayBeforeNextScene = 5f; // 다음 씬으로 넘어가기 전 대기 시간

    [Header("Next Scene")]
    public string nextSceneName = "Round3_Scene"; // 다음으로 전환할 씬 이름

    void Start()
    {
        // delayBeforeNextScene 초 후에 LoadNextScene 함수 호출
        Invoke("LoadNextScene", delayBeforeNextScene);
    }

    void LoadNextScene()
    {
        // 지정된 씬으로 전환
        SceneManager.LoadScene(nextSceneName);
    }
}
