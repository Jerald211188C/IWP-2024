using UnityEngine;
using System.Collections;

public class RPGProjectile : MonoBehaviour
{
    public float explosionRadius = 5f; // Radius of the explosion
    public int explosionDamage = 1000; // Damage dealt by the explosion
    public BaseGunScript _RPG;
    public GameObject explosionParticlePrefab; // Assign your explosion particle prefab in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"RPG Projectile hit: {other.gameObject.name}"); // Debugging
        Explode();
    }

    private void Explode()
    {
        // Instantiate the explosion particle effect
        if (explosionParticlePrefab != null)
        {
            GameObject explosionEffect = Instantiate(explosionParticlePrefab, transform.position, Quaternion.identity);

            // Scale the explosion effect based on the explosion radius
            explosionEffect.transform.localScale = new Vector3(explosionRadius, explosionRadius, explosionRadius);

            // Optionally, you can modify the particle system's size over lifetime if needed
            ParticleSystem ps = explosionEffect.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                var mainModule = ps.main;
                mainModule.startSize = explosionRadius; // Adjust particle size to match explosion radius
            }

            Destroy(explosionEffect, 3f); // Destroy the explosion effect after 3 seconds
        }

        // Find all colliders within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {
            // Skip the player
            if (collider.CompareTag("Player"))
                continue;

            Health targetHealth = collider.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.Damage(explosionDamage); // Deal damage to everything except the player
            }
        }

        Debug.Log("RPG Projectile Exploded!");

        // Play explosion sound
        SoundManager.instance.PlaySound(_RPG._Hit);

        // Destroy the projectile after a short delay
        StartCoroutine(DestroyAfterDelay(0.1f));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        Destroy(gameObject); // Destroy the projectile
    }
}
