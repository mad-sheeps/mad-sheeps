using UnityEngine;

public class R1_SheepControll : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed; // 이동 속도
    public float jumpForce; // 점프 힘
    private bool isGrounded = true; // 땅에 닿아있는 상태

    [Header("References")]
    public Rigidbody2D rb; // Rigidbody2D 참조

    void Update()
    {
        // 오른쪽 이동
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

    // 땅에 닿았는지 확인
    // TODO: 땅에 안닿으면(바다에 빠지면) 생길 이벤트 필요
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            isGrounded = true;
        }
    }
}
