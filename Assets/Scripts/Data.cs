using UnityEngine;

[System.Serializable]
public class Data
{
    //stars
    public int stars = 0;
    public int baseStarGain = 1;
    public int starGainMult = 1;
    public int starGainMultRate = 0;
    public bool produceStarGainMult = false;
    public int totalStarGain = 1;

    //planets
    public int planets = 0;
    public int planetStargainMult = 1;
}
