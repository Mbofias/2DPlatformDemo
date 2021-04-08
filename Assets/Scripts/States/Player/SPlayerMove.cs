using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerMove : SPlayerIdle
{
    public SPlayerMove(CPlayerController controller) : base(controller) { }
    
    public override void Execute()
    {
        h = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(h));
        animator.SetBool("Running", true);

        if (Input.GetButtonDown("Jump"))
        {
            rb2D.AddForce(Vector2.up * player.characterData.verticalImpulseFactor, ForceMode2D.Impulse);
            
            if (!CheckJumpImpulse())
                rb2D.velocity = new Vector2(h * player.characterData.horizontalSpeed, player.characterData.verticalImpulseFactor);
        }

        if (Input.GetButtonDown("Attack"))
            attack = true;
    }

    public override void FixedExecute()
    {
        rb2D.velocity = new Vector2(h * player.characterData.horizontalSpeed, rb2D.velocity.y);

        if (h > 0)
            rb2D.transform.rotation = Quaternion.AngleAxis(0, new Vector2(0, 1));

        if (h < 0)
            rb2D.transform.rotation = Quaternion.AngleAxis(180, new Vector2(0, 1));
    }

    public override void OnInit() { 
        
    }
    public override void OnExit()
    {
        base.OnExit();
    }
    public override void LateExecute()
    {
        if (h == 0)
            player.ChangeState(new SPlayerIdle(player));

        RaycastHit2D[] hitResults = new RaycastHit2D[2];

        if (rb2D.Cast(new Vector2(0, -1), hitResults, 0.1f) == 0)
           player.ChangeState(new SPlayerJump(player));
    }
}
