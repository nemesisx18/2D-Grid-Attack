using Block2D.Module.Blocks;
using Block2D.Module.Times;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Block2D.Module.UserInterface
{
    public class GameSceneUI : MonoBehaviour
    {
        [SerializeField] private Text _scoreText;
        
        [SerializeField] private Image _nextBlockImage;

        [SerializeField] private Button _retryButton;

        [SerializeField] private GameObject _gameOverPanel;

        private void OnEnable()
        {
            Timer.OnGameOver += GameOver;
            BlockSpawner.OnChangeImage += OnNextBlockUpdate;

            _retryButton.onClick.AddListener(RetryGame);
        }

        private void OnDisable()
        {
            Timer.OnGameOver -= GameOver;
            BlockSpawner.OnChangeImage -= OnNextBlockUpdate;
            
            _retryButton?.onClick.RemoveListener(RetryGame);
        }

        private void Update()
        {
            _scoreText.text = GameManager.Instance.score.ToString();
            
            if (GameManager.Instance.isGameOver)
            {
                GameOver();
            }
        }

        private void OnNextBlockUpdate(Sprite sprite)
        {
            _nextBlockImage.sprite = sprite;
        }

        private void RetryGame()
        {
            SceneManager.LoadScene("GameScene");
        }

        private void GameOver()
        {
            _gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}