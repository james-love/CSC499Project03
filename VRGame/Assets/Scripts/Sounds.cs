using UnityEngine;

public class Sounds : MonoBehaviour
{
    public void Teleport()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.Teleport, transform.position);
    }
}
