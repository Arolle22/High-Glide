using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    private float timer = 0f;
    private bool isTiming = false;
    private float finalTime = 0f;

    public TextMeshProUGUI timerText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (isTiming)
        {
            timer += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    public void StartTimer()
    {
        timer = 0f;
        isTiming = true;
        finalTime = 0f;
        UpdateTimerUI();
        Debug.Log("Timer started");
    }

    public void StopTimer()
    {
        isTiming = false;
        finalTime = timer;
        UpdateTimerUI();
        Debug.Log("Glide time: " + timer.ToString("F2") + " seconds");
    }

    public void EndGame()
    {
        isTiming = false;
        Debug.Log("You failed! Touched the ground.");
    }

    public float GetFinalTime()
    {
        return finalTime;
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int hours = (int)(timer / 3600);
            int minutes = (int)((timer % 3600) / 60);
            int seconds = (int)(timer % 60);
            int hundredths = (int)((timer * 100) % 100);

            timerText.text = string.Format("Timer: " + "{0:00}:{1:00}:{2:00}.{3:00}", hours, minutes, seconds, hundredths);
        }
    }
}
