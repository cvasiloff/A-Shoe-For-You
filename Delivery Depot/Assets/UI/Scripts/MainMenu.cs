using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    [Header("Set In Inspector")]
    public GameObject mainPanel;
    public GameObject optionsPanel;
    public GameObject creditsPanel;
    public GameObject howToPlayPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void OptionsButton()
    {
        mainPanel.gameObject.SetActive(false);
        optionsPanel.gameObject.SetActive(true);
        creditsPanel.gameObject.SetActive(false);
        howToPlayPanel.gameObject.SetActive(false);
    }

    public void CreditsButton()
    {
        mainPanel.gameObject.SetActive(false);
        optionsPanel.gameObject.SetActive(false);
        creditsPanel.gameObject.SetActive(true);
        howToPlayPanel.gameObject.SetActive(false);
    }

    public void HowToPlayButton()
    {
        mainPanel.gameObject.SetActive(false);
        optionsPanel.gameObject.SetActive(false);
        creditsPanel.gameObject.SetActive(false);
        howToPlayPanel.gameObject.SetActive(true);
    }

    public void MenuButton()
    {
        mainPanel.gameObject.SetActive(true);
        optionsPanel.gameObject.SetActive(false);
        creditsPanel.gameObject.SetActive(false);
        howToPlayPanel.gameObject.SetActive(false);
    }
}
