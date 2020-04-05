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
        this.canMove = true;
    }

    // Update is called once per frame
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
        if(this.moveTarget == null) {
            this.canMove = false;
            return;
        }

        Vector3 moveVector = (this.moveTarget.position - this.ownTransform.position).normalized;
        moveVector.y = 0;
        this.rigidbody.velocity = moveVector * this.movementSpeed * Time.fixedDeltaTime;
    }
}
