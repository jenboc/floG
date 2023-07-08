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
    [SerializeField] private Sprite lifeSprite;

    private AudioSource _audioSource;
    private int _score;

    private const float LIFE_Y_POS = -65f;
    private const float LIFE_X_PADDING = -57f;
    private const float LIFE_RIGHT_BORDER_PADDING = -114f;
    private const float HEART_SCALE = 0.5f;
    
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

    public void UpdateLives(int numLives)
    {
        var livesShowing = overlayPanel.transform.childCount - 1; // One child is score text

        if (livesShowing == numLives)
            return;

        if (livesShowing < numLives)
            AddLives(livesShowing, numLives - livesShowing);
        else
            RemoveLives(livesShowing - numLives);
    }

    private void AddLives(int numShowing, int toAdd)
    {
        for (var i = 0; i < toAdd; i++)
        {
            var imageObject = new GameObject($"Life {i}");
            var rectTransform = imageObject.AddComponent<RectTransform>();
            var image = imageObject.AddComponent<Image>();
            imageObject.transform.SetParent(overlayPanel.transform);

            rectTransform.anchorMin = new Vector2(1, 1);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.localScale = new Vector3(HEART_SCALE, HEART_SCALE, 1f);
            
            image.sprite = lifeSprite;
            var width = rectTransform.rect.width;
            Debug.Log(width);
            rectTransform.anchoredPosition = new Vector2(
                -numShowing * (width - LIFE_X_PADDING) + LIFE_RIGHT_BORDER_PADDING,
                LIFE_Y_POS) * HEART_SCALE;
            
            numShowing++;
        }
    }

    private void RemoveLives(int toRemove)
    {
        var totalChildren = overlayPanel.transform.childCount;
        for (var childIndex = 1; childIndex <= toRemove; childIndex++)
        {
            var child = overlayPanel.transform.GetChild(totalChildren - childIndex).gameObject;
            Destroy(child);
        }
    }
}
