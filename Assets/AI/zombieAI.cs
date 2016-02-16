using UnityEngine;
using System.Collections;
using System;

public class zombieAI : MonoBehaviour {

    public float Distance;
    public Transform Target;
    public float lookAtDistance = 20;
    public float chaseRange = 10;
    public double attackRange = 2.2;
    public float moveSpeed = 3;
    public float Damping = 6;
    public float attackRepeatTime = 1;

    public float TheDammage = 10;

    private float attackTime;
    public CharacterController controller;
    public float gravity = 20;

    private Vector3 MoveDirection = Vector3.zero;

    // Use this for initialization
    void Start() {
        attackTime = Time.time;
        FindHealth();
    }

    void FindHealth()
    {
        Target = GameObject.Find("PlayerStats").GetComponent("PlayerStat").transform;
    }

    // Update is called once per frame
    void Update() {
        Distance = Vector3.Distance(Target.position, transform.position);

        if (Distance < lookAtDistance)
        {
            lookAt();
        }

        if (Distance < attackRange)
        {
            attack();
        }
        else if (Distance < chaseRange)
        {
            chase();
        }
    }

    void attack()
    {
       if(Time.time > attackTime)
        {
            GetComponent<Animator>().Play("attack");
            Target.SendMessage("ApplyDammage", TheDammage);
            Debug.Log("The ennemy has attacked");
            attackTime = Time.time + attackRepeatTime;
        }
    }

    void chase()
    {
        GetComponent<Animator>().Play("walk");
        MoveDirection = transform.forward;
        MoveDirection *= moveSpeed;
        MoveDirection.y -= gravity * Time.deltaTime;
        controller.Move(MoveDirection * Time.deltaTime);
    }

    void lookAt()
    {
        var rotation = Quaternion.LookRotation(Target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Damping);
    }

    void ApplyDammage()
    {
        chaseRange += 30;
        moveSpeed += 2;
        lookAtDistance += 40;
    }
}
