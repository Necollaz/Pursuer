using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Light _lightSource;
    [SerializeField] private AudioSource _shootSound;

    public void Perform()
    {
        StartCoroutine(EffectRoutine());
    }

    private IEnumerator EffectRoutine()
    {
        _shootSound.Play();
        _lightSource.gameObject.SetActive(true);
        _particleSystem.Clear();
        _particleSystem.Play();

        yield return new WaitForSeconds(0.1f);
        yield return new WaitForEndOfFrame();

        _lightSource.gameObject.SetActive(false);
    }
}
