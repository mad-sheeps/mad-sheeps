using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed;
    public bool isInsideBox = false; 
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

        // 상자 내부에서만 소리를 감지하여 발사
        if (isInsideBox && audioManager != null)
        {
            if (audioManager.IsSoundDetected(50f)) // 임계값을 낮춤
            {
                Debug.Log("Sound detected and weapon is inside the box! Triggering action...");
                isMovingRight = true;
                TriggerSheepAnimation();

                transform.position = new Vector2(-1.645f, -1.507f); // 위치 이동
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Box"))
        {
        //     // 상자 안에 있는지 확인하기 위해 위치 조건 추가
        //     Vector2 weaponPosition = transform.position; // 무기의 현재 위치
        //     Collider2D boxCollider = other.GetComponent<Collider2D>(); // Box Collider 가져오기

        //     if (boxCollider != null && boxCollider.OverlapPoint(weaponPosition))
        //     {
        //         isInsideBox = true;
        //         Debug.Log("Weapon is inside the box!");
        //     }
        // }

        // if (other.CompareTag("Wolf"))
        // {
        //     Destroy(gameObject);
        // }
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
        //     // 위치 조건으로 상자 밖에 나갔는지 확인
        //     Vector2 weaponPosition = transform.position; // 무기의 현재 위치
        //     Collider2D boxCollider = other.GetComponent<Collider2D>(); // Box Collider 가져오기

        //     if (boxCollider != null && !boxCollider.OverlapPoint(weaponPosition))
        //     {
        //         isInsideBox = false;
        //         Debug.Log("Weapon exited the box.");
        //     }
        // }
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