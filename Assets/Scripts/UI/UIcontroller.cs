using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIcontroller : MonoBehaviour
{

    public GameObject pausePage,tutorialPage;
    private Image backGround;
    public GameObject PlayerUI;
    public GameObject GameOverPage;
    CPlayerController playerData;
    GameObject player;
    public BoxCollider2D endTutorialTriger;
    GamePageState GameState;
    public Toggle franMode;

/// <summary>
/// GamePageState to NONE, does not diplay any page but the PlayerUI.
/// Sets all images according to player 
/// </summary>
    private void Start()
    {
        SetGameState(GamePageState.NONE);
        player = GameObject.FindGameObjectWithTag("Player");
        backGround = pausePage.GetComponentInChildren<Image>();
        playerData = player.GetComponent<CPlayerController>();
        SetPausePage();
    }

    private void Update()
    {
        if(Input.GetKeyDown("p"))
        {
            OnPauseGame();
            //playAudioS
        }
        if(Input.GetKeyDown("k"))
        {
            OnGameOver();
        }
       
    }
    /// <summary>
    /// Sets game over page
    /// </summary>
    public void OnGameOver()
    {
       SetGameState(GamePageState.GAME_OVER);
    }
    /// <summary>
    /// Check if game is paused
    /// </summary>
    public void OnPauseGame()
    {
        if(GameState == GamePageState.PAUSE)
        {
            SetGameState(GamePageState.NONE);
            Time.timeScale = 1f;
        }
        else if(GameState != GamePageState.PAUSE)
        {
            SetGameState(GamePageState.PAUSE);
            Time.timeScale = 0f;
        }
    }
    /// <summary>
    /// Controlls all the GamePage in Game
    /// </summary>
    /// <param name="gameState"></param>
    void SetGameState(GamePageState gameState)
    {
        switch(gameState)
        {
            case GamePageState.NONE:
                pausePage.SetActive(false);
                PlayerUI.SetActive(true);
                tutorialPage.SetActive(false);
                GameOverPage.SetActive(false);
                 GameState = GamePageState.NONE;
                break;
            case GamePageState.TUTORIAL:
                pausePage.SetActive(false);
                PlayerUI.SetActive(true);
                tutorialPage.SetActive(true);
                GameOverPage.SetActive(false);
                GameState = GamePageState.NONE;
                break;
            case GamePageState.GAME_OVER:
                pausePage.SetActive(false);
                PlayerUI.SetActive(false);
                tutorialPage.SetActive(false);
                GameOverPage.SetActive(true);
                Time.timeScale = 0f;
                GameState = GamePageState.NONE;
                break;
            case GamePageState.PAUSE:
                pausePage.SetActive(true);
                PlayerUI.SetActive(false);
                tutorialPage.SetActive(false);
                GameOverPage.SetActive(false);
                GameState = GamePageState.PAUSE;
                break;
        }
    }/// <summary>
    /// Sets the background Image acording to player
    /// </summary>
    void SetPausePage()
    {
        backGround.sprite = playerData.characterData.backGroundImage;
    }

    public void DisableGamePages()
    {
        SetGameState(GamePageState.NONE);
    }
    //god like
    public void EnableFranMode()
    {
        GameManager.Instance.franMode = franMode.isOn;
    }
}
