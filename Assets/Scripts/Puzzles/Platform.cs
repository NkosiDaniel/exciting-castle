using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private List<PlatformSetpoint> setpoints;
    [SerializeField] private Transform mover;

    private int count;
    private List<Transform> transforms;
    private List<float> adjustedSpeeds;
    private float progress;
    private int previousIndex;
    private float pauseTimer;

    private void Start()
    {
        progress = 0;
        previousIndex = 0;
        count = setpoints.Count;
        transforms = new List<Transform>();
        adjustedSpeeds = new List<float>();
        
        for (int i = 0; i < count; i++)
        {
            int j = i + 1;
            if (j == count)
            {
                j = 0;
            }
            PlatformSetpoint current = setpoints[i];
            PlatformSetpoint next = setpoints[j];

            adjustedSpeeds.Add(current.Speed / (current.transform.position - next.transform.position).magnitude);
            transforms.Add(current.transform);
        }
    }

    private void Update()
    {
        if (pauseTimer > 0f)
        {
            pauseTimer -= Time.deltaTime;
            return;
        }

        int index = Mathf.FloorToInt(progress);
        int next = index + 1;
        if (next == count)
        {
            next = 0;
        }

        if (index != previousIndex)
        {
            float waitTime = setpoints[index].PostMoveWaitTime;
            if (waitTime > 0f)
            {
                pauseTimer = waitTime;
            }
        } else
        {
            progress += adjustedSpeeds[index] * Time.deltaTime;
            if (progress >= count)
            {
                progress -= count;
            }
            Vector3.MoveTowards(transforms[index].position, transforms[next].position, 10f);
        }

        previousIndex = index;
    }
}
