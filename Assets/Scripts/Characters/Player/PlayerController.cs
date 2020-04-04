using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerController : MonoBehaviour
{
    public CharacterComponents components;

    private float horizontal;
    private float vertical;
    private Vector3 inputDirection;
    private Rigidbody rb;
    [SerializeField] private float speed=300;
    public PlayerActions Actions { get; set; }
    public bool usingKeyboard=false;

    [Header("Aim")]
    public GameObject aimGO;
    private float aimDistanceFromCenter = 3.5f;
    private Vector3 aimDirection;
    private Vector3 mousePos;
    private Camera cameraMain;
    public float offset; //Probs we need offset in X and Z, in this case I use the same cuz I added the same offset in SpawnPoint position


    void Awake()
    {
        this.rb = this.GetComponent<Rigidbody>();
        this.aimDirection = new Vector3(aimDistanceFromCenter,0,0);
        this.cameraMain = Camera.main;
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
            this.Aiming();

            if (Actions.shoot) { // this works weird, I think we need to remove the WasPressed and use a delay
                this.Shoot();
            }
        }
    }

    #region Movement
    private void Movement()
    {
        horizontal = this.Actions.move.X;
        vertical = this.Actions.move.Y;
        inputDirection = new Vector3(horizontal,0, vertical).normalized;
        rb.velocity = inputDirection * speed * Time.fixedDeltaTime;

    }
    #endregion

    #region Shoot and Aim
    private void Shoot()
    {
        this.components.Attack.Attack(PowerType.BasicThrowBall, this.components, this.aimDirection.normalized); //(playerController.aim.transform.position - transform.position).normalized
        Debug.LogError("Shoot");
    }
    private void Aiming()
    {

        if (usingKeyboard) {
            mousePos = cameraMain.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
            this.aimGO.transform.localPosition = new Vector3(mousePos.x,0, mousePos.z + mousePos.y);
            mousePos.x -= offset;
            mousePos.z += (mousePos.y + offset);
            mousePos.y = 0;
            this.aimDirection = mousePos;
        } else {
            this.aimDirection = new Vector3(this.Actions.aimMove.X, 0, this.Actions.aimMove.Y);
            if (this.aimDirection.magnitude > 0.5f) {
                this.aimDirection.Normalize();
                this.aimDirection *= aimDistanceFromCenter;
                this.aimGO.transform.localPosition = this.aimDirection;
                this.aimDirection.x -= offset;
                this.aimDirection.z += offset;
            } else {
                this.aimDirection = new Vector3(this.aimGO.transform.localPosition.x - offset, 0, this.aimGO.transform.localPosition.z+ offset);
            }
        }
    }
    #endregion
}
