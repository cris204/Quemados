using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : Singleton<ResourcesManager>
{
    private string basePath = "Resource";
    public GameObject GetPowerBehaviourPrefab(PowerType name)
    {
        return Resources.Load("Prefabs/Powers/PowersEffect" + name.ToString() + "Behaviour") as GameObject;
    }

    public GameObject GetPowerEffectPrefab(PowerType name)
    {
        return Resources.Load("Prefabs/Powers/PowersEffect" + name.ToString() + "Effect") as GameObject;
    }
}
