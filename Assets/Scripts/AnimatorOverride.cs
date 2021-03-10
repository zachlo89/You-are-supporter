using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorOverride : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetAnimations(AnimatorOverrideController animatorOverride)
    {
        _animator.runtimeAnimatorController = animatorOverride;
    }
}
