using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UItutorialController : MonoBehaviour
{
    public GameObject tutorialPage;
    public Text instruction;
    public Dialog xMan,OMan;
    private string[] sentences;
    public Text subTitle;
    private int tutorialIndex;
    ItemModel itemModel;
    GameManager gameManager;
    int click;
    CPlayerController player;

    bool isChargingAttack;
    float countCharge;



    //Displays each istruction depending on the player type and the tutorial index

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CPlayerController>();
        tutorialPage.SetActive(false);
        gameManager = GameManager.Instance;
        tutorialIndex = 0;
        click = 0;
        if(gameManager.playerType == EPlayerType.X)
        { 
            sentences = xMan.sentences;
        }
        if(gameManager.playerType == EPlayerType.ZERO)
        {
            sentences = OMan.sentences;
        }
        subTitle.text = "";
        instruction.text = "";
        isChargingAttack = false;
    } 
    private void Update()
    {
        if (tutorialIndex >= 7)
            tutorialIndex = 0;
        StartTutorial(tutorialIndex);

        if (isChargingAttack)
        {
            countCharge += Time.deltaTime;
        }
    }
    IEnumerator Corroutine()
    {
        yield return new WaitForSeconds(3);
        StartTutorial(tutorialIndex);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="Player")
        {
            tutorialPage.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            tutorialPage.SetActive(false);
            tutorialIndex = 0;
        }
    }
    void StartTutorial(int _tutorialIndex)
    {
      
        switch (_tutorialIndex)
        {
            
            case 0:
                subTitle.text = "Movement:";
                instruction.text = sentences[_tutorialIndex];
                if (Input.GetButtonDown("Horizontal"))
                    tutorialIndex++;
                break;
            case 1:
                subTitle.text = "Jump:";
                instruction.text = sentences[_tutorialIndex];
                if (Input.GetButtonDown("Jump"))
                    tutorialIndex++;
                break;
            case 2:
                subTitle.text = "Attack:";
                instruction.text = sentences[_tutorialIndex];
                if (Input.GetButtonDown("Attack"))
                   tutorialIndex++;
                break;
            case 3:
                subTitle.text = "Special Attack:";
                instruction.text = sentences[_tutorialIndex];
                if(GameManager.Instance.playerType == EPlayerType.ZERO)
                { 
                    if (Input.GetButtonDown("Attack"))
                    {
                        click++;
                        if (click >= 2)
                        {
                            tutorialIndex++;
                            click = 0;
                        }
                    }
                }
                if(GameManager.Instance.playerType == EPlayerType.X)
                { 
                    if(Input.GetButtonDown("Attack"))
                    {
                        isChargingAttack = true;
                        
                    }
                    if (Input.GetButtonUp("Attack"))
                    {
                        if (player.characterData.specialAttackTimer <= countCharge)                      
                            tutorialIndex++;
                        isChargingAttack = false;
                        countCharge = 0;
                        

                    }
                }
                break;
            case 4:
                subTitle.text = "Item1:";
                instruction.text = sentences[_tutorialIndex];
                if (Input.GetButtonDown("Item1"))
                    tutorialIndex++;
                break;
            case 5:
                subTitle.text = "Item2:";
                instruction.text = sentences[_tutorialIndex];
                if (Input.GetButtonDown("Item2"))
                    tutorialIndex++;
                break;
            case 6:
                subTitle.text = "Item3:";
                instruction.text = sentences[_tutorialIndex];
                if (Input.GetButtonDown("Item3"))
                    tutorialIndex++;
                break;
            default:
                break;
        }
    }
}
