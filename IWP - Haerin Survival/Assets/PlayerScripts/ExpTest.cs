using UnityEngine;

public class ExpTest : MonoBehaviour
{
    [SerializeField] private ExpWindow expWindow;

    private void Awake()
    {
        // Use the LevelUpSystem singleton instance
        LevelUpSystem levelUpSystem = LevelUpSystem.Instance;

        // Set the LevelUpSystem in ExpWindow
        expWindow.SetLevelSystem(levelUpSystem);
    }
}
