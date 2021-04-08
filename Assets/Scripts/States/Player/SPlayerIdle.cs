using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OnLadderState;

public class SPlayerIdle : SPlayerCharacter
{
    protected float idleAnimationTimer, h = 0;
    protected static float ladderStep;
    protected bool jump = false, attack = false;
    protected static LadderPos ladderPos = LadderPos.NONE;
    public SPlayerIdle(CPlayerController controller) : base(controller) { }

    public override void Execute()
    {
        if (idleAnimationTimer > 5f)
        {
            animator.SetTrigger("Idle");
            idleAnimationTimer = 0;
        }
        else
            idleAnimationTimer += Time.deltaTime;

        h = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            rb2D.AddForce(Vector2.up * player.characterData.verticalImpulseFactor, ForceMode2D.Impulse);

            if (!CheckJumpImpulse())
                rb2D.velocity = new Vector2(0, player.characterData.verticalImpulseFactor);
        }
    }

    public override void FixedExecute()
    {
        if (h > 0)
            rb2D.transform.rotation = Quaternion.AngleAxis(0, new Vector2(0, 1));

        if (h < 0)
            rb2D.transform.rotation = Quaternion.AngleAxis(180, new Vector2(0, 1));
    }

    public override void OnInit()
    {
        idleAnimationTimer = 0;
        ladderPos = LadderPos.NONE;
    }

    public override void LateExecute()
    {
        if (ladderPos != LadderPos.NONE)
            player.ChangeState(new OnLadderState(player, ladderPos, ladderStep));

        if (h != 0)
            player.ChangeState(new SPlayerMove(player));

        RaycastHit2D[] hitResults = new RaycastHit2D[2];

        if (rb2D.Cast(new Vector2(0, -1),hitResults, 0.1f) == 0)
            player.ChangeState(new SPlayerJump(player));
    }

    public override void OnTriggerEnter(Collider2D collision)
    {
        base.OnTriggerEnter(collision);
        if (collision.tag == "BottomLadder")
        {
            ladderStep = (collision as CircleCollider2D).radius * 2 * 1.55f;
            ladderPos = LadderPos.BOTTOM;
        }
        if (collision.tag == "TopLadder")
        {           
            ladderStep = (collision as CircleCollider2D).radius * 2 * 1.55f;
            ladderPos = LadderPos.TOP;
        }
    }
}
