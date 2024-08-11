using UnityEngine;

public class ShotgunReload
{
    private AudioSource _reloadSound;
    private ShotgunAnimator _animator;

    public ShotgunReload(AudioSource reloadSound, ShotgunAnimator animator)
    {
        _reloadSound = reloadSound;
        _animator = animator;
    }

    public void Reload()
    {
        _animator.PlayReloadAnimation();
        _reloadSound.Play();
    }
}
