using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLadderState : SPlayerIdle
{
    private float v;

    public OnLadderState(CPlayerController controller, LadderPos ladderPos, float ladderStep) : base(controller)
    {
        OnLadderState.ladderPos = ladderPos;
        OnLadderState.ladderStep = ladderStep;
    }

    public override void OnInit()
    {
        Debug.Log("Entrar ladder state");
    }

    public override void Execute()
    {
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
                animator.SetFloat("Speed", v);
                break;
        }

        if (rb2D.gravityScale != 0)
            base.Execute();

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
                //animator.SetBool("ladder", true);
            }
            if (ladderPos == LadderPos.MIDDLE)
                rb2D.velocity = new Vector2(0, player.characterData.ladderSpeed * v);
        }
    }

    public override void LateExecute()
    {
        if (ladderPos == LadderPos.NONE)
        {
            //player.ChangeState(new SPlayerIdle(player));
            switch (player.characterData.type)
            {
                case EPlayerType.X:
                    player.ChangeState(new SPlayerIdleX(player));
                    break;
                case EPlayerType.ZERO:
                    player.ChangeState(new SPlayerIdleZero(player));
                    break;
                default: break;
            }
        }
    }

    public override void OnTriggerEnter(Collider2D collsion)
    {
        if (collsion.tag == "BottomLadder")
        {
            rb2D.gravityScale = 1;
            //animator.SetBool("ladder", false);
            ladderPos = LadderPos.BOTTOM;
        }
        if (collsion.tag == "TopLadder")
        {
            rb2D.position += new Vector2(0, ladderStep);
            rb2D.gravityScale = 1;
            //animator.SetBool("ladder", false);
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
