using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("Noize")]
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private AnimationCurve _perlinNoizeAmplitudeCurve;
    [SerializeField] private float _perlonNoizeTimeScale = 1f;

    [Header("Recoil")]
    [SerializeField] private float _tension = 10f;
    [SerializeField] private float _demping = 10f;
    [SerializeField] private float _impulse = 10f;

    private Vector3 _shakeAngels = new Vector3();
    private Vector3 _recoilAngels = new Vector3();
    private Vector3 _recoilVelocity = new Vector3();

    private float _amplitude = 5f;
    private float _duration = 1f;
    private float _shakeTimer = 1f;

    private void Update()
    {
        UpdateShake();
        UpdateRecoil();

        _cameraTransform.localEulerAngles = _shakeAngels + _recoilAngels;
    }

    private void UpdateRecoil()
    {
        _recoilAngels += _recoilVelocity * Time.deltaTime;
        _recoilVelocity += -_recoilAngels * Time.deltaTime * _tension;
        _recoilVelocity = Vector3.Lerp(_recoilVelocity, Vector3.zero, Time.deltaTime * _demping);
    }

    private void UpdateShake()
    {
        if(_shakeTimer > 0)
        {
            _shakeTimer -= Time.deltaTime / _duration;
        }

        float time = Time.time * _perlonNoizeTimeScale;
        _shakeAngels.x = Mathf.PerlinNoise(time, 0);
        _shakeAngels.y = Mathf.PerlinNoise(0, time);
        _shakeAngels.z = Mathf.PerlinNoise(time, time);

        _shakeAngels *= _amplitude;
        _shakeAngels *= _perlinNoizeAmplitudeCurve.Evaluate(Mathf.Clamp01(1 - _shakeTimer));
    }

    [ProPlayButton]
    public void MakeShake()
    {
        MakeShake(15, 3);
    }

    public void MakeShake(float amplitude, float duration)
    {
        _amplitude = amplitude;
        _duration = Mathf.Max(duration, 0.05f);
        _shakeTimer = 1;
    }

    [ProPlayButton]
    public void MakeRecoil()
    {
        MakeRecoil(-Vector3.right * Random.Range(_impulse * 0.5f, _impulse) + Vector3.up * Random.Range(-_impulse, _impulse)/3f);
    }

    public void MakeRecoil(Vector3 impulse)
    {
        _recoilVelocity += impulse;
    }
}
