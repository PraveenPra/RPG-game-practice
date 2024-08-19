using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected Enemy enemyBase;
    protected EnemyStateMachine stateMachine;
    protected Rigidbody2D rb;
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

        rb = enemyBase.rb;
    }

    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBoolName, false);
    }
}
//template for states===========================
//  private Enemy_Skeleton enemy;

//     public SkeletonBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,Enemy_Skeleton _enemy) : base(_enemy, _stateMachine, _animBoolName)
//    {
//         this.enemy = _enemy;

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