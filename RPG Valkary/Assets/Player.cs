using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   public PlayerStateMachine stateMachine{get; private set;}

   public PlayerIdleState idleState{get; private set;}
   public PlayerMoveState moveState{get; private set;}

   private void Awake()
   {
    stateMachine = new PlayerStateMachine();

    idleState = new PlayerIdleState(this, stateMachine, "Idle");
    moveState = new PlayerMoveState(this,stateMachine,"Move");
   }

   private void Start() {
    stateMachine.Initialize(idleState);
   }

   private void Update() {
    //because we wont use monobehaviours in the PlayerState & stateMachine classes, we need to pass the update from the play because it has the monobehaviour.
    // Less monobehavior in the game, the better
    stateMachine.currentState.Update();
   }

}
