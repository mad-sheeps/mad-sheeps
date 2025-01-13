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
            transform.position += Vector3.right * 3 * Time.deltaTime;
            transform.position += Vector3.up * 9 * Time.deltaTime;

            transform.Rotate(Vector3.forward, 1200 * Time.deltaTime);
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
            SheepAnimator.SetTrigger("RockSheep");
        }
        else if (myTag == "Leaf")
        {
            SheepAnimator.SetTrigger("LeafSheep");
        }
        else if (myTag == "Tree")
        {
            SheepAnimator.SetTrigger("TreeSheep");
        }
    }
}