using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyController : MonoBehaviour
{
    public EnemyModel enemyData;
    public SEnemyState currentState;
    public Transform attackSpawnPoint;
    public GameObject attack;
    public GameObject deathPrefab;
    public static AudioManager audioManager;
    public Transform leftPoint, rightPoint;
    public bool faceRight = true,startToAttack,faceToPlayer=true;
    [Range(0f, 20f)] public float visionRange = 4f;
    [Range(0f, 1000f)] public float timeBetweenShoots = 4f;
    private float timeUpToShoot = 0f, hP;
    public float sqrRangeVision;
    private Animator animator;
    public bool shoot = false;
    private SpriteRenderer sprite;
    public float ShakeDuration = 0.3f;

    public AudioSource music;
    public AudioClip[] audios;

    // Start is called before the first frame update
    void Start()
    {
        enemyData = Instantiate(enemyData);
        hP = enemyData.hP;
        leftPoint.parent=null;
        rightPoint.parent = null;
        sqrRangeVision = visionRange * visionRange;
        startToAttack = false;
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        ChangeState(new SEnemyPatrol(this));
        shoot = false;
        music = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        currentState.Execute();
        /*if (startToAttack)
        {
            InvokeRepeating("SpawnAttack", 0f, timeBetweenShoots);
        }
        else
        {
            CancelInvoke("SpawnAttack");
        }*/
        if (startToAttack)
        {
            timeUpToShoot += Time.deltaTime;
            if(timeUpToShoot>= timeBetweenShoots)
            {
                SpawnAttack();
                timeUpToShoot = 0f;
               // audioManager.PlayAudio("Shoot", enemyData.name);
            }
        }
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

    public void ChangeState(SEnemyState newState)
    {
        if (newState != null)
        {
            if (currentState != null)
                currentState.OnExit();
            currentState = newState;
            currentState.OnInit();
        }
    }

    public void SpawnAttack()
    {
        Instantiate(attack, attackSpawnPoint.position, attackSpawnPoint.rotation);
        animator.SetTrigger("Attack");
        DropTheBeatShoot();
    }

    public void TakeDamage(float damage)
    {
        hP -= damage;
        StartCoroutine("ChangeSpriteColor");
        if(hP>0)
            DropTheBeatHit();
        //audioManager.PlayAudio("Hit", enemyData.name);
        else
        {
            DropTheBeatDestroy();
            Destroy(gameObject);
           // audioManager.PlayAudio("Death", enemyData.name);
            GameObject e = Instantiate(deathPrefab) as GameObject;
            e.transform.position = transform.position;
            Destroy(e, 0.5f);
            
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
