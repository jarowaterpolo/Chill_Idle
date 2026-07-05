using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private DataSaver dataSaver = new();
    private GameObject settingsCanvas;

    public void StartNewGame(string sceneName)
    {
        dataSaver.ResetData();
        LoadScene(sceneName);
    }

    public void ContinueGame(string sceneName)
    {
        LoadScene(sceneName);
    }

    public void Settings()
    {
        settingsCanvas.SetActive(true);
    }

    private void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
