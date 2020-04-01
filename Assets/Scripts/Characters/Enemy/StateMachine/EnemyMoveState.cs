using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    private Transform currentTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeTarget(Transform newTarget)
    {
        this.currentTarget = newTarget;
    }

    // Update is called once per frame
    void Update()
    {
        this.Move();
    }


    private void Move()
    {

    }
}
