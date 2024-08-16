using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState 
{
    protected Enemy enemyBase;
    protected EnemyStateMachine stateMachine;

    private string animBoolName;

protected float stateTimer;
    protected bool triggerCalled;
    public EnemyState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName)
    {
        this.enemyBase = _enemyBase;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }  

    public virtual void Update()
    {
       stateTimer -= Time.deltaTime;
    }
    public virtual void Enter()
    {
        triggerCalled = false;
        enemyBase.anim.SetBool(animBoolName, true);
    }

    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBoolName, false);
    }
}
//template for states===========================
// public SkeletonIdleState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
//    {

//     }

//     public override void Enter()
//     {
//         base.Enter();
//     }

//     public override void Update()
//     {
//         base.Update();
//     }

//     public override void Exit()
//     {
//         base.Exit();
//     }