using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameOverPanel _gameOverPanel;
    [SerializeField] private PlayerHealth _playerHealth;

    private Timer _gameOverActivationTimer = new ();

    private void OnEnable() => 
        _playerHealth.PlayerDied += StartGameOverTimer;

    private void OnDisable() => 
        _playerHealth.PlayerDied -= StartGameOverTimer;

    private void Update()
    {
        if (_gameOverActivationTimer.IsActive)
            _gameOverActivationTimer.Tick();
    }

    private void StartGameOverTimer() 
    {
        float delay = 1.5f;

        _gameOverActivationTimer.TimeIsUp += ActivateGameOverPanel;
        _gameOverActivationTimer.Start(delay);
    }

    private void ActivateGameOverPanel()
    {
        _gameOverActivationTimer.TimeIsUp -= ActivateGameOverPanel;
        _gameOverPanel.gameObject.SetActive(true);
    } 
}