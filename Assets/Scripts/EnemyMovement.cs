using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _stoppingDistance = 2.0f;
    [SerializeField] private float _speedMultiplier = 1.5f;

    private PlayerMovement _playerMovement;

    public void Initialize(PlayerMovement playerMovement)
    {
        _playerMovement = playerMovement;
    }

    public void Move(Transform bot, Transform player)
    {
        MoveTowards(bot, player);
    }

    private void MoveTowards(Transform botTransform, Transform playerTransform)
    {
        if (playerTransform != null && _playerMovement != null)
        {
            Vector3 direction = playerTransform.position - botTransform.position;

            if (direction.magnitude > _stoppingDistance)
            {
                direction.Normalize();

                if (botTransform.TryGetComponent(out Rigidbody rigidbody))
                {
                    float playerSpeed = _playerMovement.Speed;
                    float botSpeed = playerSpeed / _speedMultiplier;

                    Vector3 moveDirection = direction * Mathf.Min(_speed, botSpeed) * Time.fixedDeltaTime;
                    rigidbody.MovePosition(rigidbody.position + moveDirection);
                }
            }
        }
    }
}
