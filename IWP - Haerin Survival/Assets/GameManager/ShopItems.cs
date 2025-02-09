using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "ShopItems", menuName = "Scriptable Objects/ShopItems")]
public class ShopItems : ScriptableObject
{
    public string ItemName;
    public string description;
    public Sprite icon;
    public int HealthCost;
    public int AmmoCost;
    public int RPGCost;
    public bool isbought = false;


    [Header("Default Values")]
    public int defaultcost = 100;

    private void OnEnable()
    {
        // Reset the stats when the game starts
        ResetStats();
    }

    public void ResetStats()
    {
        HealthCost = defaultcost;
        AmmoCost = defaultcost;
        RPGCost = defaultcost;
    }

}
