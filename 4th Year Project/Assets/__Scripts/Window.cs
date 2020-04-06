using UnityEngine;

public class Window : Interactable
{
    private Transform rigidbodyTransform;

    public void Awake()
    {
        rigidbodyTransform = GetComponentInChildren<Rigidbody>().transform;
    }

    public override void OnInteraction(bool activated)
    {
        rigidbodyTransform.Rotate(activated ? 90 : -90, 0, 0, Space.Self);
    }

    public override void ResetState()
    {
        rigidbodyTransform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
