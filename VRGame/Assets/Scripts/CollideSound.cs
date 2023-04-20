using UnityEngine;

public enum MaterialType
{
    Glass,
    Can,
    Plastic,
}

public class CollideSound : MonoBehaviour
{
    [SerializeField] private MaterialType type;
    private float soundCooldown = 0.5f;
    private float timeSinceLastPlayed = 0f;

    private void Update()
    {
        timeSinceLastPlayed = Mathf.Min(timeSinceLastPlayed + Time.deltaTime, soundCooldown);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (timeSinceLastPlayed == soundCooldown)
        {
            switch (type)
            {
                case MaterialType.Glass:
                    AudioManager.Instance.PlayOneShot(FMODEvents.Instance.Glass, transform.position);
                    break;
                case MaterialType.Can:
                    AudioManager.Instance.PlayOneShot(FMODEvents.Instance.Can, transform.position);
                    break;
                case MaterialType.Plastic:
                    AudioManager.Instance.PlayOneShot(FMODEvents.Instance.Plastic, transform.position);
                    break;
                default:
                    break;
            }
            timeSinceLastPlayed = 0f;
        }
    }
}
