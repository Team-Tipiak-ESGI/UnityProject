using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class EnemyAI : MonoBehaviour
{
    private bool _grounded = false;
    
    public float speed = 10.0f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public float toggleMax = 10f;
    public float toggleMin = 1.5f;
    public float attackRange = 2f;
    public GameObject target;
    public GameObject healthBar;

    void Awake()
    {
        // Get a reference to our physics component
        GetComponent<Rigidbody>().freezeRotation = true;
        GetComponent<Rigidbody>().useGravity = false;
    }

    void OnCollisionStay()
    {
        _grounded = true;
    }

    private void Update()
    {
        float dist = Vector3.Distance(target.transform.position, this.transform.position);

        if (toggleMin < dist && dist < toggleMax)
        {
            transform.LookAt(target.transform.position);
        }

        healthBar.GetComponent<Slider>().maxValue = GetComponent<Attack>().StartHealth;
        healthBar.GetComponent<Slider>().value = GetComponent<Attack>().health;
    }

    void FixedUpdate () {
        if (_grounded)
        {
            float dist = Vector3.Distance(target.transform.position, this.transform.position);
            
            if (toggleMin < dist && dist < toggleMax)
            {
                Vector3 targetVelocity = new Vector3(0, 0, 1);

                // Calculate how fast we should be moving
                targetVelocity = transform.TransformDirection(targetVelocity);
                targetVelocity *= speed;

                // Apply a force that attempts to reach our target velocity
                Vector3 velocity = GetComponent<Rigidbody>().velocity;
                Vector3 velocityChange = (targetVelocity - velocity);
                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                velocityChange.y = 0;
                GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);
            }

            if (dist < attackRange)
            {
                GetComponent<Attack>().AttackEntity();
            }
        }
 
        // We apply gravity manually for more tuning control
        GetComponent<Rigidbody>().AddForce(new Vector3 (0, -gravity * GetComponent<Rigidbody>().mass, 0));
 
        _grounded = false;
    }
}