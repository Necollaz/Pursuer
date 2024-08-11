using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Shotgun : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Projectile _prefab;
    [SerializeField] private Transform _decal;
    [SerializeField] private ShootEffect _shootEffect;
    [SerializeField] private AudioSource _reloadSound;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _maxDistance = 100f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _impactForce = 10f;
    [SerializeField] private float _decalOffset;

    [Header("Shell")]
    [SerializeField] private Rigidbody _shellPrefab;
    [SerializeField] private Transform _shellPoint;
    [SerializeField] private float _shellSpeed = 2f;
    [SerializeField] private float _shellAngular = 15f;

    [Header("Recoil")]
    [SerializeField] private CameraShake _cameraShake;
    [SerializeField] private WaterSplaher _waterSplasher;

    private Collider _collider;
    private ShotgunAnimator _shotgunAnimator;
    private ShotgunReload _shotgunReload;
    private ShotgunShellExtractor _shellExtractor;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _shotgunAnimator = new ShotgunAnimator(_animator);
        _shotgunReload = new ShotgunReload(_reloadSound, _shotgunAnimator);
        _shellExtractor = new ShotgunShellExtractor(_shellPrefab, _shellPoint, _shellSpeed, _shellAngular);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _shotgunReload.Reload();
        }
    }

    public void Shoot(Vector3 startPoint, Vector3 direction)
    {
        ShootWeapon(startPoint, direction);
        _shootEffect.Perform();
        _shotgunAnimator.PlayShootAnimation();
    }

    public void Initialize(CharacterController characterController)
    {
        _collider = characterController as Collider;
    }

    public void ExtractShell()
    {
        _shellExtractor.ExtractShell();
    }

    private void ShootWeapon(Vector3 startPoint, Vector3 direction)
    {
        _cameraShake.MakeRecoil();

        if (Physics.Raycast(startPoint, direction, out RaycastHit hitInfo, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore))
        {
            _waterSplasher.TryCreateWaterSplash(startPoint, hitInfo.point);

            var decalInstance = Instantiate(_decal, hitInfo.transform);
            decalInstance.position = hitInfo.point + hitInfo.normal * _decalOffset;
            decalInstance.LookAt(hitInfo.point);
            hitInfo.collider.TryGetComponent(out AbstractHealth health);

            if (health != null) health.TakeDamage(_damage);

            var victimBody = hitInfo.rigidbody;

            if (victimBody != null)
            {
                victimBody.AddForceAtPosition(direction * _impactForce, hitInfo.point);
            }
        }
    }
}
