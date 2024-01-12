using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour {
    [SerializeField] private GameEvent gameEvent;
    [SerializeField] private UnityEvent onEventTriggered;

    void OnEnable() {
        gameEvent.AddListener(this);
    }

    void OnDisable() {
        gameEvent.RemoveListener(this);
    }

    public void OnEventTriggered() {
        onEventTriggered.Invoke();
    }
}