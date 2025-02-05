using UnityEngine;


[CreateAssetMenu(fileName = "PlayerStats", menuName = "Game/Stats")]
public class PlayerStats : ScriptableObject
{
    public int _EXP;
    public int _StartingCoins;
    public int _CoinsToAdd;
    public int _CurrentCoins;
    public float _DamageMultiplier;

    // Default values to reset to
    [Header("Default Values")]
    public int defaultEXP = 50;
    public int defaultStartingCoins = 0;
    public int defaultCoinsToAdd = 0;
    public float defaultDamageMultiplier = 1f;


    private void OnEnable()
    {
        // Reset the stats when the game starts
        ResetStats();
    }

    public void ResetStats()
    {
        _EXP = defaultEXP;
        _StartingCoins = defaultStartingCoins;
        _CoinsToAdd = defaultCoinsToAdd;
        _CurrentCoins = defaultStartingCoins; // Start with initial coins
        _DamageMultiplier = defaultDamageMultiplier;
    }

}
