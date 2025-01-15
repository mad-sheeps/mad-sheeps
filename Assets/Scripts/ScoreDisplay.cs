using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI R1_Score_Text;
    public TextMeshProUGUI R2_Score_Text;
    public TextMeshProUGUI R3_Score_Text;
    public TextMeshProUGUI Toral_Score_Text;

    void Start()
    {
        // PlayerPrefs에서 점수 불러오기
        int r1Score = PlayerPrefs.GetInt("Round1", 0); // 기본값 0
        int r2Score = PlayerPrefs.GetInt("Round2", 0); // 기본값 0
        int r3Score = PlayerPrefs.GetInt("Round3", 0); // 기본값 0
        int total = r1Score+r2Score+r3Score;
        // 점수를 UI에 표시
        R1_Score_Text.text = $"{r1Score}";
        R2_Score_Text.text = $"{r2Score}";
        R3_Score_Text.text = $"{r3Score}";
        Toral_Score_Text.text = $"{total}";
    }
}
