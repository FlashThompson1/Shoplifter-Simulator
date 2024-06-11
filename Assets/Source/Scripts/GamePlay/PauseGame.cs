using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField]private GameObject _pauseMenuPanel,_interfacePanel; 

    private bool isPaused = false;

   private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                GamePause();
            }
        }
    }

    public void GamePause()
    {
        Cursor.lockState = CursorLockMode.None;
        _pauseMenuPanel.SetActive(true);
        _interfacePanel.SetActive(false);
        Time.timeScale = 0f; 
        isPaused = true;
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _pauseMenuPanel.SetActive(false);
        _interfacePanel.SetActive(true);
        Time.timeScale = 1f; 
        isPaused = false;
    }
}
