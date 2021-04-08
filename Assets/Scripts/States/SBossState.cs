using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SBossState
{
    protected CBossController enemy;
    protected Rigidbody2D rb2D;
    //protected Animator animator;
    protected SpriteRenderer sprite;
    protected Transform target;
    protected SBossState(CBossController boss)
    {
        this.enemy = boss;
        rb2D = enemy.GetComponent<Rigidbody2D>();
        //animator = enemy.GetComponent<Animator>();
        sprite = enemy.GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public abstract void Execute();
    public abstract void FixedExecute();
    public abstract void LateExecute();
    public abstract void CheckTranstions();

    public virtual void OnInit() { }
    public virtual void OnExit() { }

    public virtual void OnTriggerEnter(Collider2D collision) { }
    public virtual void OnTriggerExit(Collider2D collision) { }

    public virtual void OnCollisionEnter(Collision2D collision) {

        if (collision.gameObject.tag == "Player")
        {
            //collision.gameObject.GetComponent<CPlayerController>().takeDamage(damage);
        }

    }
}
