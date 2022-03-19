using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerAnimator : MonoBehaviour
{
    [System.Serializable]
    public class StringToClip
    {
        public string name;
        public AnimationClip animation;
    }

    [SerializeField] List<StringToClip> animationLibrary;
    [SerializeField] Transform weaponParent;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] Animator weaponAnimator;

    Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        InputReceiver.instance.OnFirePressed += PlayFireAnimation;
        InputReceiver.instance.OnAimPressed += () => AimAnimation(true);
        InputReceiver.instance.OnAimReleased += () => AimAnimation(false);
    }

    void PlayFireAnimation()
    {
        string animationName = weaponParent.GetChild(0).name;
        StringToClip clipToPlay = animationLibrary.FirstOrDefault(item => item.name == animationName);
        if (clipToPlay == null || clipToPlay.animation == null)
        {
            return;
        }
        playerAnim.Play("Base Layer." + clipToPlay.animation.name, 0);
        muzzleFlash.Play();
    }

    void AimAnimation(bool value)
    {
        weaponAnimator.Play("Base Layer." + (value ? "Aim" : "StopAim"));
    }
}
