using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

public class menuControl : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    

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

    public void goToMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }

    public void continueGame()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            Menu.SetActive(false);
        }
        
    }


}
