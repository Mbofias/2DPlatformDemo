using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CZeroAttackController : MonoBehaviour
{
    public ZeroAttackModel attackData;
    private List<GameObject> hitObjects;

    void Start()
    {
        hitObjects = new List<GameObject>();
        attackData = Instantiate(attackData);

        switch (transform.parent.transform.parent.GetComponent<CPlayerController>().currentComboZero)
        {
            case 1:
                attackData.damage *= 1f;
                break;
            case 2:
                attackData.damage *= 1.5f;
                break;
            case 3:
                attackData.damage *= 2f;
                break;
            default: break;
        }

        Invoke("DestroyAttack", attackData.duration);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        bool hasToHit = true;
        foreach(GameObject current in hitObjects)
            if (current == collision.gameObject)
                hasToHit = false;

        if (hasToHit)
        {
            if (collision.tag == "Missile")
                collision.GetComponent<BossShoot01Controller>().DestroyMissile();

            if (collision.tag == "Enemy")
                collision.GetComponent<CEnemyController>().TakeDamage(attackData.damage);

            if (collision.tag == "Boss")
                collision.GetComponent<CBossController>().TakeDamage(attackData.damage);

            hitObjects.Add(collision.gameObject);
        }
    }

    private void DestroyAttack()
    {
        GameObject.Destroy(gameObject);
    }
}
