using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Dialog))]
public class Dialog1 : MonoBehaviour
{   
    //Requires a dialog model to send the sentences to de UIDialogController 
    //when detects collision with player

    public int dialogIndex;
    public Dialog dialog;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            UIDialogController.Instance.DisplayDialog(dialog.sentences);
            Destroy(this.gameObject);
        }
    }

}
