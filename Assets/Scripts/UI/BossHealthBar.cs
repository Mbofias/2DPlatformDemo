using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public GameObject healthBar;
    private Slider slider;
    CBossController bossController;
    LevelManager levelManager;
    public BossHealthBar Instance;

    Transform healthBarWordlP;

    private void Awake()
    {
        healthBar.SetActive(false);
        slider = healthBar.GetComponentInChildren<Slider>();       
        Instance = this;
    }
    //sets current BossHeath when boss getDmg
    public void SetHealth(float _hp)
    {
        bossController = GameObject.FindGameObjectWithTag("Boss").GetComponent<CBossController>();
        slider.maxValue = bossController.enemyData.hP;
        slider.value = _hp;
    }
}
