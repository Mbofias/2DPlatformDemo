using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot01Controller : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private GameObject target;
    private bool targetFound;
    public EnemyShootsModel shootData;
    private Animator animator;
    private float targetPosionX, targetPosionY;
    public GameObject shootEffect;
    private float timeUpExplosion = 0f;
    public float timeToExplosion = 5f;
    private float timeUpShoot = 0f;
    public float timeToShoot = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb2D.gravityScale=1;
        targetFound = false;
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (rb2D.transform.position.y - target.transform.position.y > .01f && rb2D.gravityScale > 0)
            rb2D.gravityScale = Vector2.Distance(new Vector2(0, rb2D.position.y), new Vector2(0, target.transform.position.y));
        else
            rb2D.gravityScale = 0;


        if (rb2D.gravityScale <= 0 && !targetFound)
        {
            rb2D.velocity = Vector2.zero;
            rb2D.gravityScale = 0;
            rb2D.AddForce(Vector2.left * shootData.speed, ForceMode2D.Impulse);
            animator.SetBool("Shooting", true);
            targetFound = true;
            targetPosionX =target.transform.position.x;
            targetPosionY = target.transform.position.y;
        }
    }
    private void FixedUpdate()
    {
        if (targetFound)
        {
            rb2D.AddForce(Vector2.left * shootData.speed, ForceMode2D.Force);
            timeUpExplosion += Time.deltaTime;
            if (timeUpExplosion >= timeToExplosion)
                DestroyMissile();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerAttack")
            DestroyMissile();
        else if (collision.tag=="Player")
        {
            collision.GetComponent<CPlayerController>().currentState.TakeDamage(shootData.damage);
            DestroyMissile();
        }
    }

    public void DestroyMissile()
    {
        Instantiate(shootEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
