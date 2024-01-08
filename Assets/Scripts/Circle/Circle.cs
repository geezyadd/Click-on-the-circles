using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Circle : MonoBehaviour
{
    public delegate void CircleTaped();
    public event CircleTaped OnCircleTaped;
    private Button _circleButton;
    private Image _circleImage; 
    private Animator _circleAnimator;
    private void Start()
    {
        _circleAnimator = GetComponent<Animator>();
        _circleImage = GetComponent<Image>();
        _circleImage.color = new Color(Random.value, Random.value, Random.value);
        _circleButton = gameObject.GetComponent<Button>();
        _circleButton.onClick.AddListener(ButtonClicked);
    }
    private void ButtonClicked() 
    {
        OnCircleTaped?.Invoke();
        _circleAnimator.SetBool("end", true);
    }
    private void DestroyCircle() 
    {
        Destroy(gameObject);
    }
}
