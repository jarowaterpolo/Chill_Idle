
public static class DataConverter
{
    public static SaveData ToSave(Data data)
    {
        return new SaveData
        {
            stars = data.stars.ToString("R"),
            baseStarGain = data.baseStarGain.ToString("R"),
            starGainMult = data.starGainMult.ToString("R"),
            starGainMultRate = data.starGainMultRate.ToString("R"),
            totalStarGain = data.totalStarGain.ToString("R"),

            planets = data.planets.ToString("R"),
            planetStargainMult = data.planetStargainMult.ToString("R"),
            planetGainBonus = data.planetGainBonus.ToString("R"),
            planetGainMult = data.planetGainMult.ToString("R"),

            Milestones = data.Milestones
        };
    }
    public static Data FromSave(SaveData saveData)
    {
        return new Data
        {
            stars = double.Parse(saveData.stars),
            baseStarGain = double.Parse(saveData.baseStarGain),
            starGainMult = double.Parse(saveData.starGainMult),
            starGainMultRate = double.Parse(saveData.starGainMultRate),
            totalStarGain = double.Parse(saveData.totalStarGain),

            planets = double.Parse(saveData.planets),
            planetStargainMult = double.Parse(saveData.planetStargainMult),
            planetGainBonus = double.Parse(saveData.planetGainBonus),
            planetGainMult = double.Parse(saveData.planetGainMult),

            Milestones = saveData.Milestones
        };
    }
}