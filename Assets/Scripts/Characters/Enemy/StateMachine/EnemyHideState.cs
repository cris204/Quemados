using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHideState : EnemyState
{

    private EnemyHideOut currentHideOut;

    private const byte timeToSearchHideOut = 3;
    private float searchHideOutTimer;

    public bool CanSearchHideOut()
    {
        return this.searchHideOutTimer < Time.time;
    }

    public override void EnterState()
    {
        base.EnterState();
        if (this.SearchForHideOut()) {
            this.UpdateState();
        } else {
            this.searchHideOutTimer = Time.time + timeToSearchHideOut;
            this.ExitState();
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(this.currentHideOut == null) {
            this.ExitState();
            return;
        }
        this.ChangeMoveTarget(this.GetHideOutPosition());
    }

    private Transform GetHideOutPosition()
    {
        Transform hideOutFakeTransform = this.currentHideOut.GetHideOutObject().transform;
        //hideOutFakeTransform.position
        return hideOutFakeTransform;
    }

    private bool SearchForHideOut()
    {
        this.currentHideOut = EnemiesGameStateManager.Instance.GetHideOut();
        if (this.currentHideOut != null)
            return true;
        return false;
    }
}
