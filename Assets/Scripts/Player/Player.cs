using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Shotgun _shotgun;

    private PlayerMovement _playerMovement;
    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerMovement.Initialize(_characterController);
        _shotgun.Initialize(_characterController);
    }

    private void Update()
    {
        TryShoot();
        TryInteractDoor();

    }

    private void TryShoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _shotgun.Shoot(_shootPoint.position, _shootPoint.forward);
        }
    }

    private void TryInteractDoor()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;

            if(Physics.Raycast(transform.position, transform.forward, out hit, 2f))
            {
                if(hit.transform.TryGetComponent(out Door door))
                {
                    if(door != null)
                    {
                        door.ToggleDoor();
                    }
                }
            }
        }
    }
}
