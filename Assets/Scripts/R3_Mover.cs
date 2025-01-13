using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed;
    private bool isInsideBox = false; 
    private bool isMovingRight = false;
    private Animator SheepAnimator;  // 양 애니메이션
    public AudioManager audioManager;

    void Start()
    {
        SheepAnimator = GetComponent<Animator>();
        // AudioManager가 설정되지 않은 경우 
       if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
            if (audioManager == null)
            {
                Debug.LogError("AudioManager not found in the scene!");
            }
        }

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

        // 무기가 상자 안에 있을 때 소리를 감지해 발사
        if (isInsideBox && audioManager != null && audioManager.IsSoundDetected(50f)) 
        {
            Debug.Log("Sound detected! Triggering action...");
            isMovingRight = true;
            TriggerSheepAnimation();

            transform.position = new Vector2(-1.645f, -1.507f); // 위치 이동
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