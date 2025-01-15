using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayButtonHandler : MonoBehaviour
{
    public void LoadRound2Scene()
    {
        SceneManager.LoadScene("R1_Intro"); 
    }
    public void LoadMainScene()
    {
        SceneManager.LoadScene("Main_Scene"); 
    }
}
