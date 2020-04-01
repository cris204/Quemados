using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private PlayerController playerController;

    private bool isInit;

    private void Start()
    {
        this.Init();
    }

    private void Init()
    {
        if (!this.isInit) {
            this.isInit = true;
            this.playerController = FindObjectOfType<PlayerController>();
        }
    }

    public Transform GetPlayerTransform()
    {
        return this.playerController.gameObject.transform;
    }
}
