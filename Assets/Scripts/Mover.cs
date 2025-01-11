using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed; // 아래로 떨어지는 속도
    public float rightMoveSpeed = 5f; // 오른쪽 이동 속도

    private bool isInsideBox = false; // 상자 내부에 있는지 여부
    private bool isMovingRight = false; // 오른쪽으로 이동 중인지 여부

    void Update()
    {
        if (!isMovingRight)
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.right * rightMoveSpeed * Time.deltaTime;
        }

        if (isInsideBox && Input.GetKeyDown(KeyCode.Space))
        {
            isMovingRight = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // "Box" 태그를 가진 상자와 충돌했을 때
        if (other.CompareTag("Box"))
        {
            isInsideBox = true;
        }

        // "Wolf" 태그를 가진 객체와 충돌했을 때
        if (other.CompareTag("Wolf"))
        {
            Destroy(gameObject);
        }
    }

    // 상자에서 나왔을 때
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Box"))
        {
            isInsideBox = false;
        }
    }
}