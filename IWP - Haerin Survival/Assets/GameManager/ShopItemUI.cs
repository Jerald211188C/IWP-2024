using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemPriceText;
    [SerializeField] private Button buyButton;

    [SerializeField] private ShopItems shopItem;

    private void Start()
    {
        UpdateUI();
        UpdateUIAmmo();
    }

    public void UpdateUI()
    {
        itemNameText.text = shopItem.name;
        itemIcon.sprite = shopItem.icon;
        itemPriceText.text = shopItem.HealthCost.ToString() + " Coins";
    }

    public void UpdateUIAmmo()
    {
        itemNameText.text = shopItem.name;
        itemIcon.sprite = shopItem.icon;
        itemPriceText.text = shopItem.AmmoCost.ToString() + " Coins";
    }
}
