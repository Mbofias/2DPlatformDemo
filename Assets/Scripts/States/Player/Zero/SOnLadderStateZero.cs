using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOnLadderStateZero : SPlayerMoveZero
{
    float v;
    public SOnLadderStateZero(CPlayerController controller, LadderPos ladderPos, float ladderStep) : base(controller)
    {
        SOnLadderStateZero.ladderPos = ladderPos;
        SOnLadderStateZero.ladderStep = ladderStep;
    }

    public override void Execute()
    {
        if (rb2D.gravityScale != 0)
        {
            base.Execute();
            if (h == 0)
                animator.SetBool("Running", false);
            else
                animator.SetBool("Running", true);
        }

        v = Input.GetAxis("Vertical");

        switch (ladderPos)
        {
            case LadderPos.TOP:
                if (v < 0)
                {
                    rb2D.gravityScale = 0;
                }
                else
                    v = 0;
                break;
            case LadderPos.BOTTOM:
                if (v > 0)
                {
                    rb2D.gravityScale = 0;
                }
                else
                    v = 0;
                break;
            case LadderPos.MIDDLE:
                break;
        }
    }

    public override void FixedExecute()
    {
        if (rb2D.gravityScale != 0) 
            base.FixedExecute();
        else
        {       
            if (ladderPos == LadderPos.BOTTOM || ladderPos == LadderPos.TOP)
            {
                rb2D.position += new Vector2(0, Mathf.Sign(v) * ladderStep);
                ladderPos = LadderPos.MIDDLE;
            }
            if (ladderPos == LadderPos.MIDDLE)
                rb2D.velocity = new Vector2(0, player.characterData.ladderSpeed * v);
        }
    }

    public override void LateExecute()
    {
        if (ladderPos == LadderPos.NONE)
        {
            player.ChangeState(new SPlayerIdleZero(player));
        }
    }

    public override void OnTriggerEnter(Collider2D collsion)
    {
        if (collsion.tag == "BottomLadder")
        {
            rb2D.gravityScale = 1;
            ladderPos = LadderPos.BOTTOM;
        }
        if (collsion.tag == "TopLadder")
        {
            rb2D.position += new Vector2(0, ladderStep);
            rb2D.gravityScale = 1;
            ladderPos = LadderPos.TOP;
        }
    }

    public override void OnTriggerExit(Collider2D collsion)
    {
        if (collsion.tag == "BottomLadder" && ladderPos == LadderPos.BOTTOM)
        {
            ladderPos = LadderPos.NONE;
        }
        if (collsion.tag == "TopLadder" && ladderPos == LadderPos.TOP)
        {
            ladderPos = LadderPos.NONE;
        }
    }
}
