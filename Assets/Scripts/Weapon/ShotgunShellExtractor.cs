using UnityEngine;

public class ShotgunShellExtractor
{
    private Rigidbody _shellPrefab;
    private Transform _shellPoint;
    private float _shellSpeed;
    private float _shellAngular;

    public ShotgunShellExtractor(Rigidbody shellPrefab, Transform shellPoint, float shellSpeed, float shellAngular)
    {
        _shellPrefab = shellPrefab;
        _shellPoint = shellPoint;
        _shellSpeed = shellSpeed;
        _shellAngular = shellAngular;
    }

    public void ExtractShell()
    {
        var shell = Object.Instantiate(_shellPrefab, _shellPoint.position, _shellPoint.rotation);
        shell.velocity = _shellPoint.forward * _shellSpeed;
        shell.angularVelocity = Vector3.up * _shellAngular;
    }
}