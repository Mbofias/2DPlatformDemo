using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBossShooting : SBossState
{
    protected float timeUpToChangePosition = 0f;
    protected float timeToChangePosition = 12f;
    public float timeBetweenShoots = 2f;
    public float timeUpToShoot = 0f;
    public SBossShooting(CBossController boss) : base(boss)
    {

    }

    public override void Execute()
    {
        timeUpToChangePosition += Time.deltaTime;
        timeUpToShoot += Time.deltaTime;
        if (timeUpToShoot >= timeBetweenShoots)
        {
            enemy.SpawnShooting01();
            timeUpToShoot = 0f;
        }
    }
    public override void FixedExecute()
    {
        
    }
    public override void LateExecute()
    {
        
    }

    public override void CheckTranstions()
    {
        if (timeUpToChangePosition >= timeToChangePosition)
        {
            timeUpToChangePosition = 0f;
            enemy.ChangeState(new SBossCrash (enemy));
        }
    }
    public override void OnInit() {
        timeUpToChangePosition = 0f;
        timeUpToShoot = 0f;
        //enemy.shoot01 = true;
    }
    public override void OnExit()
    {
        timeUpToChangePosition = 0f;
        timeUpToShoot = 0f;
        //enemy.shoot01 = false;
    }
}
