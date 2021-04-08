using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerJumpAttackZero : SPlayerJumpAttack
{
    private bool floorTransition = false;

    public SPlayerJumpAttackZero(CPlayerController controller) : base(controller) 
    {
    }

    public override void OnInit()
    {
        player.SpawnAttack();
        animator.SetTrigger("Attack01");
    }
    public override void Execute()
    {

    }
    public override void LateExecute()
    {
        player.ChangeState(new SPlayerJumpZero(player));

        RaycastHit2D[] hitResults = new RaycastHit2D[2];
        ContactFilter2D layers = new ContactFilter2D();
        layers.SetLayerMask(1 << 0);

        if (rb2D.Cast(new Vector2(0, -1), layers, hitResults, 0.1f) > 0)
        {
            floorTransition = true;
            player.ChangeState(new SPlayerIdleZero(player));
        }
    }
    public override void OnExit()
    {
        if (floorTransition)
            animator.SetTrigger("StopJumpAttack");
    }
}
