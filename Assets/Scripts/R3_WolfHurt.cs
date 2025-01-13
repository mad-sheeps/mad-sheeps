using UnityEngine;

public class WolfHurt : MonoBehaviour
{
    [Header("References")]
    public Animator PlayerAnimator;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Leaf") || collision.gameObject.CompareTag("Rock") || collision.gameObject.CompareTag("Tree"))
        {
            PlayerAnimator.SetBool("hurt", true);
            Invoke("ResetHurtAnimation", 0.36f);
        }
    }

    void ResetHurtAnimation()
    {
        PlayerAnimator.SetBool("hurt", false);
    }
}
