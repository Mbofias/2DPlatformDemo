using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerController : MonoBehaviour
{
    public GameObject uiPlayerController;
    public Image playerImage;

    public Image healthBarImage;
    public Image healthBackground;
    private float healthBackgroundvalue;
    RectTransform rectTransform;
    private float minHealth;
    public Image item1, item2, item3;
    private Color originalColor;

    public GameObject shield;
    private Slider sliderShield;

    CPlayerController playerData;
    GameObject player;
    Inventory inventory;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerData = player.GetComponent<CPlayerController>();
        inventory = GameManager.Instance.inventory;
        healthBackground.fillAmount = 0;
        SetPlayerImage();
        originalColor = item1.color;

        sliderShield = shield.GetComponentInChildren<Slider>();
        shield.SetActive(false);
    }
    private void Update()
    {
        SetCurrentHealth();
        ActivateItem();
        if (inventory.bomb.currentCD <= 0)
            ResetItemColor(item1);

        if (inventory.aerialAttack.currentCD <= 0)
            ResetItemColor(item2);

        if (inventory.energyOverload.currentCD <= 0) 
            ResetItemColor(item3);
         
        if(GameManager.Instance.onEnergyOverload)
        {
            sliderShield.value = GameManager.Instance.playerEnergy;
        }
        else
        {
            shield.SetActive(false);
        }
    }
    //sets all img accordig to player (player uI)
    public void SetPlayerImage()
    {
        playerImage.sprite = playerData.characterData.image;
        healthBarImage.sprite = playerData.characterData.healthBarImage;
        item1.sprite = inventory.bomb.itemData.img;
        item2.sprite = inventory.aerialAttack.itemData.img;
        item3.sprite = inventory.energyOverload.itemData.img;
    }
    //sets player health bar
    public void SetCurrentHealth()
    {
        healthBackgroundvalue = playerData.characterData.maxHealth - GameManager.Instance.playerHealth;
        healthBackground.fillAmount = healthBackgroundvalue/100;
    }
    //sets shield bar 
    public void SetShield() 
    {
        sliderShield.maxValue = inventory.energyOverload.itemData.shield;
        sliderShield.value = inventory.energyOverload.itemData.shield;
    }
    //change color when item activated
    void ActivateItem()
    {
       if(Input.GetButtonDown("Item1"))
        {
            item1.color = new Color(0,0,0,50);
        }
        if (Input.GetButtonDown("Item2"))
        {
            item2.color = new Color(0, 0, 0, 50);
        }
        if (Input.GetButtonDown("Item3"))
        {
            item3.color = new Color(0, 0, 0, 50);
            shield.SetActive(true);
            SetShield();
        }
    }
    void ResetItemColor(Image _item)
    {
        _item.color = originalColor;   
    }
}
