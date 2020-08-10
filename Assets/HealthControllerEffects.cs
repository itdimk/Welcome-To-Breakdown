using System.Collections;
using System.Collections.Generic;
using Itdimk;
using UnityEngine;

[RequireComponent(typeof(HealthController))]
public class HealthControllerEffects : MonoBehaviour
{
    public string HitAnimationName;
    public string DeathAnimationName;
    public string HitSoundName;
    public string DeathSoundName;

    public Animator Animator;
    public AudioManager Audio;

    // Start is called before the first frame update
    void Start()
    {
        var controller = GetComponent<HealthController>();

        if (Animator != null)
        {
            controller.Hitted += () => PlayAnimation(HitAnimationName);
            controller.Dead += () => PlayAnimation(DeathAnimationName);
        }

        if (Audio != null)
        {
            controller.Hitted += () => PlaySound(HitSoundName);
            controller.Dead += () => PlaySound(DeathSoundName);
        }
    }

    private void PlayAnimation(string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
            Animator.Play(name);
    }

    private void PlaySound(string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
            Audio.Play(name);
    }

    // Update is called once per frame
    void Update()
    {
    }
}