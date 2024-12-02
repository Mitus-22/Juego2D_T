using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine;

public class menuControl : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ExitGame(){
        Application.Quit();
    }


}
