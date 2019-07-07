﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowStarOver : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ObjectPool.Instance.UnSpawn(animator.gameObject);
    }
}
