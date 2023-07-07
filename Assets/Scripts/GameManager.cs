using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text scoreTextOverlay;
    [SerializeField] private Text scoreTextGameOver;
    [SerializeField] private GameObject overlayPanel;
    [SerializeField] private GameObject gameOverPanel;

    private AudioSource _audioSource;
    private int _score;

    private void Start()
    {
        _score = 0;
        UpdateScoreText(scoreTextOverlay);
        ShowOverlay();

        _audioSource = GetComponent<AudioSource>();
    }
    
    public void UpdateScore(int dScore)
    {
        _score += dScore;
        UpdateScoreText(scoreTextOverlay);
    }
    
    private void UpdateScoreText(Text textBox) => textBox.text = $"Score: {_score}";

    private void ShowOverlay()
    {
        gameOverPanel.SetActive(false);
        overlayPanel.SetActive(true);
    }

    private void ShowGameOverMenu()
    {
        overlayPanel.SetActive(false);
        UpdateScoreText(scoreTextGameOver);
        gameOverPanel.SetActive(true);
    }

    public void PlayerDied() => ShowGameOverMenu();

    public void ShowMainMenu()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlaySound(AudioClip clip) => _audioSource.PlayOneShot(clip);
}
