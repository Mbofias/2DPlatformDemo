using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEnemyPatrol : SEnemyState
{
    //public bool shoot;
    public SEnemyPatrol(CEnemyController enemy) : base(enemy)
    {

    }
    public override void OnInit()
    {
        //shoot = false;
    }
    public override void Execute() 
    {
        /*float target_dist = (enemy.transform.position - target.position).sqrMagnitude;
        if (target_dist < enemy.sqrRangeVision)
            shoot = true;*/
    }

    public override void FixedExecute() 
    {
        if (enemy.faceRight)
        {
            rb2D.velocity = new Vector2(enemy.enemyData.horizontalSpeed, rb2D.velocity.y);
            if (enemy.transform.position.x > enemy.rightPoint.transform.position.x)
            {
                enemy.transform.localScale = new Vector3(1, 1, 1);
                enemy.faceRight = false;
            }
        }
        else
        {
            rb2D.velocity = new Vector2(-enemy.enemyData.horizontalSpeed, rb2D.velocity.y);
            if (enemy.transform.position.x < enemy.leftPoint.transform.position.x)
            {
                enemy.transform.localScale = new Vector3(-1, 1, 1);
                enemy.faceRight = true;
            }
        }
    }

    public override void LateExecute() {

        if(enemy.shoot)
        {
            enemy.ChangeState(new SEnemyAttack (enemy));
        }
    
    
    }
}
