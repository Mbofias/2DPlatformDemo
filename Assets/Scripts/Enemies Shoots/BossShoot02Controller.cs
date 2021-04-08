using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot02Controller : MonoBehaviour
{
    public EnemyShootsModel shootData;
    private Rigidbody2D rb2D;
    private GameObject target;
    public GameObject explosion;
    private float timeUpToExplosion = 3f,timeUp=0;
    private float targetPosionX;
    private bool targetFound, isLaunch;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        targetFound = false;
        isLaunch = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeUp += Time.deltaTime;
        if (timeUp >= timeUpToExplosion)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        RaycastHit2D[] hitResults = new RaycastHit2D[2];

        if (rb2D.Cast(new Vector2(0, -1), hitResults, 0.1f) == 0)
        {
            rb2D.velocity = new Vector3(0, -shootData.y, 0);
        }
        else
        {
            if (!targetFound)
            {
                targetPosionX = target.transform.position.x;
                targetFound = true;
            }
            if (targetPosionX >= transform.position.x && !isLaunch)
            {
                //rb2D.velocity = new Vector3(shootData.speed, 0, 0);
                rb2D.AddForce(Vector2.right* shootData.speed, ForceMode2D.Impulse);
                isLaunch = true;
            }
            if (targetPosionX < transform.position.x && !isLaunch)
            {
                //rb2D.velocity = new Vector3(-1*shootData.speed, 0, 0);
                rb2D.AddForce(Vector2.left * shootData.speed, ForceMode2D.Impulse);
                isLaunch = true;
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<CPlayerController>().currentState.TakeDamage(shootData.damage);
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }


}
