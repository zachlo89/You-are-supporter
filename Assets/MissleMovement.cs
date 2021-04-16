using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform target;
    private Rigidbody2D rb;
    private CharacterBattle hero;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right * speed;
    }
    public void SetTarget(Transform target, CharacterBattle hero)
    {
        this.target = target;
        this.hero = hero;
    }

    private void Update()
    {
        if(target != null)
        {
            if (target.position.x <= transform.position.x)
            {
                hero.NormalAttackEffect();
                Destroy(gameObject);
            }
        }
    }

}
