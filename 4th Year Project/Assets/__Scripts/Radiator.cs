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
        SetColor(activated ? Color.red : Color.white);
    }

    public void SetColor(Color color)
    {
        mat.SetColor("_Color", color);
    }
}
