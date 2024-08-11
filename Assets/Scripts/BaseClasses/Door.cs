using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    private const string Openned = "isOpened";

    [SerializeField] private Animator _animator;

    private bool _isOpen = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ToggleDoor()
    {
        _isOpen = !_isOpen;
        _animator.SetBool(Openned, _isOpen);
    }
}
