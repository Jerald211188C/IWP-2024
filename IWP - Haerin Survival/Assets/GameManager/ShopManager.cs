using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("Gun References")]
    [SerializeField] private Gamemanager gamemanager;
    [SerializeField] private PlayerMovement _Stats;
    [SerializeField] private PlayerStats _tats;
    [Header("ShopItems")]
    [SerializeField] private ShopItems _ShopItems;
    [SerializeField] private ShopItemUI _ShopItemUI;
    private Health playerHealth;

    private void Awake()
    {
        playerHealth = _Stats.GetComponent<Health>();
    }

    public void BuyHealthPotion()
    {
        if (_tats._StartingCoins >= _ShopItems.HealthCost)
        {
            Debug.Log("bought");
            playerHealth.Heal(10);
            _tats._StartingCoins -= _ShopItems.HealthCost;
            _ShopItems.HealthCost += 10;
            _ShopItemUI.UpdateUI();
        }
    }

    
}
