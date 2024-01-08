using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimation : MonoBehaviour
{
    private bool _resetAnim = false;
    private Animator _animator;
    public bool GetResetAnimation() { return _resetAnim; }
    public void SetResetAnimation() { _resetAnim = !_resetAnim; }
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    private void ResetTextAnimation()
    {
        _resetAnim = true;
    }
  
}
