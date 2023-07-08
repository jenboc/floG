using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] private TMP_Text scoreTextOverlay;
    [SerializeField] private TMP_Text scoreTextGameOver;
    [SerializeField] private GameObject overlayPanel;
    [SerializeField] private GameObject gameOverPanel;

    private AudioSource _audioSource;
    private int _score;
    
    public bool GameActive { get; private set; }

    private void Start()
    {
        GameActive = true;
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
    
    private void UpdateScoreText(TMP_Text textBox) => textBox.text = $"Score: {_score}";

    private void ShowOverlay()
    {
        gameOverPanel.SetActive(false);
        overlayPanel.SetActive(true);
    }

    private void ShowGameOverMenu()
    {
        GameActive = false;
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
