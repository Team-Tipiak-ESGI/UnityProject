using UnityEngine;

public class Attack : MonoBehaviour
{
    public float health = 100;
    public float strength = 1;
    
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
        }
    }
}
