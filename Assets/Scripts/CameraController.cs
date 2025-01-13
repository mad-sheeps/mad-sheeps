using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Settings")]
    public Transform target; // 따라갈 대상(양)
    public float smoothSpeed; // 카메라 이동 속도
    public Vector3 offset; // 카메라의 위치 오프셋

    void LateUpdate()
    {
        if (target != null)
        {
            // 목표 위치 계산
            Vector3 targetPosition = new Vector3(transform.position.x, target.position.y + offset.y, transform.position.z);
            //Debug.Log($"타겟 위치: {targetPosition}");
            // 카메라를 부드럽게 이동
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }


    }
}
