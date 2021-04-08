using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerJumpZero : SPlayerJump
{
    public SPlayerJumpZero(CPlayerController controller) : base(controller) { }
    public override void OnInit()
    {
        base.OnInit();
    }
    public override void Execute()
    {
        base.Execute();
        if (Input.GetButtonDown("Attack"))
            attack = true;

        if (GameManager.Instance.franMode && Input.GetAxis("Jump") != 0)
            rb2D.AddForce(new Vector2(0, player.characterData.verticalImpulseFactor), ForceMode2D.Force);
    }
    public override void FixedExecute()
    {
        base.FixedExecute();
    }
    public override void OnExit()
    {
        if (!attack)
            base.OnExit();
    }
    public override void LateExecute()
    {
        if (attack)
            player.ChangeState(new SPlayerJumpAttackZero(player));

        RaycastHit2D[] hitResults = new RaycastHit2D[2];
        ContactFilter2D layers = new ContactFilter2D();
        layers.SetLayerMask(1 << 0);

        if (rb2D.Cast(new Vector2(0, -1), layers, hitResults, 0.1f) > 0)
            player.ChangeState(new SPlayerIdleZero(player));
    }
}
