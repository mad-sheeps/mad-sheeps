using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayButtonHandler : MonoBehaviour
{
    public void LoadRound2Scene()
    {
        SceneManager.LoadScene("Round1_Scene"); 
    }
}
