using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    private void OnEnable() => Time.timeScale = 0f;

    public void RestartScene() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void ExitApplcation() => Application.Quit();
}