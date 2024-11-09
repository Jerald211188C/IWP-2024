using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI ammoText;  // TextMeshPro reference for ammo
    [Header("Player Stats")]
    public int currentAmmo;
    public int maxAmmo;

    private void Awake()
    {
        
    }
    void Start()
    {
        
        UpdateAmmoText();
    }

    public void UpdateAmmoText()
    {
        ammoText.text = "Ammo: " + currentAmmo + "/" + maxAmmo;
    }


}
