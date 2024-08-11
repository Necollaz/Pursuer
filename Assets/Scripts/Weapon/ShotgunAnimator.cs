using UnityEngine;

public static class ShotgunAnimationData
{
    public static class Params
    {
        public static readonly int Reload = Animator.StringToHash("Reload");
        public static readonly int Shoot = Animator.StringToHash("Shoot");
    }
}

public class ShotgunAnimator
{
    private Animator _animator;

    public ShotgunAnimator(Animator animator)
    {
        _animator = animator;
    }

    public void PlayShootAnimation()
    {
        _animator.SetTrigger(ShotgunAnimationData.Params.Shoot);
    }

    public void PlayReloadAnimation()
    {
        _animator.SetTrigger(ShotgunAnimationData.Params.Reload);
    }

}
