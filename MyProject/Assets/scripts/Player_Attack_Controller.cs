using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player_Attack_Controller : NetworkBehaviour
{
    enum State { movementState, attackState, deathState, idleState };
    private State currentState;

    public Collider hitBoxCollider;

    private Animator animationComponent;

    public float HP = 100;
    private float currentHP;
    private float bounceForce;

    bool playerAttackEnabled = true;
    int playerAttackingHash;
    int playerSpawnedHash;
    int HPHash;

    void Awake()
    { 
        animationComponent = GetComponent<Animator>();
        currentHP = HP;
    }

    void Start()
    {
        Change_to_Normla_BounceForce();
        Player_Attack_HitBox_Disabled();
        Animation_Controller(State.movementState);
        playerSpawnedHash = Animator.StringToHash("GameStarted");
        playerAttackingHash = Animator.StringToHash("Attack");
        HPHash = Animator.StringToHash("HP");
    }

    void Animation_Controller(State _state)
    {
        if (_state == State.movementState && currentHP > 0)
        {
            animationComponent.SetTrigger(playerSpawnedHash);
        }
        if (_state == State.attackState && currentHP > 0)
        {
            animationComponent.SetTrigger(playerAttackingHash);
        }
        if (_state == State.deathState && currentHP <= 0)
        {
            //Conecten con el sistema de respawn o lo que sea que tenga que pasar cuando el player se muera
        }
    }

    void Update()
    {
        if (Input.GetKey("space"))
        {
            if (playerAttackEnabled == true)
            {
                Animation_Controller(State.attackState);
            }
        }

        animationComponent.SetFloat(HPHash, currentHP);
    }
    
    private IEnumerator Attack_Timer()
    {
        WaitForSeconds wait = new WaitForSeconds(2.5f);

        while (true)
        {
            yield return wait;
            playerAttackEnabled = true;
        }

    }

    void Attack_Animation_Ended()
    {
        Animation_Controller(State.movementState);
        StartCoroutine(Attack_Timer());
    }

    void Damage_Taken(Collision other)
    {
        if(other.transform.tag == "Player")
        {
            currentHP -= 2;
        }
        else if(other.transform.tag == "Hit_Box")
        {
            currentHP -= 5;
        }
        if (currentHP <= 0)
        {
            currentHP = 0;
            Animation_Controller(State.deathState);
        }
        animationComponent.SetFloat(HPHash, currentHP);
    }

    void Deactivate_Attack()
    {
        playerAttackEnabled = false;
    }

    void Change_to_Normla_BounceForce()
    {
        bounceForce = 350;
    }

    void Change_to_Attack_BounceFoce()
    {
        bounceForce = 500;
    }

    void Player_Attack_HitBox_Enabled()
    {
        hitBoxCollider.enabled = true;
    }

    void Player_Attack_HitBox_Disabled()
    {
        hitBoxCollider.enabled = false;
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag == "Player" || other.transform.tag == "Hit_Box")
        {
            Damage_Taken(other);

            Rigidbody collidedRigidBody = other.rigidbody;

            collidedRigidBody.AddExplosionForce(bounceForce, other.contacts[0].point, 20);
        }
    }

}
