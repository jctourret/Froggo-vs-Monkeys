using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightOrFlightPanel : MonoBehaviour
{
    public static Action onFight;
    public static Action onFlight;
    public Animator animator;


    enum state { Fight, Flight, InFight }
    enum OptionAnimations { Up, OnFightDown, OnFlightDown, Idle, Flip }
    [SerializeField]
    state currentState;
    OptionAnimations currentAnimation;



    private void OnEnable()
    {
        CombatManager.OnOptionChange += swapSide;
        CombatManager.OnOptionSelect += playOption;
    }
    private void OnDisable()
    {
        CombatManager.OnOptionChange -= swapSide;
        CombatManager.OnOptionSelect -= playOption;
    }
    private void Awake()
    {
        ChangeAnimation(OptionAnimations.Up);
        Invoke("SetIdle", animator.GetCurrentAnimatorStateInfo(0).length);
    }
    // Start is called before the first frame update
    void Start()
    {
        currentState = state.Fight;
    }

    void swapSide()
    {
        if (currentAnimation == OptionAnimations.Idle && currentState != state.InFight)
        {
            if (currentState == state.Fight)
            {
                currentState = state.Flight;
            }
            else
            {
                currentState = state.Fight;
            }
            ChangeAnimation(OptionAnimations.Flip);
            Invoke("SetIdle", animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }

    void playOption()
    {
        if (currentState != state.InFight && currentAnimation == OptionAnimations.Idle)
        {
            if (currentState == state.Fight && currentAnimation == OptionAnimations.Idle)
            {
                onFight?.Invoke();
                ChangeAnimation(OptionAnimations.OnFightDown);
                currentState = state.InFight;
            }
            else
            {
                onFlight?.Invoke();
                ChangeAnimation(OptionAnimations.OnFlightDown);
            }
            Invoke("SetIdle", animator.GetCurrentAnimatorStateInfo(0).length);
        }
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
