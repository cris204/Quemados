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

    public override void StartAttack()
    {
        this.Init();
        this.isMoving = true;
    }

    private void Update()
    {
        if (this.isMoving) {
            this.MovementBehavior();
        }
    }

    protected override void MovementBehavior()
    {
        Vector3 movementVector = this.transform.forward;

        this.rigidbody.velocity = movementVector * this.speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.isMoving) {
            CharacterComponents component = other.GetComponent<CharacterComponents>();
            if(component != null) {
                if(component != this.attacker) {
                    this.Collided();
                }
            }
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
        this.effectPrefab = Instantiate(prefab.gameObject, this.gameObject.transform);
        this.effectPrefab.transform.localPosition = Vector3.zero;
        this.m_Effect = this.effectPrefab.GetComponent<BasicThrowBallEffect>();
        this.m_Effect.InitEffect(PowersDataBase.GetPowerDataByType(this.powerType), this.attacker);
        this.m_Effect.StartToEffect();
    }
}
