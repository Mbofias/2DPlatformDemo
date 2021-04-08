using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SCharacterState
{
    protected Rigidbody2D rb2D;
    protected Animator animator;
    protected SpriteRenderer sprite;

       
    
    public abstract void Execute();
    public abstract void FixedExecute();
    public abstract void LateExecute();
    public abstract void TakeDamage(float damage);
    public abstract bool CheckDeath();

    public virtual void OnInit() { }
    public virtual void OnExit() { }

    public virtual void OnTriggerEnter(Collider2D collision) { }
    public virtual void OnTriggerExit(Collider2D collision) { }

    public virtual void OnCollisionEnter(Collision2D collision) { }
}
