using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBossCrash : SBossState
{
    private bool movePoint01Arrived, movePoint02Arrived;
    public SBossCrash(CBossController boss) : base(boss)
    {

    }

    public override void Execute()
    {
        
    }

    public override void FixedExecute()
    {
        if (Vector2.Distance(rb2D.transform.position, enemy.movePoint01.position) > 0.01f&& !movePoint01Arrived)
        {
            rb2D.transform.position = Vector2.MoveTowards(enemy.transform.position, new Vector2(enemy.movePoint01.position.x, enemy.movePoint01.position.y), enemy.enemyData.VerticalSpeed * Time.deltaTime);
        }
        if(Vector2.Distance(rb2D.transform.position, enemy.movePoint02.position) > 0.01f && !movePoint02Arrived&& movePoint01Arrived)
        {
            rb2D.transform.position = Vector2.MoveTowards(enemy.transform.position, new Vector2(enemy.movePoint02.position.x, enemy.movePoint02.position.y), enemy.enemyData.horizontalSpeed * Time.deltaTime);
        }
        
    }

    public override void LateExecute()
    {
        if (Vector2.Distance(rb2D.transform.position, enemy.movePoint01.position) <= 0.01f)
            movePoint01Arrived = true;
        if (Vector2.Distance(rb2D.transform.position, enemy.movePoint02.position) <= 0.01f && movePoint01Arrived)
        {
            movePoint02Arrived = true;
        }
    }

    public override void CheckTranstions()
    {
        if(movePoint02Arrived)
        {
            enemy.ChangeState(new SBossPatrol (enemy));
        }
    }

    public override void OnInit()
    {
        movePoint01Arrived = false;
        movePoint02Arrived = false;
    }

    public override void OnExit()
    {
        movePoint01Arrived = false;
        movePoint02Arrived = false;
    }
}
