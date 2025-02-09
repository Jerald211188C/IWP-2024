using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("Gun References")]
    [SerializeField] private Gamemanager gamemanager;
    [SerializeField] private PlayerMovement _Stats;
    [SerializeField] private PlayerStats _tats;
    [SerializeField] private Rifle _Ammo;
    [SerializeField] private Rifle _RPGAmmo;
    [Header("ShopItems")]
    [SerializeField] private ShopItems _ShopItems;
    [SerializeField] private ShopItems _AmmoCost;
    [SerializeField] private ShopItems _RPGCost;
    [SerializeField] private ShopItemUI _ShopItemUI;
    [SerializeField] private ShopItemUI _AmmoShopItemUI;
    [SerializeField] private ShopItemUI _RPGAmmoShopItemUI;
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

    public void BuyRifleAmmo()
    {
        if (_tats._StartingCoins >= _AmmoCost.AmmoCost)
        {
            Debug.Log("Bought rifle ammo");
            _Ammo.ReserveAmmo += 20; // Add to ReserveAmmo instead of MaxAmmo
            _Ammo.UpdateAmmoUI();
            _tats._StartingCoins -= _AmmoCost.AmmoCost;
            _AmmoCost.AmmoCost += 10;
            _AmmoShopItemUI.UpdateUIAmmo();
        }
    }

    public void BuyRPGAmmo()
    {
        if (_tats._StartingCoins >= _RPGCost.AmmoCost)
        {
            Debug.Log("Bought rifle ammo");
            _RPGAmmo.RPGReserveAmmo += 5; // Add to ReserveAmmo instead of MaxAmmo
            _RPGAmmo.UpdateAmmoUI();
            _tats._StartingCoins -= _RPGCost.AmmoCost;
            _RPGCost.AmmoCost += 10;
            _RPGAmmoShopItemUI.UpdateUIAmmo();
        }
    }



}
