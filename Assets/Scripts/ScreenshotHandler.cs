using UnityEngine;

public class ScreenshotHandler : MonoBehaviour
{
    public void CaptureAndSaveScreenshot()
    {
        string fileName = "Screenshot_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        string filePath = System.IO.Path.Combine(Application.persistentDataPath, fileName);

        // 스크린샷 저장
        ScreenCapture.CaptureScreenshot(fileName);
        Debug.Log($"스크린샷 저장 완료: {filePath}");

        // 파일 경로를 공유
        StartCoroutine(ShareScreenshot(filePath));
    }

    System.Collections.IEnumerator ShareScreenshot(string filePath)
    {
        // 파일 저장 대기 (스크린샷 저장은 비동기적으로 처리됨)
        yield return new WaitForSeconds(1f);

        Debug.Log($"공유 준비된 파일 경로: {filePath}");
        Share(filePath);
    }

    void Share(string filePath)
    {
        // 플랫폼별 공유
        #if UNITY_ANDROID
        new NativeShare().AddFile(filePath).SetSubject("Check out my score!").SetText("I just got this score!").Share();
        #elif UNITY_IOS
        new NativeShare().AddFile(filePath).SetSubject("Check out my score!").SetText("I just got this score!").Share();
        #else
        Debug.Log("공유 기능은 Android 및 iOS에서만 지원됩니다.");
        #endif
    }
}