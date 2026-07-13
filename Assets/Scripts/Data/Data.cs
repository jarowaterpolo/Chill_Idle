[System.Serializable]
public enum AutobuyerType
{
    StarGainAddition, StarGainMultProducer, StarPlanetGain
}

[System.Serializable]
public class Data
{
    //stars
    public double stars = 0;
    public double baseStarGain = 1;
    public double starGainMult = 1;
    public double starGainMultRate = 0;
    public bool produceStarGainMult = false;
    public double totalStarGain = 1;
    public double starPlanetGainIncrease = 0;

    //planets
    public double planets = 0;
    public double planetStargainMult = 1;
    public double planetGainBonus = 0;
    public double planetGainMult = 1;
    public double planetStarGainPower = 0;

    //nova's
    public double nova = 0;

    //milestones
    public bool[] milestones = new bool[4];

    //auto buyers
    public Autobuyer[] autobuyers = new Autobuyer[]
    {
        new Autobuyer{ type = AutobuyerType.StarGainAddition, isActive = false, buyAmount = 1, buyDelay = 1f},
        new Autobuyer{ type = AutobuyerType.StarGainMultProducer, isActive = false, buyAmount = 1, buyDelay = 1f },
        new Autobuyer{ type = AutobuyerType.StarPlanetGain, isActive = false, buyAmount = 1, buyDelay = 1f }
    };
}

[System.Serializable]
public class SaveData
{
    //stars
    public string stars = "0";
    public string baseStarGain = "1";
    public string starGainMult = "1";
    public string starGainMultRate = "0";
    public bool produceStarGainMult = false;
    public string totalStarGain = "1";
    public string starPlanetGainIncrease = "0";

    //planets
    public string planets = "0";
    public string planetStargainMult = "1";
    public string planetGainBonus = "0";
    public string planetGainMult = "1";
    public string planetStarGainPower = "0";

    //nova's
    public string nova = "0";

    //milestones
    public bool[] milestones = new bool[4];

    //auto buyers
    public Autobuyer[] autobuyers;
}
