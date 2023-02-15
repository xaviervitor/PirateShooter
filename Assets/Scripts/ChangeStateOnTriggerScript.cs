using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStateOnTriggerScript : MonoBehaviour {

    public GameObject Ship;
    public EnemyStateMachine.EnemyState State = EnemyStateMachine.EnemyState.Idle;
    public EnemyStateMachine.EnemyState RevertState = EnemyStateMachine.EnemyState.Idle;

    private EnemyStateMachine stateMachine;
    
    void Start() {
        stateMachine = Ship.GetComponent<EnemyStateMachine>();
    }
    
    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag != "Player") return;
        stateMachine.ChangeState(State);
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.tag != "Player") return;
        stateMachine.ChangeState(RevertState);
    }
}