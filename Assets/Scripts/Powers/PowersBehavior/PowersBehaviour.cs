using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowersBehaviour : MonoBehaviour
{
    public PowerType powerType;
    public float speed;
    public CharacterComponents attacker;
    protected GameObject effectPrefab;
    protected Rigidbody rigidbody;
    protected Collider collider;
    protected Vector3 moveDirection;

    protected bool isInit;
    protected bool isMoving;

    private void Start()
    {
        this.Init();
    }
    public void SetPower(PowerType powerType, CharacterComponents attacker)
    {
        this.powerType = powerType;
        this.attacker = attacker;
    }

    protected abstract void Init();
    public abstract void StartAttack(Vector3 direction);
    protected abstract void MovementBehavior();
}
