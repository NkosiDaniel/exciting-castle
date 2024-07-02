using UnityEngine;

public class TimeWatch : MonoBehaviour
{
    private const float tolerance = 0.005f;
    public static float Multiplier;

    [SerializeField] private float minMult;
    [SerializeField] private float maxMult;
    [SerializeField] private float minVelocity;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float stabalizingVelocity;

    [Range(0f, +2f)]
    [SerializeField]
    private float mult; // for debug purposes
    
    private float vel;

    private void Start()
    {
        vel = 0f;
        Multiplier = 1f;
    }

    private void Update()
    {
        bool down = Input.GetKey(KeyCode.DownArrow);
        bool up = Input.GetKey(KeyCode.UpArrow);

        vel = DetermineVelocity(down, up);
        if (vel == 0f)
        {
            Stabalize();
        }
        else
        {
            Multiplier += vel * Time.deltaTime;
        }

        if (Multiplier > maxMult)
        {
            Multiplier = maxMult;
        } else if (Multiplier < minMult)
        {
            Multiplier = minMult;
        }

        mult = Multiplier;
    }

    private void Stabalize()
    {
        if (1 - tolerance > Multiplier || Multiplier > 1 + tolerance)
        {
            Multiplier += stabalizingVelocity * Mathf.Sign(1f - Multiplier) * Time.deltaTime; // stabalize back to 1f
        }
        else
        {
            Multiplier = 1f; // its close enough to 1f
        }
    }

    private float DetermineVelocity(bool down, bool up)
    {
        if (down && up)
        {
            return 0f;
        }
        if (up)
        {
            return maxVelocity;
        }
        if (down)
        {
            return minVelocity;
        }
        return 0f;
    }
}
