using UnityEngine;

public class Window : Interactable
{
    private Transform rigidbodyTransform;

    public void Start()
    {
        rigidbodyTransform = GetComponentInChildren<Rigidbody>().transform;
    }

    public override void OnInteraction(bool activated)
    {
        rigidbodyTransform.Rotate(activated ? 90 : -90, 0, 0, Space.Self);
    }
}
