using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);
    private const string MouseY = "Mouse Y";
    private const string MouseX = "Mouse X";

    [SerializeField] private Transform _camera;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravityFactor = 2f;
    [SerializeField] private float _strafeSpeed;
    [SerializeField] private float _horizaontalTurn;
    [SerializeField] private float _verticalTurn;
    [SerializeField] private float _verticalMinAngle = -89f;
    [SerializeField] private float _verticalMaxAngle = 89f;

    private Vector3 _verticalVelocity;
    private Transform _transform;
    private CharacterController _characterController;
    private float _cameraAngle = 0f;

    public float Speed => _speed;

    private void Awake()
    {
        _transform = transform;
        _characterController = GetComponent<CharacterController>();
        _cameraAngle = _camera.localEulerAngles.x;
    }

    private void Update()
    {
        Move();
        HandleCamera();
    }

    public void Initialize(CharacterController characterController)
    {
        _characterController = characterController;
    }

    private void Move()
    {
        Vector3 forward = Vector3.ProjectOnPlane(_camera.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(_camera.right, Vector3.up).normalized;

        if (_characterController != null)
        {
            Vector3 playerSpeed = forward * Input.GetAxis(Vertical) * _speed + right * Input.GetAxis(Horizontal) * _strafeSpeed;

            if (_characterController.isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _verticalVelocity = Vector3.up * _jumpForce;
                }
                else
                {
                    _verticalVelocity = Vector3.down;
                }

                _characterController.Move((playerSpeed + _verticalVelocity) * Time.deltaTime);
            }
            else
            {
                Vector3 horizontalVelocity = _characterController.velocity;
                horizontalVelocity.y = 0;
                _verticalVelocity += Physics.gravity * Time.deltaTime * _gravityFactor;
                _characterController.Move((horizontalVelocity + _verticalVelocity) * Time.deltaTime);
            }
        }
    }

    private void HandleCamera()
    {
        _cameraAngle -= Input.GetAxis(MouseY) * _verticalTurn;
        _cameraAngle = Mathf.Clamp(_cameraAngle, _verticalMinAngle, _verticalMaxAngle);
        _camera.localEulerAngles = Vector3.right * _cameraAngle;
        _transform.Rotate(Vector3.up * _horizaontalTurn * Input.GetAxis(MouseX));
    }
}
