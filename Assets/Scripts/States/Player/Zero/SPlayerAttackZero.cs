using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerAttackZero : SPlayerAttack
{
    [Range(0, 3)] private int chainAttacks;
    private float comboTimeUp = 0;

    public SPlayerAttackZero(CPlayerController controller, int currentCombo = 1) : base(controller) 
    {
        chainAttacks = currentCombo;
    }

    public override void OnInit()
    {
        switch (chainAttacks)
        {
            case 1:
                animator.SetTrigger("Attack");
                break;
            case 2:
                animator.SetTrigger("Attack02");
                break;
            case 3:
                animator.SetTrigger("Attack03");
                break;
            default: break;
        }
        player.SpawnAttack(chainAttacks);
    }
    public override void Execute()
    {
        base.Execute();

        comboTimeUp += Time.deltaTime;

        switch (chainAttacks)
        {
            case 1:
                if (comboTimeUp <= player.characterData.specialAttackTimer && Input.GetButtonDown("Attack"))
                {
                    chainAttacks++;
                    animator.SetTrigger("Attack02");
                    player.SpawnAttack(chainAttacks);
                    comboTimeUp = 0;
                }
                else
                    if (comboTimeUp > player.characterData.specialAttackTimer)
                    chainAttacks = 0;
                break;
            case 2:
                if (comboTimeUp <= player.characterData.specialAttackTimer && Input.GetButtonDown("Attack"))
                {
                    chainAttacks++;
                    animator.SetTrigger("Attack03");
                    player.SpawnAttack(chainAttacks);
                    comboTimeUp = 0;
                }
                else
                    if (comboTimeUp > player.characterData.specialAttackTimer)
                    chainAttacks = 0;
                break;
            default: break;
        }
    }
    public override void LateExecute()
    {
        if (chainAttacks < 1 || chainAttacks > 2)
        {
           player.currentComboZero = 1;
           player.ChangeState(new SPlayerIdleZero(player));
        }

        RaycastHit2D[] hitResults = new RaycastHit2D[2];
        ContactFilter2D layers = new ContactFilter2D();
        layers.SetLayerMask(1 << 0);

        if (rb2D.Cast(new Vector2(0, -1), layers, hitResults, 0.1f) == 0)
        {
            player.currentComboZero = 1;
            player.ChangeState(new SPlayerJumpAttackZero(player));
        }
    }
}
