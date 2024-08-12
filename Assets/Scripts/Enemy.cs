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

        if (_player.TryGetComponent(out PlayerMovement playerMovement))
        {
            _playerMovement = playerMovement;
            _movement.Initialize(_playerMovement);
        }
    }

    private void FixedUpdate()
    {
        if (_player != null)
        {
            _movement.Move(transform, _player);
        }
    }
}