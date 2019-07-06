using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBehaviour : StateMachineBehaviour
{

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<PlayerController>().lockAttack = false;
        animator.gameObject.GetComponent<PlayerController>().lockJump = false;
        animator.gameObject.GetComponent<PlayerController>().lockMove = false;
    }
}
