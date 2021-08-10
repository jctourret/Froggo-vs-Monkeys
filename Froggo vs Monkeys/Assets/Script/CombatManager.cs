using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static Action OnCombatBegin;
    public static Action OnOptionChange;
    public static Action OnOptionSelect;
    public static Action OnCancel;
    public static Action OnCombatEnd;

    public GameObject FightOrFlight;

    public CharacterCombat playerCharacter;
    List<CharacterCombat> enemyCharacters;

    enum Turn { Player, Enemies};
    Turn currentTurn;
    private void Start()
    {
        currentTurn = Turn.Player;
    }
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        if (currentTurn == Turn.Player)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                OnOptionChange?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                OnOptionSelect?.Invoke();
            }
        }
    }
}
