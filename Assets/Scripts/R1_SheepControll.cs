using UnityEngine;

public class R1_SheepControll : MonoBehaviour
{
    [Header("Settings")]
    public float jumpForce; // 점프 힘
    public bool isGrounded = true; // 땅에 닿아있는 상태

    [Header("References")]
    public Rigidbody2D rb; // Rigidbody2D 참조
    public R1_BackgroundScroll backgroundScroll; // 배경 스크롤 스크립트 참조

    void Update()
    {
        // 스페이스바를 눌렀을 때 점프
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // 위쪽으로 점프
            isGrounded = false; // 공중에 있음
            backgroundScroll.StartScroll(); // 배경 이동 시작
        }

        // 공중에 있을 때 배경 이동 업데이트
        if (!isGrounded)
        {
            backgroundScroll.UpdateScroll(rb.linearVelocity.y); // 속도 기반으로 배경 이동
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 땅과 충돌했을 때만 isGrounded를 true로 변경
        if (collision.gameObject.name == "GroundSpawner" || collision.gameObject.name == "firstGround")
        {
            isGrounded = true; // 땅에 닿음
            backgroundScroll.StopScroll(); // 배경 이동 멈춤
        }
    }
}
