using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayButtonPressed() => SceneManager.LoadScene("Scenes/Game");
    public void ExitButtonPressed() => Application.Quit();
}
