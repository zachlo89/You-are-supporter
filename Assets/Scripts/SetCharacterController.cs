using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCharacterController : MonoBehaviour
{
    [SerializeField] private AnimatorOverrideController[] overriderControllers;
    [SerializeField] private AnimatorOverride overrider;

    public void Set(int value)
    {
        overrider.SetAnimations(overriderControllers[value]);
    }
}
