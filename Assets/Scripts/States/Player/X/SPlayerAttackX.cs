using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerAttackX : SPlayerAttack
{
    public SPlayerAttackX(CPlayerController controller) : base(controller) { }

    public override void LateExecute()
    {
        RaycastHit2D[] hitResults = new RaycastHit2D[2];
        ContactFilter2D layers = new ContactFilter2D();
        layers.SetLayerMask(1 << 0);

        if (rb2D.Cast(new Vector2(0, -1), layers, hitResults, 0.1f) == 0)
            player.ChangeState(new SPlayerJumpAttackX(player));
        else
            player.ChangeState(new SPlayerIdleX(player));
    }
}
