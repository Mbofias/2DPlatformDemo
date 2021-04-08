using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CBossController : MonoBehaviour
{
    public EnemyModel enemyData;
    public SBossState currentState;
    public Transform attackSpawnPoint01, attackSpawnPoint02;
    public GameObject attack01, attack02;
    public Transform initPoint, movePoint01, movePoint02, movePoint03, movePoint04;
    public float ShakeDuration = 0.3f;
    private SpriteRenderer sprite;
    [Range(0f, 1000f)] public float damage=20f;
    public float hP;
    BossHealthBar healthBar;
    public AudioSource music;
    public AudioClip[] audios;
    //private float timeUpToShoot = 0f;
    //private Animator animator;
    //public bool shoot01 = false, shoot02 = false, startToAttack =false;
    // Start is called before the first frame update
    void Start()
    {
        enemyData = Instantiate(enemyData);
        hP = enemyData.hP;
        //startToAttack = false;
        //animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        ChangeState(new SBossShooting(this));
        //shoot01 = false;
        //shoot02 = false;
        initPoint.parent = null;
        movePoint01.parent = null;
        movePoint02.parent = null;
        movePoint03.parent = null;
        movePoint04.parent = null;
        healthBar = FindObjectOfType<BossHealthBar>();
        healthBar.SetHealth(hP);
        music = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
       // healthBar.value = hP;
        currentState.Execute();
     
        /*if (startToAttack)
        {
            InvokeRepeating("SpawnAttack", 0f, timeBetweenShoots);
        }
        else
        {
            CancelInvoke("SpawnAttack");
        }*/
        /*if (shoot01)
        {
            timeUpToShoot += Time.deltaTime;
            if (timeUpToShoot >= timeBetweenShoots)
            {
                SpawnShooting01();
                timeUpToShoot = 0f;
            }
        }*/
    }

    void FixedUpdate()
    {
        currentState.FixedExecute();
    }

    void LateUpdate()
    {
        currentState.LateExecute();
        currentState.CheckTranstions();
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

    public void ChangeState(SBossState newState)
    {
        if (newState != null)
        {
            if (currentState != null)
                currentState.OnExit();
            currentState = newState;
            currentState.OnInit();
        }
    }

    public void SpawnShooting01()
    {
        Instantiate(attack01, attackSpawnPoint01.position, attackSpawnPoint01.rotation);
        DropTheBeatShoot();
        //animator.SetTrigger("Attack");
    }

    public void SpawnShooting02()
    {
        Instantiate(attack02, attackSpawnPoint02.position, attackSpawnPoint02.rotation);
        DropTheBeatShoot();
        //animator.SetTrigger("Attack");
    }
    public virtual void TakeDamage(float damage)
    {
        hP -= damage;
        healthBar.SetHealth(hP);
        StartCoroutine("ChangeSpriteColor");
        if (hP > 0)
            DropTheBeatHit();
        else
        {
            DropTheBeatDestroy();
            Destroy(gameObject);
            LevelManager.Instance.EndBossFight();
        }
    }

    private IEnumerator ChangeSpriteColor()
    {
        //Empieza a vibrar
        InvokeRepeating("ChangeColor", 0, 0.1f);

        //Esperate unos instantes mientras vibra
        yield return new WaitForSeconds(ShakeDuration);

        //Deja de vibrar
        CancelInvoke("ChangeColor");
        sprite.color = Color.white;
    }

    private void ChangeColor()
    {
        if (sprite.color == Color.white)
            sprite.color = Color.red;
        else
            sprite.color = Color.white;
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

    public void DropTheBeatDestroy()
    {
        music.clip = audios[2];
        music.Play();
    }
}
