using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerController : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private Vector2 inputDirection;
    private Rigidbody2D rb;
    private float speed=100;
    public PlayerActions Actions { get; set; }
    public bool usingKeyboard=false;
    void Awake()
    {
        this.rb = this.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (InputManager.Devices.Count <= 0) {
            this.Actions = PlayerActions.CreateWithKeyboardBindings();
            this.usingKeyboard = true;
        } else {
            if (InputManager.Devices[0] != null) {
                this.Actions = PlayerActions.CreateWithJoystickBindings();
                this.Actions.Device = InputManager.Devices[0];//current one, we need to add with code later
                this.usingKeyboard = false;
            }
        }
    }

    private void Update()
    {
        this.CheckUsingControl();
    }

    #region Controls

    private void CheckUsingControl()
    {
        if (InputManager.Devices.Count <= 0) {
            if (!this.usingKeyboard) {
                this.Actions = PlayerActions.CreateWithKeyboardBindings();
                this.usingKeyboard = true;
            }
        } else {
            if (InputManager.AnyKeyIsPressed && !this.usingKeyboard) {
                this.Actions = PlayerActions.CreateWithKeyboardBindings();
                this.usingKeyboard = true;
            } else if (InputManager.Devices[0] != null && InputManager.Devices[0].IsActive && this.usingKeyboard) {
                this.Actions = PlayerActions.CreateWithJoystickBindings();
                this.Actions.Device = InputManager.Devices[0];//current one, we need to add with code later
                this.usingKeyboard = false;
            }
        }
    }

    #endregion

    void FixedUpdate()
    {
        if (Actions != null) {
            this.Movement();
            if (Actions.shoot.WasPressed) { // this works weird, I think we need to remove the WasPressed and use a delay
                this.Shoot();
            }
        }
    }

    private void Movement()
    {
        horizontal = this.Actions.move.X;
        vertical = this.Actions.move.Y;
        inputDirection = new Vector2(horizontal, vertical).normalized;
        rb.velocity = inputDirection * speed * Time.deltaTime;

    }

    private void Shoot()
    {
        Debug.LogError("Shoot");
    }

}
