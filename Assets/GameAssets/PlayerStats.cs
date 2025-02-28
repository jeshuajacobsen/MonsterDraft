using Zenject;
using UnityEngine;


public class PlayerStats : IInitializable
{
    private int _coins;
    private int _mana;
    private int _actions;
    private int _experience;
    private RoundUIManager _uiManager;

    [Inject]
    public PlayerStats(RoundUIManager uiManager)
    {
        _uiManager = uiManager;
    }

    public void Initialize()
    {
        Coins = 0;
        Mana = 0;
        Actions = 0;
        Experience = 0;
    }

    public int Coins
    {
        get => _coins;
        set
        {
            _coins = value;
            _uiManager.UpdateCoins(_coins);  // Update UI
        }
    }

    public int Mana
    {
        get => _mana;
        set
        {
            _mana = value;
            _uiManager.UpdateMana(_mana);
        }
    }

    public int Actions
    {
        get => _actions;
        set
        {
            _actions = value;
            _uiManager.UpdateActions(_actions);
        }
    }

    public int Experience
    {
        get => _experience;
        set
        {
            _experience = value;
            _uiManager.UpdateExperience(_experience);
        }
    }
}
