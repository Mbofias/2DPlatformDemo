using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OnLadderState;

public class SPlayerIdleZero : SPlayerIdle
{
    public SPlayerIdleZero(CPlayerController controller) : base(controller) 
    {
        animator.SetFloat("HealthFactor", GameManager.Instance.playerHealth / player.characterData.maxHealth);
    }

    public override void OnInit()
    {
        base.OnInit();
    }

    public override void Execute()
    {
        base.Execute();
        
        if(Input.GetButtonDown("Attack"))
            attack = true;
    }
    public override void LateExecute()
    {
        if (h != 0)
            player.ChangeState(new SPlayerMoveZero(player));
        if (attack)
            player.ChangeState(new SPlayerAttackZero(player));

        if (ladderPos != LadderPos.NONE)
        {
            player.ChangeState(new OnLadderState(player, ladderPos, ladderStep));
        }

        RaycastHit2D[] hitResults = new RaycastHit2D[2];
        ContactFilter2D layers = new ContactFilter2D();
        layers.SetLayerMask(1<<0);

        if (rb2D.Cast(new Vector2(0, -1), layers, hitResults, 0.1f) == 0)
            player.ChangeState(new SPlayerJumpZero(player));
    }

    public override void OnTriggerEnter(Collider2D collision)
    {
        base.OnTriggerEnter(collision);
    }
}
