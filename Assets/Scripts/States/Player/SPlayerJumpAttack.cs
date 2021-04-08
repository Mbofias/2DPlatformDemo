using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerJumpAttack : SPlayerJump
{
    public SPlayerJumpAttack(CPlayerController controller) : base(controller) { }

    public override void Execute()
    {
        base.Execute();
    }

    public override void FixedExecute()
    {
        rb2D.velocity = new Vector2(h * player.characterData.horizontalSpeed * player.characterData.horizontalSpeedAirFactor, rb2D.velocity.y);

        if (h > 0)
            rb2D.transform.rotation = Quaternion.AngleAxis(0, new Vector2(0, 1));

        if (h < 0)
            rb2D.transform.rotation = Quaternion.AngleAxis(180, new Vector2(0, 1));
    }

    public override void OnInit()
    {
        base.OnInit();
        animator.SetTrigger("Attack");
        player.SpawnAttack();
    }

    public override void LateExecute()
    {
        player.ChangeState(new SPlayerJump(player));
    }
}
