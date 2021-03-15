using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopUpAniamation : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("DamagePopUp", -1, Random.Range(0.0f, 2.0f));
    }

}
