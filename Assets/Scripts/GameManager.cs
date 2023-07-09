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
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private GameObject overlayPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Sprite lifeSprite;

    [SerializeField] private AudioClip[] gameMusic;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioClip buttonClickSound;

    [SerializeField] private GameObject explosionParticles;
    
    private AudioSource _sfxAudioSource;
    private AudioSource _musicAudioSource; 
    private int _score;

    private const float LIFE_Y_POS = -80f;
    private const float LIFE_X_PADDING = -45f;
    private const float LIFE_RIGHT_BORDER_PADDING = -114f;
    private const float HEART_SCALE = 0.5f;
    
    public bool GameActive { get; private set; }

    private void Awake()
    {
        var audioSources = GetComponents<AudioSource>();
        _sfxAudioSource = audioSources[0];
        _musicAudioSource = audioSources[1];

        _musicAudioSource.volume = 0.5f;
    }
    
    private void Start()
    {
        GameActive = true;
        _score = 0;
        UpdateScoreText(scoreTextOverlay);
        ShowOverlay();
    }

    private void Update()
    {
        if (GameActive && !_musicAudioSource.isPlaying)
            StartGameMusic();
    }
    
    private void StartGameMusic()
    {
        var music = gameMusic[Random.Range(0, gameMusic.Length)];
        _musicAudioSource.PlayOneShot(music);
    }
    
    public void UpdateScore(int dScore)
    {
        _score += dScore;
        UpdateScoreText(scoreTextOverlay);
    }

    public void UpdateAmmo(int newAmmo)
    {
        ammoText.text = $"Ammo: {newAmmo}";
    }
    
    private void UpdateScoreText(TMP_Text textBox) => textBox.text = $"Score: {_score}";

    private void ShowOverlay()
    {
        gameOverPanel.SetActive(false);
        overlayPanel.SetActive(true);
    }

    private void ShowGameOverMenu()
    {
        PlayGameOverSound(); 
        GameActive = false;
        overlayPanel.SetActive(false);
        UpdateScoreText(scoreTextGameOver);
        gameOverPanel.SetActive(true);
    }

    private void PlayGameOverSound()
    {
        _musicAudioSource.Stop();
        _musicAudioSource.PlayOneShot(gameOverSound);
    }

    public void PlayerDied() => ShowGameOverMenu();

    private void PlayClickSound() => _sfxAudioSource.PlayOneShot(buttonClickSound);
    
    public void ShowMainMenu()
    {
        PlayClickSound();
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    public void ReloadScene()
    {
        PlayClickSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlaySound(AudioClip clip) => _sfxAudioSource.PlayOneShot(clip);

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

    public void PlayExplosionParticles(Vector3 location)
    {
        Instantiate(explosionParticles, location, Quaternion.identity);
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
