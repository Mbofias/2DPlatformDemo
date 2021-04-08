using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01ShootController : MonoBehaviour
{
    public EnemyShootsModel dataShoot;
    private GameObject target;
    private Rigidbody2D rb2D;
    public GameObject ExplsionEffectNoMissed;
    public GameObject ExplsionEffectMissed;
    public float timeToExplosion = 1f;
    private float timeUp = 0f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rb2D = GetComponent<Rigidbody2D>();
        if (target.transform.position.x >= transform.position.x)
            rb2D.velocity = new Vector2(dataShoot.speed* dataShoot.x, dataShoot.speed * dataShoot.y);
        else
            rb2D.velocity = new Vector2(-dataShoot.speed * dataShoot.x, dataShoot.speed * dataShoot.y);
    }

    private void Update()
    {
        timeUp+=Time.deltaTime;
        if(timeUp>timeToExplosion)
        {
            timeUp = 0;
            DestroyShootMissed();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            collision.gameObject.GetComponent<CPlayerController>().currentState.TakeDamage(dataShoot.damage);
            DestroyShoot();
        }
        else
            DestroyShootMissed();        
    }

    private void DestroyShootMissed()
    {
        Instantiate(ExplsionEffectMissed, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void DestroyShoot()
    {
        Instantiate(ExplsionEffectNoMissed, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
