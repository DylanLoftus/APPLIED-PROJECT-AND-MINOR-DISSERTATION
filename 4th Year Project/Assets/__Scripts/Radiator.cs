using UnityEngine;

public class Radiator : Interactable
{
    private float wattage = 300;
    // radiators cut off after reaching a certain temperature (celsius)
    private float cutoffTemp = 20;
    // max temperature that the radiator can add to the room each minute
    private float maxTempInc = 1.0f;
    // temperature increase (heat added to the room each minute)
    private float tempInc;
    // time it takes for the radiator to heat up to maximum temperature
    private const float heatupTimeMinutes = 30;
    // heat increase per minute (inverse of above)
    private const float heatupInc = 1 / heatupTimeMinutes;

    private Material mat;

    void Awake()
    {
        mat = GetComponentInChildren<Renderer>().material;
        tempInc = 0;
    }

    public float SimulateTime(float deltaMinutes, Room room)
    {
        // heatup/cooldown radiator if it's on/off
        tempInc += (activated ? heatupInc : -heatupInc) * deltaMinutes;
        tempInc = Mathf.Clamp(tempInc, 0, maxTempInc);
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

                // heat up the room
                room.temperature += tempInc * deltaMinutes;

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

    public override void ResetState()
    {
        // no rotation to reset
    }

    public void SetColor(Color color)
    {
        mat.SetColor("_Color", color);
    }
}
