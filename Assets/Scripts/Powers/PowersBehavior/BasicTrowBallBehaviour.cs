using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTrowBallBehaviour : PowersBehaviour
{
    private BasicThrowBallEffect m_Effect;

    protected override void Init()
    {
        if (!this.isInit) {
            this.isInit = true;
            this.rigidbody = GetComponent<Rigidbody>();
            this.collider = GetComponent<Collider>();
        }
    }

    public override void StartAttack(Vector3 direction)
    {
        this.Init();
        this.isMoving = true;
        this.moveDirection = direction;
        this.moveDirection.y = 0;
        this.MovementBehavior();
    }

    private void FixedUpdate()
    {
        if (this.isMoving) {
            this.MovementBehavior();
        }
    }

    protected override void MovementBehavior()
    {
        //Vector3 movementVector = this.transform.forward;

        this.rigidbody.velocity = moveDirection * this.speed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterComponents component = other.GetComponent<CharacterComponents>();
        if(component != null) {
            if(component != this.attacker) {
                this.Collided();
            }
        } else {
            this.Destroy();
        }
    }

    private void Collided()
    {
        this.collider.enabled = false;
        this.isMoving = false;
        this.rigidbody.velocity = Vector3.zero;
        this.CreateEffect();
    }

    private void CreateEffect()
    {
        BasicThrowBallEffect prefab = (BasicThrowBallEffect)ResourcesManager.Instance.GetPowerEffectPrefab(this.powerType);
        this.effectPrefab = Instantiate(prefab.gameObject);
        this.effectPrefab.transform.localPosition = this.transform.localPosition;
        this.m_Effect = this.effectPrefab.GetComponent<BasicThrowBallEffect>();
        this.m_Effect.InitEffect(PowersDataBase.GetPowerDataByType(this.powerType), this.attacker);
        this.Destroy();
    }

    protected override void Destroy()
    {
        Destroy(this.gameObject);
    }
}
