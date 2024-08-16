using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    
    public EnemyStateMachine stateMachine { get; private set; }
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }

    // Update is called once per frame
   protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }
}
