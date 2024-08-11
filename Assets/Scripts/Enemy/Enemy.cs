using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private Rigidbody _rigidbody;
    private EnemyMovement _movement;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _movement = GetComponent<EnemyMovement>();
        _playerMovement = _player.GetComponent<PlayerMovement>();

        if (_playerMovement != null)
        {
            _movement.Initialize(_playerMovement);
        }

    }

    private void FixedUpdate()
    {
        _movement.Move(transform, _player);
    }
}
