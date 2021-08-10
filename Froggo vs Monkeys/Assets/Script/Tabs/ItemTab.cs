using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTab : MonoBehaviour
{

    public Animator animator;

    enum OptionAnimations { Up, OnFightDown, Idle, Flip }

    OptionAnimations currentAnimation;

    private void OnEnable()
    {
        FlightOrFlightPanel.onFight += StandUp;
    }

    private void OnDisable()
    {
        FlightOrFlightPanel.onFight -= StandUp;
    }

    private void Start()
    {
        ChangeAnimation(OptionAnimations.Idle);
    }
    void StandUp()
    {
        ChangeAnimation(OptionAnimations.Up);
        Invoke("SetIdle", animator.GetCurrentAnimatorStateInfo(0).length);
    }

    void ChangeAnimation(OptionAnimations newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        animator.Play(newAnimation.ToString());

        currentAnimation = newAnimation;
    }

    void SetIdle()
    {
        ChangeAnimation(OptionAnimations.Idle);
    }
}
