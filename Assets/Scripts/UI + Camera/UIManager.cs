using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameOverPanel _gameOverPanel;
    [SerializeField] private PlayerHealth _playerHealth;

    private void OnEnable()
    {
        _playerHealth.PlayerDied += ActivateGameOverPanel;
    }

    private void OnDisable()
    {
        _playerHealth.PlayerDied -= ActivateGameOverPanel;
    }

    public async void ActivateGameOverPanel()
    {
        await Task.Delay(1500);
        _gameOverPanel.gameObject.SetActive(true);
    }
}
