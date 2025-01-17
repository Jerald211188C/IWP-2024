using UnityEngine;


[CreateAssetMenu(fileName = "PlayerStats", menuName = "Game/Stats")]
public class PlayerStats : ScriptableObject
{
    public int _EXP;
    public int _StartingCoins;
    public int _CoinsToAdd;
}
