using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEnemyAttack : SEnemyState
{
    public SEnemyAttack(CEnemyController enemy) : base(enemy)
    {
        this.enemy = enemy;
    }
    public override void OnInit()
    {
        enemy.startToAttack = true;
    }
    public override void OnExit()
    {
        enemy.startToAttack = false;
    }
    public override void Execute()
    {
        if (enemy.faceToPlayer)
        {
            if (target.position.x > enemy.transform.position.x)
            {
                enemy.transform.localScale = new Vector3(-1, 1, 1);
            }
            if (target.position.x < enemy.transform.position.x)
            {
                enemy.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public override void FixedExecute()
    {
        
    }

    public override void LateExecute()
    {
        /*float target_dist = (enemy.transform.position - target.position).sqrMagnitude;
        if (target_dist > enemy.sqrRangeVision)
            enemy.ChangeState(new SEnemyPatrol (enemy));*/
        if(!enemy.shoot)
            enemy.ChangeState(new SEnemyPatrol(enemy));
    }
    

}
