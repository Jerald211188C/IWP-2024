using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{
    [SerializeField] private float _max; // Max Health
    [SerializeField] private float _value; // Current Health
    [SerializeField] private Slider _Slider;
    [SerializeField] private Image redFlashImage; // Reference to the red flash Image

    [Header("Settings")]
    [SerializeField] private bool isPlayer = false; // Identify if this is the player's health

    public UnityEvent Death = new UnityEvent(); // Event for when the object dies

    private void Awake()
    {
        _Slider.maxValue = _max;
        _Slider.value = _value;

        if (isPlayer && redFlashImage != null)
        {
            redFlashImage.color = new Color(1f, 0f, 0f, 0f); // Red with 0 alpha
        }
    }

    public void Damage(float diff)
    {
        _value -= diff;
        _Slider.value = _value;

        // Only trigger red flash if this is the player's health
        if (isPlayer && redFlashImage != null)
        {
            StartCoroutine(FlashRed());
        }

        if (_value <= 0)
        {
            Death.Invoke(); // Notify listeners of death
            Destroy(gameObject); // Destroy the GameObject
        }
    }

    private IEnumerator FlashRed()
    {
        redFlashImage.color = new Color(1f, 0f, 0f, 0.5f); // Red with 50% alpha
        float fadeDuration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0.5f, 0f, elapsedTime / fadeDuration);
            redFlashImage.color = new Color(1f, 0f, 0f, alpha);
            yield return null;
        }

        redFlashImage.color = new Color(1f, 0f, 0f, 0f); // Ensure fully transparent
    }
}
