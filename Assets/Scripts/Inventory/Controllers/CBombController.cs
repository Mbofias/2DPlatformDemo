using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBombController : MonoBehaviour
{
    public BombModel bombData;
    public GameObject ExplosionEffect;

    private float timeUp = 0f;
    private Rigidbody2D rb2D;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
         Debug.Log("BUM");
        bombData = Instantiate(bombData);
        transform.parent = null;

        rb2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player.rotation.y == 0) 
            rb2D.AddForce(new Vector2(bombData.throwFactorHorizontal, bombData.throwFactorVertical), ForceMode2D.Impulse);
        else
            rb2D.AddForce(new Vector2(-bombData.throwFactorHorizontal, bombData.throwFactorVertical), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        timeUp += Time.deltaTime;
        if (timeUp >= bombData.timeToExplosion)
        {
            Instantiate(ExplosionEffect, transform);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "Enemy":
                collision.GetComponent<CEnemyController>().TakeDamage(bombData.damage);
                Instantiate(ExplosionEffect, transform);
                Destroy(gameObject);
                break;
            case "Boss":
                collision.GetComponent<CBossController>().TakeDamage(bombData.damage);
                Instantiate(ExplosionEffect, transform);
                Destroy(gameObject);
                break;
            default:break;
        }


    }
}
