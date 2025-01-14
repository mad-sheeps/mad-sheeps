using UnityEngine;

public class R1_Spinning : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 400 * Time.deltaTime));
    }
}
