using UnityEngine;

public class R1_ScrollableObject : MonoBehaviour
{
    private float scrollSpeed = 0f;

    void Update()
    {
        if (scrollSpeed > 0f)
        {
            transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime, Space.World);
        }
    }

    public void SetScrollSpeed(R1_BackgroundScroll backgroundScroll)
    {
        scrollSpeed = backgroundScroll.GetCurrentScrollSpeed();
    }
}
