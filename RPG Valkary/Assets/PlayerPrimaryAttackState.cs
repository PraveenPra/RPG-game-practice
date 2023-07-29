using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{

    private int comboCounter; //for 0 1 2 attacks index
    private float lastTimeAttacked;// to compare & do 1attack or next attack in combo
    private float comboWindow = 1f;//u can do combo within 2 sec, after that it starts with first attack


     public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        if(comboCounter > 2 || Time.time > lastTimeAttacked + comboWindow)
        comboCounter = 0;

        player.anim.SetInteger("ComboCounter",comboCounter);

        stateTimer = 0.1f; //movenmt for short bit while attacking(forward,backward push etc..)
    }

    public override void Update()
    {
        base.Update();

        if(triggerCalled)
        stateMachine.ChangeState(player.idleState);

        if(stateTimer < 0)
        rb.velocity = new Vector2(0,0); //no movemnt while attack only 0.1s move
    }

    public override void Exit()
    {
        base.Exit();

        comboCounter++;

        lastTimeAttacked = Time.time;

       player.StartCoroutine("BusyFor",0.15f);
    }
}