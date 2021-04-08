using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerJump : SPlayerCharacter
{
    protected float h;
    protected bool attack;
    public SPlayerJump(CPlayerController controller) : base(controller) { }

    public override void OnInit()
    {
        animator.SetBool("Jump", true);
    }

    public override void OnExit()
    {
        animator.SetBool("Jump", false);
        
    }

    public override void Execute()
    {
        h = Input.GetAxis("Horizontal");
    }

    public override void FixedExecute()
    {
        rb2D.velocity = new Vector2(h * player.characterData.horizontalSpeed * player.characterData.horizontalSpeedAirFactor, rb2D.velocity.y);

        if (h > 0)
            rb2D.transform.rotation = Quaternion.AngleAxis(0, new Vector2(0, 1));

        if (h < 0)
            rb2D.transform.rotation = Quaternion.AngleAxis(180, new Vector2(0, 1));
    }

    public override void LateExecute()
    {
        RaycastHit2D[] hitResults = new RaycastHit2D[2];

        if (rb2D.Cast(new Vector2(0, -1), hitResults, 0.1f) > 0)
            player.ChangeState(new SPlayerIdle(player));
    }
}
