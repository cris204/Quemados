using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    public PlayerXPUI playerXp;

    private void Awake()
    {
        EventManager.Instance.AddListener<OnPlayerXPUpdatedEvent>(this.OnPlayerXpUpdated);
    }

    private void OnDestroy()
    {
        if (EventManager.HasInstance()) {
            EventManager.Instance.RemoveListener<OnPlayerXPUpdatedEvent>(this.OnPlayerXpUpdated);
        }
    }

    private void OnPlayerXpUpdated(OnPlayerXPUpdatedEvent e)
    {
        this.playerXp.UpdateXP(e.newXpValue);
    }

}
