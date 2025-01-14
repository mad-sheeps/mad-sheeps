using UnityEngine;

public class SheepMovement : MonoBehaviour
{
    private float moveSpeed;
    private float jumpForce;
    private bool isGrounded = true; // 양이 땅에 닿아있는지 확인

    public void Initialize(float speed, float force)
    {
        moveSpeed = speed;
        jumpForce = force;
    }

    void Update()
    {
        // 양이 오른쪽으로 이동
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

        // 점프 로직
        if (isGrounded && transform.position.x >= 0.15f) // 특정 위치에서 점프
        {
            Jump();
        }
    }

    void Jump()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false; // 공중 상태로 변경
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 땅과 충돌하면 점프 가능 상태로 변경
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
