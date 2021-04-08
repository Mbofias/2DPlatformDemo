using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootsController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public EnemyShootsModel shoot;
    private Transform player;
    private float timeUp = 0f, timeUpToDestroy = 3f;
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
        rb2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb2D.AddForce(new Vector2(player.gameObject.GetComponent<CPlayerController>().attackSpawnPoint.position.x - player.position.x, 0) * shoot.speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        timeUp += Time.deltaTime;
        if (timeUp >= timeUpToDestroy)
        {
            AutoDestroy();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<CEnemyController>().TakeDamage(shoot.damage);
            AutoDestroy();
        }
        if (collision.tag == "Boss")
        {
            collision.gameObject.GetComponent<CBossController>().TakeDamage(shoot.damage);
            AutoDestroy();
        }
    }

    void AutoDestroy()
    {
        Destroy(gameObject);
    }
}
