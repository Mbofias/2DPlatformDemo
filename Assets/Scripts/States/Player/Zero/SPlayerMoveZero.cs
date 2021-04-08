using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerMoveZero : SPlayerMove
{
    public SPlayerMoveZero(CPlayerController controller) : base(controller) { }

    public override void Execute()
    {
        base.Execute();
        if (Input.GetButtonDown("Attack"))
            attack = true;
    }
    public override void FixedExecute()
    {
        base.FixedExecute();
    }
    public override void LateExecute()
    {
        if (h == 0)
            player.ChangeState(new SPlayerIdleZero(player));
        if (attack)
            player.ChangeState(new SPlayerAttackZero(player));

        if (ladderPos != LadderPos.NONE)
        {
            player.ChangeState(new SOnLadderStateZero(player, ladderPos, ladderStep));
        }

        RaycastHit2D[] hitResults = new RaycastHit2D[2];
        ContactFilter2D layers = new ContactFilter2D();
        layers.SetLayerMask(1 << 0);

        if (rb2D.Cast(new Vector2(0, -1), layers, hitResults, 0.1f) == 0)
            player.ChangeState(new SPlayerJumpZero(player));
    }

    public override void OnExit()
    {
        base.OnExit();
        animator.SetBool("Running", false);
    }
}
