using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SettingsManager : MonoBehaviour
{

    [Header("Pause")]
    public GameObject pauseContainer;
    public GameObject continueGO;
    

    void Start()
    {
        EventManager.Instance.AddListener<TogglePauseEvent>(this.OnTogglePause);
    }

    #region Events

    private void OnTogglePause(TogglePauseEvent e)
    {
        this.pauseContainer.SetActive(e.isPaused);
        EventSystem.current.SetSelectedGameObject(continueGO);

    }

    #endregion

    public void ContinueGame()
    {
        GameManager.Instance.TogglePause();
    }

    public void ReturnToMenu()
    {
        GameManager.Instance.TogglePause();
        OwnSceneLoadManager.Instance.LoadScene("MainMenu");
    }

    private void OnDestroy()
    {
        if (EventManager.HasInstance()) {
            EventManager.Instance.RemoveListener<TogglePauseEvent>(this.OnTogglePause);
        }
    }

}
