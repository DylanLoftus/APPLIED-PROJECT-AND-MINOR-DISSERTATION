using UnityEngine;

public class Radiator : Interactable
{
    // wattage/temperature variables
    public float wattage = 300;
    public float tempIncreasePerMinute = 1;
    // radiators cut off after reaching a certain temperature (celsius)
    public float cutoffTemp = 20;

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
