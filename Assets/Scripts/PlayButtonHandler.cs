using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayButtonHandler : MonoBehaviour
{
    public void LoadRound2Scene()
    {
        SceneManager.LoadScene("Round1_Scene"); 
    }
    public void LoadMainScene()
    {
        SceneManager.LoadScene("Main_Scene"); 
    }
}
