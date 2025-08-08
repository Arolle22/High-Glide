using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleMenu : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject helpPanel;

    [Header("Final Time UI")]
    public TMP_Text finalTimeText;

    private void Start()
    {
        ShowMainMenu();

        if (GameSession.FinalTime >= 0f && finalTimeText != null)
        {
            finalTimeText.text = $"Final Time: {GameSession.FinalTime:F2}s";

            GameSession.FinalTime = -1f;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Mountains");
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
    }

    public void ShowHelp()
    {
        if (helpPanel != null) helpPanel.SetActive(true);
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
    }

    public void CloseHelp()
    {
        if (helpPanel != null) helpPanel.SetActive(false);
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void ShowMainMenu()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
        if (helpPanel != null) helpPanel.SetActive(false);
    }
}
