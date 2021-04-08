using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerSpecialAttack : SPlayerCharacter
{
    public SPlayerSpecialAttack(CPlayerController controller) : base(controller) { }

    public override void OnInit()
    {
        animator.SetTrigger("SpecialAttack");
    }

    public override void LateExecute()
    {
        RaycastHit2D[] hitResults = new RaycastHit2D[2];

        if (rb2D.Cast(new Vector2(0, -1), hitResults, 0.1f) == 0)
            player.ChangeState(new SPlayerJumpAttack(player));
        else
            player.ChangeState(new SPlayerIdle(player));
    }
}
