using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerJumpAttackX : SPlayerJumpAttack
{
    public SPlayerJumpAttackX(CPlayerController controller) : base(controller) { }

    public override void LateExecute()
    {
        player.ChangeState(new SPlayerJumpX(player));
    }
}
