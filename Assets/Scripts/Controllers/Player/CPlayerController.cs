using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerController : MonoBehaviour
{
    public PlayerModel characterData;
    public SCharacterState currentState;
    public static AudioManager audioManager;
    public Transform attackSpawnPoint;
    public GameObject attack, specialAttack, shield;
    public ParticleSystem particles;
    public AudioSource music;
    public AudioClip[] audios;

    [HideInInspector] [Range(1,3)]public int currentComboZero = 1;

    void Awake()
    {
        characterData = Instantiate(characterData);
        if(particles != null)
            particles.gameObject.SetActive(false);
        GameManager.RevivePlayer(this);
        GameManager.Instance.inventory.PassPlayerReference(this);

        switch (characterData.type)
        {
            case EPlayerType.X:
                ChangeState(new SPlayerIdleX(this));
                break;
            case EPlayerType.ZERO:
                ChangeState(new SPlayerIdleZero(this));
                break;
            default: break;
        }

        music = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        currentState.Execute();
        if (currentState.CheckDeath())
            LevelManager.Instance.userInterface.OnGameOver();
    }

    void FixedUpdate()
    {
        currentState.FixedExecute();
    }

    void LateUpdate()
    {
        currentState.LateExecute();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        currentState.OnTriggerEnter(collision);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        currentState.OnTriggerExit(collision);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollisionEnter(collision);
    }

    /// <summary>
    /// Changes player current state, executing previous state OnExit function, and next state OnInit function.
    /// </summary>
    /// <param name="newState">The new state the player should be using.</param>
    public void ChangeState(SCharacterState newState)
    {
        if (newState != null)
        {
            if (currentState != null)
                currentState.OnExit();
            currentState = newState;
            currentState.OnInit();
        }
    }

    /// <summary>
    /// Spawns player normal attack.
    /// </summary>
    public void SpawnAttack(int combo = 1)
    {
        switch (characterData.type)
        {
            case EPlayerType.X:
                break;
            case EPlayerType.ZERO:
                currentComboZero = combo;
                break;
            default: break;
        }
        /*if (characterData.type == EPlayerType.ZERO)
            currentComboZero = combo;
        //audioManager.PlayAudio("Shoot", characterData.name);
        if(characterData.type == EPlayerType.X)
        {
            
        }*/
        DropTheBeatShoot();
        Instantiate(attack, attackSpawnPoint);
    }

    /// <summary>
    /// Spawns player special attack.
    /// </summary>
    public void SpawnSpecialAttack()
    {
        //audioManager.PlayAudio("SpecialShoot", characterData.name);
        DropTheBeatShoot();
        Instantiate(specialAttack, attackSpawnPoint);
    }

    public void SpawnShield()
    {
        Instantiate(shield, transform);
    }

    public void ThrowObject(GameObject throwingObject, bool onAttackSpawner)
    {
        if (onAttackSpawner)
            Instantiate(throwingObject, attackSpawnPoint);
        else
            Instantiate(throwingObject, transform);
    }

    public void ButtonAttack()
    {
        Input.GetButtonDown("Attack");
    }

    public void ButtonJump()
    {
        Input.GetButtonDown("Jump");
    }

    public void ButtonLeft()
    {
        Input.GetButtonDown("ButtonLeft");
    }

    public void ButtonRight()
    {
        Input.GetButtonDown("ButtonRight");
    }

    void DropTheBeatShoot()
    {
        music.clip = audios[0];
        music.Play();
    }
    public void DropTheBeatHit()
    {
        music.clip = audios[1];
        music.Play();
    }
}
