using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    private float _score = 0;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _gameOverScoreText;
    [SerializeField] private CircleSpawner _circleSpawner;
    [SerializeField] private Slider _timerSlider;
    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private Button _reloadButton;
    private ResetAnimation _resetAnimation;
    private Animator _textAnimator;
    private bool _isGameOver = false;
    private void Start()
    {
        _reloadButton.onClick.AddListener(ReloadButtonClicked);
        _resetAnimation = _scoreText.GetComponent<ResetAnimation>();
        _textAnimator = _scoreText.GetComponent<Animator>();
        _circleSpawner.OnPointsAdded += TextAnimations;
    }
    private void TextAnimations() 
    {
        _textAnimator.SetBool("GetPoints", !_textAnimator.GetBool("GetPoints"));
    }
    private void ResetAnimationChecker() 
    {
        if (_resetAnimation.GetResetAnimation()) 
        {
            TextAnimations();
            _resetAnimation.SetResetAnimation();
        }
    }

    private void ReloadButtonClicked() 
    {
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex);
    }

    private void Update()
    {
        ResetAnimationChecker();
        TextUpdator();
        SliderUpdator();
        GameOverCanvasUpdator();
    }
    private void TextUpdator() 
    {
        _scoreText.text = string.Format("{0} {1}", "Score:", _circleSpawner.GetScore());
    }
    private void SliderUpdator() 
    {
        _timerSlider.value -= Time.deltaTime;
        if(_timerSlider.value == 0) 
        {
            _isGameOver = true;
        }
    }
    private void GameOverCanvasUpdator() 
    {
        if(_isGameOver) 
        {
            _circleSpawner.SetGameOver();
            _gameOverCanvas.SetActive(true);
            _timerSlider.gameObject.SetActive(false);
            _gameOverScoreText.text = string.Format("{0} {1}", "Score:", _circleSpawner.GetScore());
        }
    }
}
