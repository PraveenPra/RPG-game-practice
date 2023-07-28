using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
        //call the player anim trig from the animator event aftr finish, this will set the bool to true in player state, attack state reads this and stops attack after playing once. - to solve looping of attack 
    }
}
