using System;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float StartHealth { get; private set; }
    public float health = 100;
    public float strength = 1;
    public int level = 0;
    public GameObject deathParticles;

    public void Start()
    {
        StartHealth = health;
    }
    

    public void AttackEntity()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Attack attack = hit.collider.GetComponent<Attack>();
            if (attack == null) return;
            hit.collider.GetComponent<Attack>().health -= this.strength;
            
            if (deathParticles != null)
            {
                Instantiate(deathParticles, transform.position, Quaternion.identity);
                deathParticles.GetComponent<ParticleSystem>().Play();
            }

            if (GetComponent<Attack>().health <= 0)
            {
                Destroy(gameObject);
                level++; // Increment level
                
                if (deathParticles != null)
                {
                    Instantiate(deathParticles, transform.position, Quaternion.identity);
                    deathParticles.GetComponent<ParticleSystem>().Play();
                }
            }
        }
    }

    private void Update()
    {
        this.health += Time.deltaTime;
    }
}
