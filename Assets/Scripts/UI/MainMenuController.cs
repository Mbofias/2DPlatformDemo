using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour
{
    private CPlayerController player;
    public GameObject SelectPlayerPage;
    public GameObject MainMenuPage;
    AudioManager audioManager;

    void Start()
    {
        MainMenuPage.SetActive(true);
        SelectPlayerPage.SetActive(false);
    }
    public void OnPlayButton()
    {
        MainMenuPage.SetActive(false);
        SelectPlayerPage.SetActive(true);
    }
    public void SelectPlayerX()
    {
        //sets player X
        GameManager.Instance.playerType = EPlayerType.X;
        SceneManager.LoadScene("GameScene");
        print(GameManager.Instance.playerType);
    }
    public void SelectPlayerZero()
    {
        //sets playerY
        GameManager.Instance.playerType = EPlayerType.ZERO;
        SceneManager.LoadScene("GameScene");
        print(GameManager.Instance.playerType);
    }
    public void Load()
    {
        SaveManager.Instance.Load();
        if (SaveManager.Instance.Slot != null)
            SceneManager.LoadScene("GameScene");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
