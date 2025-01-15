using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayButtonHandler : MonoBehaviour
{
    public ScreenshotHandler screenshotHandler;

    public void LoadRound2Scene()
    {
        SceneManager.LoadScene("R1_Intro"); 
    }
    public void LoadMainScene()
    {
        SceneManager.LoadScene("Main_Scene"); 
    }
    public void LoadScreenShare()
    {
        if (screenshotHandler != null)
        {
            screenshotHandler.CaptureAndSaveScreenshot(); // 스크린샷 캡처 및 공유 호출
        }
    }
}
