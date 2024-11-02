using UnityEngine;
using System;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager _instance;

    private void Awake()
    {
        _instance = this;
    }

    public event Action _iswalking;

    public void IsPlayerWalking()
    {
        // Invoke the event if there are any subscribers
        _iswalking?.Invoke();
    }
}
