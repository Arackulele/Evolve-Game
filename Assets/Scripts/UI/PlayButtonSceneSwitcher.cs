using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonSceneSwitcher : MonoBehaviour
{


    public void LoadChapter()
    {

        SceneManager.LoadScene("TestScene");
    }

    public void QuitGame()
    {

        Application.Quit();
    }


}
