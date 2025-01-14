using UnityEngine;

public class R3_WolfHurt : MonoBehaviour
{
    [Header("References")]
    public Animator PlayerAnimator;
    private Rigidbody2D rb;

    [Header("Settings")]
    private bool isGrounded = true; // 늑대가 땅에 있는지 여부
    private float spinSpeed = 720f; // 회전 속도 (도/초)
    private float spinDuration = 2f; // 회전 지속 시간 (초)


    void Start()
    {
        if (PlayerAnimator == null)
        {
            Debug.LogError("PlayerAnimator is not assigned!");
        }
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        // 땅에서 떨어졌을 때 회전 효과 적용
        if (!isGrounded && rb.linearVelocity.y < 0) // 아래로 떨어질 때만 회전 시작
        {
            transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime); // 빙글빙글 회전
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"OnTriggerEnter2D called with collision: {collision.gameObject.name}");

        if (collision.gameObject.CompareTag("Leaf") || collision.gameObject.CompareTag("Rock") || collision.gameObject.CompareTag("Tree"))
        {
            Debug.Log("Triggering hurt animation");
            PlayerAnimator.SetBool("hurt", true);
            Invoke("ResetHurtAnimation", 0.36f);
        }
    }

    public void ResetHurtAnimation()
    {
        PlayerAnimator.SetBool("hurt", false);
    }

    // 게임에서 탈락 시
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "wolf_ground")
        {
            Debug.Log("Wolf has left the ground!");
            isGrounded = false;
        }
    }
}