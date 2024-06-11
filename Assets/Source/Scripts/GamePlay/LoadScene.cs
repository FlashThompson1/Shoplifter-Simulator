using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public static LoadScene Instance;


    [SerializeField] private GameObject _infoPanel;
    [SerializeField] private GameObject _settingsPanel;

    private void Awake()
    {
        Instance = this;   
    }

    public void LoadNextScene(int ID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(ID);
    }


    public void ShowInfoPanel() { 
        _infoPanel.SetActive(true);
    }
    public void ShowSettingsPanel()
    {
        _settingsPanel.SetActive(true);
    }


    public void ExitGame()
    {
    
        Application.Quit();

    }
}
