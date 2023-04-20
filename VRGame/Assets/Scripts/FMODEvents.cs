using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents Instance { get; private set; }
    [field: SerializeField] public EventReference OpenMenu { get; private set; }
    [field: SerializeField] public EventReference CloseMenu { get; private set; }
    [field: SerializeField] public EventReference GameMusic { get; private set; }
    [field: SerializeField] public EventReference Teleport { get; private set; }
    [field: SerializeField] public EventReference Glass { get; private set; }
    [field: SerializeField] public EventReference Can { get; private set; }
    [field: SerializeField] public EventReference Plastic { get; private set; }
    [field: SerializeField] public EventReference Pouring { get; private set; }
    [field: SerializeField] public EventReference Angry { get; private set; }
    [field: SerializeField] public EventReference Happy { get; private set; }
    [field: SerializeField] public EventReference Win { get; private set; }
    [field: SerializeField] public EventReference Lose { get; private set; }
    [field: SerializeField] public EventReference Scream { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
