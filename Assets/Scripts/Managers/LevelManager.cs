using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance { get => instance; }

    public float trainEventDurationInSeconds, spawnWaveEveryXSeconds;
    public int minEnemiesPerWave, maxEnemiesPerWave;
    public Transform playerSpawnPoint, bossSpawnPoint, cameraLeftBottomBorder, cameraRightTopBorder, cameraTrainRightBorder, cameraTrainLeftBorder;
    public Transform[] enemyLandSpawnPoints, enemyCloseAirSpawnPoints, enemyUnreachableAirSpawnPoints;
    public CircleCollider2D trainTrigger;
    public BoxCollider2D bossTrigger;
    public GameObject healthBarBoss;
    public UIcontroller userInterface;

    private SpawnManager spawner;
    private CCameraController cam;
    public float currentTrainEventCountDown;
    private bool trainEventCompleted = false, trainEventPlaying = false, isOnBossFight = false;
    private int cameraSize= 0;

    public bool TrainEventCompleted { get => trainEventCompleted; }
    public bool TrainEventPlaying { get => trainEventPlaying; }
    public CCameraController Cam { get => cam; }
    public int CameraSize { get => cameraSize; }
    void Awake()
    {
        instance = this;

        spawner = GetComponent<SpawnManager>();

        if (SaveManager.Instance.Slot != null)
        {
            cam = Camera.main.GetComponent<CCameraController>(); 
            cam.enabled = true;
            LoadSavedGame();
            GameManager.LoadSavedGame();
            RespawnPlayer();
        }
        else
        {
            RespawnPlayer();

            cam = Camera.main.GetComponent<CCameraController>();
            cam.enabled = true;
            cam.StartCameraBounds(cameraLeftBottomBorder.position, cameraTrainRightBorder.position);
        }
    }

    void Update()
    {
        if (trainEventPlaying)
        {
            currentTrainEventCountDown -= Time.deltaTime;
            if (currentTrainEventCountDown <= 0)
                EndTrainEvent();
        }
    }

    /// <summary>
    /// Respawns the player, stopping either Train or Boss events if any of them is active.
    /// </summary>
    public void RespawnPlayer()
    {
        if (trainEventPlaying)
            StopTrainEvent();

        if (isOnBossFight)
            StopBossFight();

        GameObject[] tmpPlayerList = GameObject.FindGameObjectsWithTag("Player");

        if (tmpPlayerList.Length > 0)
            foreach(GameObject playerGameObject in tmpPlayerList)
                Destroy(playerGameObject);

        spawner.SpawnPlayer(instance.playerSpawnPoint.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            if (!trainEventCompleted)
                BeginTrainEvent();
            else
                BeginBossFight();
    }

    /// <summary>
    /// Spawns an enemy wave.
    /// </summary>
    public void SpawnWave()
    {
        int tmp = Random.Range(minEnemiesPerWave, maxEnemiesPerWave + 1);

        for (int index = 0; index < tmp; index++)
        {
            if (index == maxEnemiesPerWave-1)
                spawner.SpawnEnemy(1, enemyUnreachableAirSpawnPoints[Random.Range(0, enemyUnreachableAirSpawnPoints.Length)]);
            else if (index == tmp-2)
                spawner.SpawnEnemy(0, enemyCloseAirSpawnPoints[Random.Range(0, enemyCloseAirSpawnPoints.Length)]);
            else
                spawner.SpawnEnemy(Random.Range(2,4), enemyLandSpawnPoints[Random.Range(0, enemyLandSpawnPoints.Length)]);
        }
    }

    //--------------------------------------------------------------------------------------


    /// <summary>
    /// Starts the Train event.
    /// </summary>
    private void BeginTrainEvent()
    {
        cam.ModifyCameraBounds(cameraTrainLeftBorder.position, false);
        InvokeRepeating("SpawnWave", .5f, spawnWaveEveryXSeconds);
        currentTrainEventCountDown = trainEventDurationInSeconds;
        trainEventPlaying = true;
        trainTrigger.enabled = false;

    }

    /// <summary>
    /// Ends the Train event, called only when the event is completed succesfully.
    /// </summary>
    private void EndTrainEvent()
    {
        cameraSize++;
        cam.UpdateCameraSize(cameraSize);
        CancelInvoke("SpawnWave");
        cam.ModifyCameraBounds(cameraRightTopBorder.position, true);
        trainEventPlaying = false;
        trainEventCompleted = true;
    }

    /// <summary>
    /// Ends the Train event, called only if the player dies before completing the event.
    /// </summary>
    private void StopTrainEvent()
    {
        CancelInvoke("SpawnWave");
        trainEventPlaying = false;
        trainTrigger.enabled = true;

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
            Destroy(go);
    }

    /// <summary>
    /// Starts the Boss fight.
    /// </summary>
    private void BeginBossFight()
    {
        cameraSize++;
        bossTrigger.enabled = false;
        cam.UpdateCameraSize(cameraSize);
        spawner.SpawnBoss(bossSpawnPoint.position);
        isOnBossFight = true;
        healthBarBoss.SetActive(true);
    }

    /// <summary>
    /// Ends the Boss fight, called only when the player succesfully defeated the boss.
    /// </summary>
    public void EndBossFight()
    {
        healthBarBoss.SetActive(false);
        SaveManager.Instance.Delete();
        GameManager.ChangeScene("MainMenu");
    }

    /// <summary>
    /// Ends the Boss fight, called only when the player fails to defeat the boss.
    /// </summary>
    private void StopBossFight()
    {
        cameraSize--;
        healthBarBoss.SetActive(false);
        bossTrigger.enabled = true;
        cam.UpdateCameraSize(cameraSize);
        isOnBossFight = false;

        List<GameObject> tmpList = new List<GameObject>();

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Boss"))
            Destroy(go);
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Missile"))
            Destroy(go);
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("BossAttack"))
            Destroy(go);
    }

    /// <summary>
    /// Updates your respawn position and saves the game.
    /// </summary>
    /// <param name="newPostion">New respawn position.</param>
    public void UpdateCheckpoint(Vector3 newPostion)
    {
        instance.playerSpawnPoint.position = newPostion;
        SaveManager.Instance.Save(new SaveData(instance, GameManager.Instance));
    }

    /// <summary>
    /// Loads a saved game data.
    /// </summary>
    private void LoadSavedGame()
    {
        instance.trainEventCompleted = SaveManager.Instance.Slot.trainEventCompleted;
        instance.playerSpawnPoint.SetPositionAndRotation(new Vector3(SaveManager.Instance.Slot.playerSpawnX, SaveManager.Instance.Slot.playerSpawnY, 0), new Quaternion(0,0,0,0));
        cam.StartCameraBounds(new Vector2(SaveManager.Instance.Slot.camMinX, SaveManager.Instance.Slot.camMinY), new Vector2(SaveManager.Instance.Slot.camMaxX, SaveManager.Instance.Slot.camMaxY));
        instance.cameraSize = SaveManager.Instance.Slot.cameraSize;
        cam.UpdateCameraSize(instance.cameraSize);
        trainTrigger.enabled = !SaveManager.Instance.Slot.trainEventCompleted;
    }

    /// <summary>
    /// Removes the instance onDestroy.
    /// </summary>
    private void OnDestroy()
    {
        instance = null;
    }
}
