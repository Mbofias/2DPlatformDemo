using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBossPatrol : SBossState
{
    private bool movePoint03Arrived, movePoint04Arrived,initPointArrived;
    private float timeUpToShoot = 0f,timeBetweenShoots=1f;
    public SBossPatrol(CBossController boss) : base(boss)
    {

    }

    public override void Execute()
    {
        if (movePoint03Arrived && !movePoint04Arrived)
        {
            timeUpToShoot += Time.deltaTime;
            if (timeUpToShoot >= timeBetweenShoots)
            {
                enemy.SpawnShooting02();
                timeUpToShoot = 0f;
            }
        }
    }

    public override void FixedExecute()
    {
        if (Vector2.Distance(rb2D.transform.position, enemy.movePoint03.position) > 0.01f && !movePoint03Arrived)
        {
            rb2D.transform.position = Vector2.MoveTowards(enemy.transform.position, new Vector2(enemy.movePoint03.position.x, enemy.movePoint03.position.y), enemy.enemyData.VerticalSpeed * Time.deltaTime);
        }
        if(Vector2.Distance(rb2D.transform.position, enemy.movePoint04.position) > 0.01f && !movePoint04Arrived && movePoint03Arrived)
        {
            enemy.transform.localScale = new Vector3(-1, 1, 1);
            rb2D.transform.position = Vector2.MoveTowards(enemy.transform.position, new Vector2(enemy.movePoint04.position.x, enemy.movePoint04.position.y), enemy.enemyData.horizontalSpeed * Time.deltaTime);
        }
        if(Vector2.Distance(rb2D.transform.position, enemy.initPoint.position) > 0.01f && !initPointArrived && movePoint04Arrived)
        {
            rb2D.transform.position = Vector2.MoveTowards(enemy.transform.position, new Vector2(enemy.initPoint.position.x, enemy.initPoint.position.y), enemy.enemyData.VerticalSpeed * Time.deltaTime);
        }
    }

    public override void LateExecute()
    {
        if (Vector2.Distance(rb2D.transform.position, enemy.movePoint03.position) <= 0.01f)
            movePoint03Arrived = true;
        if (Vector2.Distance(rb2D.transform.position, enemy.movePoint04.position) <= 0.01f && movePoint03Arrived)
        {
            enemy.transform.localScale = new Vector3(1, 1, 1);
            movePoint04Arrived = true;
        }
        if(Vector2.Distance(rb2D.transform.position, enemy.initPoint.position) <= 0.01f && movePoint04Arrived)
        {
            initPointArrived = true;
        }
    }

    public override void CheckTranstions()
    {
        if (initPointArrived)
        {
            enemy.ChangeState(new SBossShooting(enemy));
        }
    }
    public override void OnInit()
    {
        movePoint03Arrived = false;
        movePoint04Arrived = false;
        initPointArrived = false;
    }

    public override void OnExit()
    {
        movePoint03Arrived = false;
        movePoint04Arrived = false;
        initPointArrived = true;
    }
}
