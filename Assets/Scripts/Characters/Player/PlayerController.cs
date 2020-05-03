using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerController : MonoBehaviour
{
    public CharacterComponents components;
    public CharacterType character;

    private float horizontal;
    private float vertical;
    private Vector3 inputDirection;
    private Rigidbody rb;
    private Animator playerAnim;
    [SerializeField] private float speed=300;
    public PlayerActions Actions { get; set; }

    [Header("Shoot and Aim")]
    public GameObject aimGO;
    private float aimDistanceFromCenter = 12f;
    private Vector3 aimDirection;
    private Vector3 mousePos;
    private Camera cameraMain;
    public float offset; //Probs we need offset in X and Z, in this case I use the same cuz I added the same offset in SpawnPoint position
    private WaitForSeconds attackDelay;
    public float attackCoolDown=0.2f;
    private bool canAttack = true;
    public bool CanAttack
    {
        get => this.canAttack;
        set => this.canAttack = value;
    }
    private bool isDashing;

    void Awake()
    {
        this.rb = this.GetComponent<Rigidbody>();
        this.playerAnim = this.GetComponent<Animator>();
        this.aimDirection = new Vector3(aimDistanceFromCenter,0,0);
        this.cameraMain = Camera.main;

        EventManager.Instance.AddListener<KilledEnemyEvent>(this.OnkillEnemy);
    }

    private void OnDestroy()
    {
        if (EventManager.HasInstance()) {
            EventManager.Instance.RemoveListener<KilledEnemyEvent>(this.OnkillEnemy);
        }
    }

    private void Start()
    {
        if (InputManager.Devices.Count <= 0) {
            this.Actions = PlayerActions.CreateWithKeyboardBindings();
            Env.IS_USING_KEYBOARD = true;
        } else {
            if (InputManager.Devices[0] != null) {
                this.Actions = PlayerActions.CreateWithJoystickBindings();
                this.Actions.Device = InputManager.Devices[0];//current one, we need to add with code later
                Env.IS_USING_KEYBOARD = false;
            }
        }
        this.attackDelay = new WaitForSeconds(this.attackCoolDown);
        this.components.character = this.character;
        this.components.SuscribeDeathAction(this.Death);
        this.SetDashAction();
    }
    private void Update()
    {
        this.CheckUsingControl();
        this.PressPause();
    }

    #region Controls

    private void CheckUsingControl()
    {
        if (InputManager.Devices.Count <= 0) {

            if (!Env.IS_USING_KEYBOARD) {
                this.Actions = PlayerActions.CreateWithKeyboardBindings();
                Env.IS_USING_KEYBOARD = true;
            }
        } else {

            if ((Input.GetMouseButtonDown(0) || InputManager.AnyKeyIsPressed) && !Env.IS_USING_KEYBOARD) {
                this.Actions = PlayerActions.CreateWithKeyboardBindings();
                Env.IS_USING_KEYBOARD = true;

            } else if (InputManager.Devices[0] != null && InputManager.Devices[0].IsActive && Env.IS_USING_KEYBOARD) {
                this.Actions = PlayerActions.CreateWithJoystickBindings();
                this.Actions.Device = InputManager.Devices[0];//current one, we need to add with code later
                Env.IS_USING_KEYBOARD = false;
            }

        }
    }

    #endregion

    void FixedUpdate()
    {
        if (Actions != null) {
            this.Movement();
            this.Aiming();

            if (Actions.shoot.IsPressed) {
                this.Shoot();
            }
            if (Actions.dash) {
                this.Dash();
            }
        }
    }

    #region Movement
    private void Movement()
    {
        if (!this.isDashing) {
            horizontal = this.Actions.move.X;
            vertical = this.Actions.move.Y;
            inputDirection = new Vector3(horizontal, 0, vertical).normalized;
            rb.velocity = inputDirection * speed * Time.fixedDeltaTime;

            if (this.rb.velocity.magnitude != 0) {
                this.playerAnim.SetFloat("MoveMagnitude", this.aimGO.transform.localPosition.magnitude - Vector3.Distance(rb.velocity,this.aimGO.transform.localPosition));
            } else {
                this.playerAnim.SetFloat("MoveMagnitude", this.rb.velocity.magnitude);
            }

        }
    }
    #endregion

    #region Shoot and Aim
    private void Shoot()
    {
        if (!this.CanAttack)
            return;

        this.CanAttack = false;
        this.components.Attack.Attack(PowerType.BasicThrowBall, this.components, this.aimDirection.normalized); //(playerController.aim.transform.position - transform.position).normalized
        this.TriggerAnimation("BasicAttack");
        StartCoroutine(this.AttackCoolDown());
    }
    private void Aiming()
    {

        if (Env.IS_USING_KEYBOARD) {
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

        this.playerAnim.SetFloat("AimSpeedX", this.aimDirection.x);
        this.playerAnim.SetFloat("AimSpeedZ", this.aimDirection.z);
    }

    private IEnumerator AttackCoolDown()
    {
        yield return this.attackDelay;
        this.CanAttack = true;
    }

#endregion

#region Death
private void Death()
    {
        EventManager.Instance.Trigger(new ChangeGameStateEvent
        {
            currentGameState = GameState.ended
        });
        this.components.DesuscribeDeathAction(this.Death);
        Destroy(this.gameObject);
    }
    #endregion

    #region Actions

    public void PressPause()
    {
        if (Actions.pause.WasPressed) {
            GameManager.Instance.TogglePause();
        }
    }


    #endregion

    #region Dash

    public void SetDashAction()
    {
        this.components.Dash.OnDashAction = this.ToggleDash;
    }

    public void Dash()
    {
       this.components.Dash.TriggerDash(this.rb);
    }

    public void ToggleDash(bool isDashing)
    {
        this.isDashing = isDashing;
    }

    #endregion

    #region Experience
    private void OnkillEnemy(KilledEnemyEvent e)
    {
        int newXp = Mathf.Max(0, this.components.Experience.GetCurrentCumulativeXP() + e.enemyData.xpToDrop);
        this.components.Experience.GiveXP(newXp);
    }
    #endregion

    #region TriggerAnimations
    public void TriggerAnimation(string animationTrigger)
    {
        this.playerAnim.SetTrigger(animationTrigger);
    }
    
    #endregion

}
