using UnityEngine;

public class R2_DropSheeps : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject[] gameObjects;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 디버깅: 충돌한 객체의 이름 출력
        Debug.Log($"Collision detected with: {collision.gameObject.name}");

        if (collision.gameObject.name=="Hill")
        {
            Debug.Log("Collision with Hill detected.");
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // 디버깅: Rigidbody 상태 출력
                Debug.Log($"Before Collision - Velocity: {rb.linearVelocity}, IsKinematic: {rb.isKinematic}");

                rb.linearVelocity = Vector2.zero; // 떨어진 후 움직임 멈춤
                rb.isKinematic = true; // 물리 효과 제거
            // 디버깅: Rigidbody 상태 출력
                Debug.Log($"After Collision - Velocity: {rb.linearVelocity}, IsKinematic: {rb.isKinematic}");
            }
            else
            {
                Debug.LogWarning("Rigidbody2D not found on the GameObject.");
            }
        }
    }

}
