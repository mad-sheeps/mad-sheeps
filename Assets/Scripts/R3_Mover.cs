using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed;
    private bool isInsideBox = false; 
    private bool isMovingRight = false;
    private Animator SheepAnimator;  // 양 애니메이션

    void Start()
    {
        SheepAnimator = Object.FindAnyObjectByType<R3_Sheep>()?.GetComponent<Animator>();
    }

    void Update()
    {
        if (!isMovingRight)
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.right * 4 * Time.deltaTime;
            transform.position += Vector3.up * 9 * Time.deltaTime;
        }

        if (isInsideBox && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("스페이스 눌림!");
            isMovingRight = true;
            TriggerSheepAnimation();

            transform.position = new Vector2(-1.645f, -1.507f); //위치 이동
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Box"))
        {
            Debug.Log("충돌함!");
            isInsideBox = true;
        }

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

    //양 애니메이션
    void TriggerSheepAnimation()
    {
        string myTag = gameObject.tag;

        if (myTag == "Rock")
        {
            Debug.Log("돌 양 애니메이션 실행!");
            SheepAnimator.SetTrigger("RockSheep");
        }
        else if (myTag == "Leaf")
        {
            Debug.Log("나뭇잎 양 애니메이션 실행!");
            SheepAnimator.SetTrigger("LeafSheep");
        }
        else if (myTag == "Tree")
        {
            Debug.Log("나무 양 애니메이션 실행!");
            SheepAnimator.SetTrigger("TreeSheep");
        }
        else
        {
            Debug.LogWarning("무기의 태그가 정의되지 않았습니다!");
        }
    }
}