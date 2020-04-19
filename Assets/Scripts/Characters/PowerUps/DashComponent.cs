using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DashComponent : MonoBehaviour
{
    private float dashDelay=1;
    public float dashDuration=1;
    private bool canDash=true;
    public float dashSpeed;
    public Action<bool> OnDashAction;

    public DashComponent()
    {
        this.dashDuration = 0.1f;
        this.dashSpeed = 4;
    }

    public void TriggerDash(Rigidbody rb)
    {
        if (this.canDash) {
            this.OnDashAction?.Invoke(true);
            this.canDash = false;
            StartCoroutine(Dashing(rb));
        }
    }

    IEnumerator Dashing(Rigidbody rb)
    {
        rb.velocity *= dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        this.OnDashAction?.Invoke(false);
        StartCoroutine(WaitToCanDash());
    }

    IEnumerator WaitToCanDash()
    {
        yield return new WaitForSeconds(this.dashDelay);
        this.canDash = true;
    }

}
