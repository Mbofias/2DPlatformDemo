using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Text timer;
    LevelManager levelManager;
    public Animator anim;
    //displays  remaining time of the train phase.
    void Update()
    {
        anim.SetFloat("time", LevelManager.Instance.currentTrainEventCountDown);
        if (LevelManager.Instance.TrainEventPlaying) {
            
            timer.text = LevelManager.Instance.currentTrainEventCountDown.ToString("0.00");
        } else {
            timer.text = "";
        }
            
    }
}
