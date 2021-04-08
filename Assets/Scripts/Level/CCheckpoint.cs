using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheckpoint : MonoBehaviour
{
    public static List<CCheckpoint> checkpoints;
    public Animator anim;
    public bool isActive = false;

    void Awake()
    {
        //Crea la lista de checkpoints.
        if (checkpoints == null)
        {
            checkpoints = new List<CCheckpoint>();
            checkpoints.Add(this);
        }
        else 
        {
            int index = 0;
            foreach (CCheckpoint checkpoint in checkpoints)
            {
                if (checkpoint == this)
                    break;
                index++;
            }
            if (index == checkpoints.Count)
                checkpoints.Add(this);
        } 
    }

    void Start()
    {
        //Reinicia los checkpoints.
        foreach (CCheckpoint checkpoint in checkpoints)
            if (!checkpoint.enabled)
                checkpoint.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Desactiva los checkpoints ya usados conforme avanzas.
        if (collision.tag == "Player")
        {
            foreach (CCheckpoint checkpoint in checkpoints)
            {
                if (checkpoint.isActive && checkpoint != this)
                {
                    checkpoint.enabled = false;
                    break;
                }
            }

            isActive = true;
            anim.enabled = true;
            LevelManager.Instance.UpdateCheckpoint(this.transform.position);
        }
    }

    void OnDestroy()
    {
        //Elimina los checkpoints de la lista al eliminar la escena, para evitar errores al volver a cargarla sin cerrar el juego.
        checkpoints.Remove(this);
    }
}
