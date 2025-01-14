using UnityEngine;

public class R1_Seesaw : MonoBehaviour
{
    [Header("References")]
    public HingeJoint2D hinge;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "sheep")
        {
            Debug.Log("시소 올라감!");
            // 플레이어가 시소에 올라갔을 때 물리적 반응 설정
            JointMotor2D motor = hinge.motor;
            motor.motorSpeed = 5f; // 원하는 속도로 시소를 움직이게 설정
            hinge.motor = motor;
            hinge.useMotor = false; // 모터 활성화
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "sheep")
        {
            // 플레이어가 떠났을 때 모터 비활성화
            hinge.useMotor = false;
        }
    }
}
