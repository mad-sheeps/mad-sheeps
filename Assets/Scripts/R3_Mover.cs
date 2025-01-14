using UnityEngine;
using UnityEngine.UI;

public class Mover : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed=0.001f;
    private bool isInsideBox = false; 
    private bool isMovingRight = false;

    [Header("References")]
    public R3_ProgressBar progressBarController;
    private Animator SheepAnimator;  // 양 애니메이션
    public AudioManager audioManager;

    void Start()
    {
        SheepAnimator = Object.FindAnyObjectByType<R3_Sheep>()?.GetComponent<Animator>();
        // AudioManager가 설정되지 않은 경우 
       if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
            if (audioManager == null)
            {
                Debug.LogError("AudioManager not found in the scene!");
            }
        }
        progressBarController = Object.FindAnyObjectByType<R3_ProgressBar>();
    }

    void Update()
    {
        if (!isMovingRight)
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime * 0.01f;
        }
        else
        {
            transform.position += Vector3.right * 3 * Time.deltaTime;

            transform.position += Vector3.up * 4.5f * Time.deltaTime;

            transform.Rotate(Vector3.forward, 1200 * Time.deltaTime);
        }

        // 상자 내부 지속 확인
        CheckInsideBox();
        
        // 상자 내부에서만 소리를 감지하여 발사 audioManager.IsSoundDetectedAmplitude(0.01f)
        //Input.GetKeyDown(KeyCode.Space)
        if (isInsideBox && Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("Sound detected and weapon is inside the box! Triggering action...");
            isMovingRight = true;
            progressBarController.IncreaseOverlayWidth(20f);
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

    void CheckInsideBox()
    {
        Collider2D boxCollider = GameObject.FindWithTag("Box").GetComponent<Collider2D>();
        if (boxCollider != null)
        {
            // 현재 무기의 위치가 상자 Collider 안에 있는지 확인
            isInsideBox = boxCollider.OverlapPoint(transform.position);

            if (isInsideBox)
            {
                //Debug.Log("Weapon is inside the box.");
            }
            else
            {
                //Debug.Log("Weapon exited the box.");
            }
        }
    }



    //양 애니메이션
    void TriggerSheepAnimation()
    {
        string myTag = gameObject.tag;
        Debug.Log("gameobject: "+ myTag);

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