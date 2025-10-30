using UnityEngine;

/// <summary>
/// Clamps a Rigidbody's velocity each FixedUpdate to a maximum speed.
/// Safe to use with SpringJoint; also provides an optional soft-clamp smoothing mode.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class RigidbodyVelocityClamper : MonoBehaviour
{
    public float maxSpeed = 20f;                  // max linear speed (units/sec)
    [Tooltip("If true, reduces velocity smoothly instead of snapping to maxSpeed.")]
    public bool smoothClamp = true;
    [Tooltip("How fast to lerp velocity down when smoothClamp is enabled.")]
    public float smoothDamping = 10f;

    [SerializeField] Rigidbody2D rb;

   

    void FixedUpdate()
    {
        
        Vector3 v = rb.linearVelocity;
        float currentSpeed = v.sqrMagnitude; // use sqrMagnitude for cheap check

        float maxSpeedSqr = maxSpeed * maxSpeed;
        if (currentSpeed > maxSpeedSqr)
        {
            if (smoothClamp)
            {
                // Smoothly scale the velocity down towards maxSpeed to avoid sudden snap
                float speed = Mathf.Sqrt(currentSpeed);
                float targetSpeed = Mathf.MoveTowards(speed, maxSpeed, smoothDamping * Time.fixedDeltaTime);
                rb.linearVelocity = v.normalized * targetSpeed;
            }
            else
            {
                // Hard clamp
                rb.linearVelocity = Vector3.ClampMagnitude(v, maxSpeed);
            }
        }
    }
}
