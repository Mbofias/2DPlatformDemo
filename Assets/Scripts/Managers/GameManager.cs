using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void GameDelegate();
    public static event GameDelegate onGameStarted;
    public static event GameDelegate onGameOver;

    private static GameManager instance;
    public static GameManager Instance { get => instance; }

    public Inventory inventory;
    public float playerHealth, playerEnergy;
    public bool onEnergyOverload = false, franMode = false;

    //--variable de tipo player--
    public EPlayerType playerType;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = this;
        else
            DestroyImmediate(gameObject);

        SceneManager.LoadScene("MainMenu");
    }

    void Start()
    {
        StartInventory(GetComponent<InventoryData>());
    }

    void Update()
    {
        if (onEnergyOverload)
            if (playerEnergy > 0)
                playerEnergy -= inventory.energyOverload.itemData.reductionFactor * Time.deltaTime;
            else
                onEnergyOverload = false;

        if (instance.inventory != null)
        {
            instance.inventory.Execute();
        }
    }

    /// <summary>
    /// Deals damge to the current energy shield.
    /// </summary>
    /// <param name="damage">The amount of damage dealt.</param>
    public void EnergyDamage(float damage)
    {
        playerEnergy -= damage;
        if (playerEnergy <= 0)
            onEnergyOverload = false;
    }

    /// <summary>
    /// Starts the inventory.
    /// </summary>
    /// <param name="tmpInv">The items inventory will contain.</param>
    public static void StartInventory(InventoryData tmpInv)
    {
        instance.inventory = new Inventory(tmpInv.aerialAttack, tmpInv.bomb, tmpInv.energyOverload, tmpInv.bombObject, tmpInv.aerialObject);
    }

    /// <summary>
    /// Resets the player current health.
    /// </summary>
    /// <param name="player">Reference to the player.</param>
    public static void RevivePlayer(CPlayerController player)
    {
        instance.playerHealth = player.characterData.maxHealth;
    }

    /// <summary>
    /// Loads an scene.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    public static void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void LoadSavedGame()
    {
        instance.playerHealth = SaveManager.Instance.Slot.playerHealth;
        instance.playerEnergy = SaveManager.Instance.Slot.playerShield;
        instance.onEnergyOverload = SaveManager.Instance.Slot.shieldEnabled;
        instance.inventory.bomb.currentAmount = SaveManager.Instance.Slot.bombsAmount;
        instance.inventory.aerialAttack.currentAmount = SaveManager.Instance.Slot.aerialAmount;
        instance.inventory.energyOverload.currentAmount = SaveManager.Instance.Slot.shieldAmount;
        instance.inventory.bomb.currentCD = SaveManager.Instance.Slot.bombCD;
        instance.inventory.aerialAttack.currentCD = SaveManager.Instance.Slot.aerialCD;
        instance.inventory.energyOverload.currentCD = SaveManager.Instance.Slot.shieldCD;

        if (SaveManager.Instance.Slot.isZero)
            instance.playerType = EPlayerType.ZERO;
        else
            instance.playerType = EPlayerType.X;
    }
}
