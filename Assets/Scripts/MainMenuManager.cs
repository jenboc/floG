using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject helpPanel;
    private AudioSource _audioSource;

    private void Start() 
    {
        _audioSource = GetComponent<AudioSource>();
        mainPanel.SetActive(true);
        helpPanel.SetActive(false);
    }

    public void PlayButtonPressed()
    {
        ButtonSound();
        SceneManager.LoadScene("Scenes/Game");
    }

    public void HelpButtonPressed()
    {
        ButtonSound();
        helpPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void BackButtonPressed()
    {
        ButtonSound();
        mainPanel.SetActive(true);
        helpPanel.SetActive(false);
    }

    public void ExitButtonPressed()
    {
        ButtonSound();
        Application.Quit();
    }

    private void ButtonSound() => _audioSource.PlayOneShot(buttonClickSound);
}
