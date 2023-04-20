using UnityEngine;

public class WaterFaucet : MonoBehaviour
{
    [SerializeField] private ParticleSystem water;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        water.Play();
    }
    private void OnTriggerExit(Collider other)
    {
        water.Stop();
    }
}
