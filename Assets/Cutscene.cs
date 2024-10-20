using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class Cutscene : MonoBehaviour
{
    [SerializeField]
    private SkeletonGraphic _characterSpine;

    private void OnEnable()
    {
        PlayAnimation("animation", false);
    }

    private void OnDestroy()
    {
        if (_characterSpine.AnimationState != null)
        {
            _characterSpine.AnimationState.Complete -= OnAnimationComplete;
        }
    }

    public void PlayAnimation(string animationName, bool loop)
    {
        if (_characterSpine.AnimationState != null)
        {
            _characterSpine.AnimationState.ClearTrack(0);
            //_characterSpine.AnimationState.SetEmptyAnimation(0, 0f);
            _characterSpine.AnimationState.SetAnimation(0, animationName, loop);

            _characterSpine.AnimationState.Complete += OnAnimationComplete;
        }
        else
        {
            Debug.LogWarning("SkeletonGraphic's AnimationState is null!");
        }
    }

    private void OnAnimationComplete(TrackEntry trackEntry)
    {
        _characterSpine.AnimationState.Complete -= OnAnimationComplete;
        gameObject.SetActive(false);
        Debug.Log($"Animation '{trackEntry.Animation.Name}' completed.");
    }
}
