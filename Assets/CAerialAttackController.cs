using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAerialAttackController : MonoBehaviour
{
    public AerialModel aerialData;
    public GameObject bomb;
    public Transform bombSpawnPoint, movementDirection;
    public float speed;

    private float timeSinceLastShot = 0;
    private Rigidbody2D rb2D;

    void Start()
    {
        aerialData = Instantiate(aerialData);
        transform.parent = null;
        GetComponent<SpriteRenderer>().sprite = aerialData.img;
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        aerialData.duration -= Time.deltaTime;

        if (timeSinceLastShot >= aerialData.timeBetweenBombs)
        {
            timeSinceLastShot = 0;
            Instantiate(bomb, bombSpawnPoint);
        }

        if (aerialData.duration <= 0)
            Destroy(gameObject);
    }

    void FixedUpdate()
    {
        rb2D.position = Vector2.MoveTowards(transform.position, movementDirection.position, speed * Time.deltaTime);
    }
}
