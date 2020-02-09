using UnityEngine;

public class Radiator : Interactable
{
    private Material mat;

    void Start()
    {
        mat = gameObject.GetComponentInChildren<Renderer>().material;
    }

    public override void OnInteraction(bool activated)
    {
        mat.SetColor("_Color", activated ? Color.red : Color.white);
    }
}
