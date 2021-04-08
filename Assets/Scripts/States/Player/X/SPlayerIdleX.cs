using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerIdleX : SPlayerIdle
{
    float specialChargeTime;
    bool isCharging;

    public SPlayerIdleX(CPlayerController controller, float _specialChargeTime = 0, bool _isCharging = false) : base(controller)
    {
        specialChargeTime = _specialChargeTime;
        isCharging = _isCharging;
    }

    public override void Execute()
    {
        base.Execute();

        if (Input.GetButtonDown("Attack"))
        {
            isCharging = true;
            player.particles.gameObject.SetActive(true);
        }
           

        if (isCharging)
        {
            specialChargeTime += Time.deltaTime;
            player.particles.startSpeed = specialChargeTime;
        }



        if (Input.GetButtonUp("Attack"))
        {
            isCharging = false;
            player.particles.gameObject.SetActive(false);
        }

        // Debug.Log(specialChargeTime);
    }

    public override void LateExecute()
    {
        if (h != 0)
            player.ChangeState(new SPlayerMoveX(player, specialChargeTime, isCharging));

        RaycastHit2D[] hitResults = new RaycastHit2D[2];
        ContactFilter2D layers = new ContactFilter2D();
        layers.SetLayerMask(1 << 0);

        if (rb2D.Cast(new Vector2(0, -1), layers, hitResults, 0.1f) == 0)
            player.ChangeState(new SPlayerJumpX(player, specialChargeTime, isCharging));

        if (!isCharging && specialChargeTime != 0)
            if (specialChargeTime >= player.characterData.specialAttackTimer)
                player.ChangeState(new SPlayerSpecialAttackX(player));
            else
                player.ChangeState(new SPlayerAttackX(player));
    }

    
}
