using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Text gameOverText;
    public float fadeDuration = 1f;
    public float displayDuration = 2f;

    private bool isGameOver = false;
    private float elapsedTime = 0f;
    private enum GameOverState { None, FadingIn, ShowingText, Done }
    private GameOverState currentState = GameOverState.None;

    void Update()
    {
        if (isGameOver)
        {
            HandleGameOverTransition();
        }
    }

    public void TriggerGameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            currentState = GameOverState.FadingIn;
            elapsedTime = 0f;

            gameOverPanel.SetActive(true);

            Image panelImage = gameOverPanel.GetComponent<Image>();
            Color panelColor = panelImage.color;
            panelColor.a = 0;
            panelImage.color = panelColor;

            Color textColor = gameOverText.color;
            textColor.a = 0;
            gameOverText.color = textColor;
        }
    }

    private void HandleGameOverTransition()
    {
        elapsedTime += Time.deltaTime;

        switch (currentState)
        {
            case GameOverState.FadingIn:
                FadeInPanel();
                break;

            case GameOverState.ShowingText:
                FadeInText();
                break;

            case GameOverState.Done:
                if (elapsedTime >= displayDuration)
                {
                    RestartFromCheckpoint();
                }
                break;
        }
    }

    private void FadeInPanel()
    {
        Image panelImage = gameOverPanel.GetComponent<Image>();
        Color panelColor = panelImage.color;
        panelColor.a = Mathf.Clamp01(elapsedTime / fadeDuration);
        panelImage.color = panelColor;

        if (panelColor.a >= 1f)
        {
            currentState = GameOverState.ShowingText;
            elapsedTime = 0f;
        }
    }

    private void FadeInText()
    {
        Color textColor = gameOverText.color;
        textColor.a = Mathf.Clamp01(elapsedTime / fadeDuration);
        gameOverText.color = textColor;

        if (textColor.a >= 1f)
        {
            currentState = GameOverState.Done;
            elapsedTime = 0f;
        }
    }

    private void RestartFromCheckpoint()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            PlayerDeath playerDeath = player.GetComponent<PlayerDeath>();
            if (playerDeath != null)
            {
                player.transform.position = playerDeath.respawnPosition;
                playerDeath.playerHealth.ResetLives(playerDeath.playerHealth.maxLives);
            }
        }

        // Reset panel dan teks
        gameOverPanel.SetActive(false);
        isGameOver = false;
        currentState = GameOverState.None;
        elapsedTime = 0f;

        // Reset warna panel
        Image panelImage = gameOverPanel.GetComponent<Image>();
        if (panelImage != null)
        {
            Color panelColor = panelImage.color;
            panelColor.a = 0f;
            panelImage.color = panelColor;
        }

        // Reset warna teks
        if (gameOverText != null)
        {
            Color textColor = gameOverText.color;
            textColor.a = 0f;
            gameOverText.color = textColor;
        }
    }

}