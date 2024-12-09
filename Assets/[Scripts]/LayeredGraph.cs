using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

public class LayeredAnimation : MonoBehaviour
{

    [SerializeField] private Animator animator;

    public void AnimationSetup(AnimationClip animationClip)
    {
        animator.Play(animationClip.name, 1, 0f);
        animator.SetLayerWeight(1 , 1f);
    }

}