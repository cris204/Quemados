using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour //https://www.youtube.com/watch?v=Gwc4VCGEuBM tutorial
{
    public Transform lookAt;
    public float boundX = 2.0f;
    public float boundY = 1.5f;


    [Header("CameraMove")]
    private Vector3 delta;
    private float dx;
    private float dy;
    private Vector3 desiredPosition;
    [SerializeField] private float speed = 0.1f;


   private void LateUpdate()
    {
        if (this.lookAt == null)
            return;

        this.delta = Vector3.zero;
        this.dx = this.lookAt.position.x - transform.position.x;
        if(this.dx > this.boundX || this.dx < -this.boundX) {
            if (transform.position.x < lookAt.position.x) {
                this.delta.x = this.dx - this.boundX;
            } else {
                this.delta.x = this.dx + this.boundX;
            }
        }

         this.dy = this.lookAt.position.z - transform.position.z;
        if (this.dy > this.boundY || this.dy < -this.boundY) {
            if (transform.position.z < this.lookAt.position.z) {
                this.delta.z = this.dy - this.boundY;
            } else {
                this.delta.z = this.dy + this.boundY;
            }
        }

        //Move

        this.desiredPosition = transform.position + delta;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, speed);

    }
}