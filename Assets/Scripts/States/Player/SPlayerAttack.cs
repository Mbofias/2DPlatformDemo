using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerAttack : SPlayerCharacter
{
    public SPlayerAttack(CPlayerController controller) : base(controller) { }
    protected bool attack = false;

    public override void OnInit()
    {
        animator.SetTrigger("Attack");
        player.SpawnAttack();
        attack = true;
    }
    public override void OnExit()
    {
        attack = false;
    }
    public override void Execute()
    {
        if (Input.GetButtonDown("Jump"))
            rb2D.AddForce(Vector2.up * player.characterData.verticalImpulseFactor, ForceMode2D.Impulse);
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
