using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : Singleton<ResourcesManager>
{
    private string basePath = "Resource";
    public PowersBehaviour GetPowerBehaviourPrefab(PowerType name)
    {
        PowersBehaviour objectLoaded = Resources.Load<PowersBehaviour>("Prefabs/Powers/PowersBehaviour/" + name.ToString() + "Behaviour");
        return objectLoaded;
    }

    public PowersEffect GetPowerEffectPrefab(PowerType name)
    {
        PowersEffect objectLoaded = Resources.Load<PowersEffect>("Prefabs/Powers/PowersEffect/" + name.ToString() + "Effect");
        return objectLoaded;
    }

    public GameObject GetEnemy(string enemyId)
    {
        GameObject enemy = Resources.Load<GameObject>("Prefabs/Characters/Enemies/Enemy");
        return enemy;

    }
}
