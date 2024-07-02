using UnityEngine;

public class PlatformSetpoint : MonoBehaviour
{
    /// <summary>
    /// The speed for the platform to move from this setpoint to the next one
    /// </summary>
    public float Speed;
    /// <summary>
    /// An amount of time in seconds for the platform to wait after reaching the next setpoint
    /// </summary>
    public float PostMoveWaitTime;
}
