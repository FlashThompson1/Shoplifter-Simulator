using UnityEngine;
using TMPro;

public class Countdowntimer : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI timerText; 
    [SerializeField] private float countdownTime = 600f; 

    private float _currentTime;
    private Movement _player;

    private void Start()
    {
        _player = GameObject.FindObjectOfType<Movement>();
        _currentTime = countdownTime;
        UpdateTimerText();
    }

   private void Update()
    {
        if (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            _currentTime = 0;
            TimerEnded();
        }
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(_currentTime / 60);
        int seconds = Mathf.FloorToInt(_currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void TimerEnded()
    {
        _player.Gameover();
    }
}
