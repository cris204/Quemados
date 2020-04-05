using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    public float movementSpeed;
    private Rigidbody rigidbody;

    private bool canMove;

    void Awake()
    {
        this.rigidbody = this.gameObject.GetComponent<Rigidbody>();
        this.canMove = false;
    }

    private void Update()
    {
        this.CheckCanMove();
    }

    void FixedUpdate()
    {
        if (this.canMove) {
            this.Move();
        }
    }

    private void OnDisable()
    {
        this.rigidbody.velocity = Vector3.zero;
    }

    private void Move()
    {
      
        
        Vector3 moveVector = (this.moveTarget.position - this.ownTransform.position).normalized;
        moveVector.y = 0;
        this.rigidbody.velocity = moveVector * this.movementSpeed * Time.fixedDeltaTime;
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
