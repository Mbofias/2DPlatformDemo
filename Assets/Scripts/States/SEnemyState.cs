using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SEnemyState
{
    protected CEnemyController enemy;
    protected Rigidbody2D rb2D;
    //protected Animator animator;
    protected SpriteRenderer sprite;
    protected Transform target;
    public AudioManager audioManager;
    protected SEnemyState(CEnemyController enemy)
    {
        this.enemy = enemy;
        rb2D = enemy.GetComponent<Rigidbody2D>();
        //animator = enemy.GetComponent<Animator>();
        sprite = enemy.GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public abstract void Execute();
    public abstract void FixedExecute();
    public abstract void LateExecute();
    

    public virtual void OnInit() { }
    public virtual void OnExit() { }

    public virtual void OnTriggerEnter(Collider2D collision) { }
    public virtual void OnTriggerExit(Collider2D collision) { }

    public virtual void OnCollisionEnter(Collision2D collision) { }
}
