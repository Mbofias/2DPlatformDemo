using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Model to store the dialog you want to display 
[CreateAssetMenu(fileName = "Dialog", menuName = "Dialog/Dialog")]
public class Dialog : ScriptableObject
{
    public string[] sentences;
}
