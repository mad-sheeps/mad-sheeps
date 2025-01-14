using UnityEngine;

public class WolfHurt : MonoBehaviour
{
    [Header("References")]
    public Animator PlayerAnimator;

    void Start()
    {
        if (PlayerAnimator == null)
        {
            Debug.LogError("PlayerAnimator is not assigned!");
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
}