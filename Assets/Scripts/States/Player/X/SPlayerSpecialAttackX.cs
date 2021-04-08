using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerSpecialAttackX : SPlayerSpecialAttack
{
    private float attackTimer;

    public SPlayerSpecialAttackX(CPlayerController controller) : base(controller) { }

    public override void OnInit()
    {
        base.OnInit();
        attackTimer = 0;
    }

    public override void Execute()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > player.characterData.specialAttackTimer) 
            player.SpawnSpecialAttack();
    }

    public override void LateExecute()
    {
        if (attackTimer > player.characterData.specialAttackTimer)
        {
            RaycastHit2D[] hitResults = new RaycastHit2D[2];
            ContactFilter2D layers = new ContactFilter2D();
            layers.SetLayerMask(1 << 0);

            if (rb2D.Cast(new Vector2(0, -1), layers, hitResults, 0.1f) == 0)
                player.ChangeState(new SPlayerJumpX(player));
            else
                player.ChangeState(new SPlayerIdleX(player));
        }
    }
}
