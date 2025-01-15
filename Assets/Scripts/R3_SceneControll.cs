using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionController : MonoBehaviour
{
    [Header("References")]
    public GameObject wolf; // 늑대 오브젝트
    public Camera mainCamera; // 메인 카메라
    public string nextSceneName = "Score_Scene"; // 전환할 씬 이름
    public float delayBeforeSceneChange = 1.5f; // 씬 전환 전 대기 시간

    private bool isSceneChangeTriggered = false; // 씬 전환 중복 방지 플래그

    void Update()
    {
        if (wolf == null || isSceneChangeTriggered) return;

        // 늑대가 카메라 밖으로 나갔는지 확인
        Vector3 wolfViewportPos = mainCamera.WorldToViewportPoint(wolf.transform.position);
        if (wolfViewportPos.y < 0 || wolfViewportPos.y > 1 || wolfViewportPos.x < 0 || wolfViewportPos.x > 1)
        {
            Debug.Log("Wolf has left the camera view. Triggering scene change...");
            isSceneChangeTriggered = true; // 씬 전환 중복 방지
            Invoke("LoadNextScene", delayBeforeSceneChange);
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName); // 씬 전환
    }
}
