using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerControllerScript : MonoBehaviour {

    [SerializeField] private GameEvent onTimerEnd;
    [SerializeField] private GameEvent onTimerStart;

    private Animator animator;
    private AudioManager audioManager;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    void Start() {
        audioManager = AudioManager.instance;
    }

    public void TimerEnd() {
        onTimerEnd.TriggerEvent();
    }

    public void TimerStart() {
        onTimerStart.TriggerEvent();
    }

    public void TimerTick() {
        audioManager.Play("Timer");
    }

}
