using UnityEngine;

public class Radiator : Interactable
{
    // wattage/temperature variables
    public float wattage = 300;
    // radiators cut off after reaching a certain temperature (celsius)
    public float cutoffTemp = 20;

    private float maxTempInc = 1.0f;
    private float tempInc;
    private const float heatupTimeMinutes = 600;
    private const float heatupInc = 1 / heatupTimeMinutes;

    private Material mat;

    void Start()
    {
        mat = GetComponentInChildren<Renderer>().material;
        tempInc = 0;
    }

    public float SimulateTime(float deltaMinutes, Room room)
    {
        // heatup/cooldown radiator if it's on/off
        tempInc = Mathf.Clamp(tempInc + (activated ? heatupInc : -heatupInc), 0, maxTempInc);
        float kwhCost = 0;

        if (activated)
        {
            if (room.temperature >= cutoffTemp)
            {
                // radiator has reached it's target temp; don't heat the room any more
                // (color the radiator yellow to show this)
                SetColor(Color.yellow);
            }
            else
            {
                SetColor(Color.red);

                room.temperature += tempInc * deltaMinutes;
                SetColor(Color.red);

                // calculate kwh used for this radiator
                kwhCost = (wattage / 1000) / 60 * deltaMinutes;
            }
        }
        else
        {
            SetColor(Color.white);
        }

        return kwhCost;
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
