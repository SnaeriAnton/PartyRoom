using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HumanAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private string _dance = "Dance";
    private string _walking = "Walking";
    private string _stay = "Stay";

    public void Dance()
    {
        _animator.SetTrigger(_dance);
    }

    public void Walk()
    {
        _animator.SetTrigger(_walking);
    }

    public void Stay()
    {
        _animator.SetTrigger(_stay);
    }
}
