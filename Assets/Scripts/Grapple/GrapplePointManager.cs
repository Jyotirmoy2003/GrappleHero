using UnityEngine;
using System.Collections.Generic;
using Jy_Util;

public class GrapplePointManager : MonoSingleton<GrapplePointManager>
{
    [Header("Player Reference")]
    public Transform player;

    [Header("Settings")]
    public float checkInterval = 0.1f; // How often to check (to reduce CPU load)
    public float activeRange = 50f;    // Optional: only consider points within this range

    [Header("Runtime Info")]
    public GrapplePoint closestGrapplePoint;

    private List<GrapplePoint> allPoints = new List<GrapplePoint>();
    private float timer = 0f;

    void Start()
    {
        // Gather all available GrapplePoints in the scene
        allPoints.AddRange(FindObjectsOfType<GrapplePoint>());
    }

    void Update()
    {
        // Optional timing to avoid running every frame
        timer += Time.deltaTime;
        if (timer >= checkInterval)
        {
            timer = 0f;
            UpdateClosestPoint();
        }
    }

    void UpdateClosestPoint()
    {
        if (player == null || allPoints.Count == 0)
            return;

        GrapplePoint newClosest = null;
        float closestDist = Mathf.Infinity;

        foreach (var point in allPoints)
        {
            if (point == null) continue;

            float dist = Vector3.Distance(player.position, point.transform.position);
            if (dist < closestDist && dist <= activeRange)
            {
                closestDist = dist;
               
                newClosest = point;
                newClosest.closest = true;
            }
        }

        // Only call when a new closest point is found
        if (newClosest != closestGrapplePoint)
        {
            if(closestGrapplePoint)
            {
                closestGrapplePoint.closest = false;
                closestGrapplePoint.KnobStatusChanged(E_KnobState.Idle);
            }
            closestGrapplePoint = newClosest;

            if (closestGrapplePoint != null)
            {
                closestGrapplePoint.closest = true;
                closestGrapplePoint.KnobStatusChanged(E_KnobState.Hover);
                
            }
        }
    }

    /// <summary>
    /// Call this if you dynamically add or remove grapple points during runtime.
    /// </summary>
    public void RefreshPoints()
    {
        allPoints.Clear();
        allPoints.AddRange(FindObjectsOfType<GrapplePoint>());
    }
}
