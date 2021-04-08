using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    public ECollectableType type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            switch(type)
            {
                case ECollectableType.BOMB:
                    GameManager.Instance.inventory.bomb.PickUp();
                    break;
                case ECollectableType.AERIAL:
                    GameManager.Instance.inventory.aerialAttack.PickUp();
                    break;
                case ECollectableType.OVERLOAD:
                    GameManager.Instance.inventory.energyOverload.PickUp();
                    break;
                default: break;
            }

            Destroy(gameObject);
        }
    }
}
