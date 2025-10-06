using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine;

public class menuControl : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }
    
    public void lvl2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void ExitGame()
    {
        Application.Quit();
    }


}
