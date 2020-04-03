using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    public float movementSpeed;
    private Rigidbody rigidbody;

    void Awake()
    {
        this.rigidbody = this.gameObject.GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
        this.Move();
    }

    private void OnDisable()
    {
        this.rigidbody.velocity = Vector3.zero;
    }

    private void Move()
    {
        Vector3 moveVector = (this.moveTarget.position - this.ownTransform.position).normalized;
        moveVector.y = 0;
        this.rigidbody.velocity = moveVector * this.movementSpeed * Time.deltaTime;
    }
}
