using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    public GameObject timeUpPanel;
    public TextMeshProUGUI finalScoreText;
    public Button retryButton;

    public float startTime = 120f;
    private float timeLeft;
    private bool gameEnded = false;

    public int score = 0;
    public int highScore = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        ResetGame();
        retryButton.onClick.AddListener(RestartGame);
        timeUpPanel.SetActive(false);
        UpdateUI();
    }

    private void Update()
    {
        if (gameEnded) return;

        timeLeft -= Time.deltaTime;
        timeLeft = Mathf.Clamp(timeLeft, 0f, startTime);
        UpdateUI();

        if (timeLeft <= 0f)
        {
            EndGame();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (score > highScore)
        {
            highScore = score;
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (timerText != null)
            timerText.text = $"Время: {Mathf.CeilToInt(timeLeft)}";

        if (scoreText != null)
            scoreText.text = $"Очки: {score}";

        if (highScoreText != null)
            highScoreText.text = $"Рекорд: {highScore}";
    }

    private void EndGame()
    {
        gameEnded = true;

        timeUpPanel.SetActive(true);

        if (finalScoreText != null)
            finalScoreText.text = $"Ты набрал: {score} очков!";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("HallScene");

        score = 0;
        timeLeft = startTime;
        gameEnded = false;
    }

    private void ResetGame()
    {
        score = 0;
        timeLeft = startTime;
        gameEnded = false;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "HallScene")
        {
            timeUpPanel = GameObject.Find("TimeUpPanel");

            GameObject finalScoreGO = GameObject.Find("FinalScoreText");
            if (finalScoreGO != null)
                finalScoreText = finalScoreGO.GetComponent<TextMeshProUGUI>();

            GameObject retryButtonGO = GameObject.Find("RetryButton");
            if (retryButtonGO != null)
            {
                retryButton = retryButtonGO.GetComponent<Button>();
                retryButton.onClick.RemoveAllListeners();
                retryButton.onClick.AddListener(RestartGame);
            }

            if (timeUpPanel != null)
                timeUpPanel.SetActive(false);

            UpdateUI();
        }
    }
}