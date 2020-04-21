using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveState : EnemyState
{
    public float movementSpeed;
    private NavMeshAgent navAgent;
    private Rigidbody rigidbody;

    private bool canMove;

    private float checkUpdateTimer;
    private const int timeToUpdateTarget = 1;

    void Awake()
    {
        this.rigidbody = this.gameObject.GetComponent<Rigidbody>();
        this.navAgent = this.gameObject.GetComponent<NavMeshAgent>();
        this.navAgent.updateRotation = false;
        this.canMove = false;
    }
    private void Start()
    {
        this.navAgent.speed = this.movementSpeed;
    }

    public override void ChangeMoveTarget(Transform newTarget)
    {
        base.ChangeMoveTarget(newTarget);
        this.navAgent.SetDestination(newTarget.position);
    }

    public override void EnterState()
    {
        base.EnterState();
        this.navAgent.isStopped = false;
        this.UpdateState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        this.CheckCanMove();
        if (this.checkUpdateTimer < Time.time) {
            this.checkUpdateTimer = Time.time + timeToUpdateTarget;
            if (this.canMove) {
                this.Move();
            }
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        this.rigidbody.velocity = Vector3.zero;
        this.navAgent.isStopped = true;
    }

    private void Move()
    {
        this.navAgent.SetDestination(this.moveTarget.position);

        //Vector3 moveVector = (this.moveTarget.position - this.ownTransform.position).normalized;
        //moveVector.y = 0;
        //this.rigidbody.velocity = moveVector * this.movementSpeed * Time.fixedDeltaTime;
    }

    private void CheckCanMove()
    {
        if (this.moveTarget == null) {
            this.canMove = false;
            return;
        } else {
            this.canMove = true;
        }
    }

}
