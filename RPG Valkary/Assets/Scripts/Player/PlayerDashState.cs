using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{

    private float dashAcceleration = 60f; // Adjust this value to control the acceleration rate.
    private float currentDashSpeed;
    private bool isDashing;
//#

    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = 0.6f;//perform dash for 0.4sec

        // Start the dash with acceleration #
        isDashing = true;
        // currentDashSpeed = 0f; // Reset the current dash speed.#
        currentDashSpeed = player.moveSpeed/2;
    }

    public override void Update()
    {
        base.Update();

        if (!player.IsGroundDetected() && player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState);

        // player.SetVelocity((player.moveSpeed + player.dashSpeed) * player.dashDir, 0);

        // Check if we are still dashing#
        if (isDashing)
        {
            // Gradually increase the current dash speed towards the target dash speed.
            currentDashSpeed = Mathf.MoveTowards(currentDashSpeed, player.dashSpeed, dashAcceleration * Time.deltaTime);

            // Set the velocity with the current dash speed (y velocity remains 0 for a horizontal dash).
            player.SetVelocity(currentDashSpeed * player.dashDir, 0f);

            // If the current dash speed reaches the target dash speed, stop dashing.
            if (Mathf.Approximately(currentDashSpeed, player.dashSpeed))
            {
                isDashing = false;
            }
        }//#


        if (stateTimer < 0)//after 0.4sec stop the dash ability
            stateMachine.ChangeState(player.idleState);


    }

    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(0, rb.velocity.y);

        // Reset the current dash speed and dashing flag when exiting the dash state.#
        currentDashSpeed = 0f;
        isDashing = false;
    }
}
