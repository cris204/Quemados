using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTrowBallBehaviour : MonoBehaviour
{
    public PowerType powerType;
    private GameObject effectPrefab;
    private BasicThrowBallPowerEffect m_Effect;

    private bool isInit;

    private void Start()
    {
        this.Init();
    }
    private void Init()
    {
        if (!this.isInit) {
            this.effectPrefab = ResourcesManager.Instance.GetPowerBehaviourPrefab(this.powerType);
            this.m_Effect = this.effectPrefab.GetComponent<BasicThrowBallPowerEffect>();
            this.m_Effect.FillData(PowersDataBase.GetPowerDataByType(this.powerType));
        }
    }
}
