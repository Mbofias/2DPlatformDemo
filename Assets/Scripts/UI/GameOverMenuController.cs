using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenuController : MonoBehaviour
{
    public UIcontroller userInterface;
    public Image backGround;
    public CharacterModel XModel;
    public CharacterModel ZeroModel;

    void Start()
    {
        //sets background for current player.
        if(GameManager.Instance.playerType == EPlayerType.X)
            backGround.sprite = XModel.backGroundImage;
        if (GameManager.Instance.playerType == EPlayerType.ZERO)
            backGround.sprite = ZeroModel.backGroundImage;
        backGround.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
    }
    //load MainMenu.
    public void OnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    //resets Game.
    public void OnRestartGame()
    {
        LevelManager.Instance.RespawnPlayer();
        userInterface.DisableGamePages();
        Time.timeScale = 1f;
    }
    public void OnExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
