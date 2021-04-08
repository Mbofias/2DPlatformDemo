using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SPlayerCharacter : SCharacterState
{
    protected CPlayerController player;
    protected CCameraController cam;
    protected SPlayerCharacter(CPlayerController controller)
    {
        player = controller;
        rb2D = controller.GetComponent<Rigidbody2D>();
        animator = controller.GetComponent<Animator>();
        sprite = controller.GetComponent<SpriteRenderer>();
        cam = Camera.main.GetComponent<CCameraController>();
    }

    public override void Execute() { }

    public override void FixedExecute() { }

    public override void LateExecute() { }

    public override void TakeDamage(float damage)
    {
        if (!GameManager.Instance.franMode)
        {
            if (GameManager.Instance.onEnergyOverload)
                GameManager.Instance.EnergyDamage(damage);
            else
            {
                GameManager.Instance.playerHealth -= damage;
                player.DropTheBeatHit();
            }

            animator.SetFloat("HealthFactor", GameManager.Instance.playerHealth / player.characterData.maxHealth);
            animator.SetTrigger("TakeDamage");
        }
    }

    public virtual bool CheckJumpImpulse()
    {
        if (rb2D.velocity.y > player.characterData.verticalImpulseFactor || rb2D.velocity.y < player.characterData.verticalImpulseFactor * .8)
            return false;
        else
            return true;
    }

    /// <summary>
    /// Checks if the player is dead.
    /// </summary>
    /// <returns>Returns true if the player is dead, false if the player is still alive.</returns>
    public override bool CheckDeath()
    {
        if (GameManager.Instance.playerHealth <= 0 || rb2D.position.y <= cam.GetCameraBounds().min.y)
            return true;
        else
            return false;
    }
}
