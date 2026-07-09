using System.Linq;

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

            milestones = data.milestones,

            autobuyers = data.autobuyers.Select(b => new Autobuyer
            {
                type = b.type,
                isActive = b.isActive,
                buyAmount = b.buyAmount,
                buyDelay = b.buyDelay,
            }).ToArray(),
        };
    }
    public static Data FromSave(SaveData saveData)
    {
        Data data = new Data
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

            milestones = saveData.milestones,
        };

        if (saveData.autobuyers != null)
        {
            foreach (Autobuyer savedBuyer in saveData.autobuyers)
            {
                Autobuyer existing = data.autobuyers
                    .FirstOrDefault(b => b.type == savedBuyer.type);

                if (existing != null)
                {
                    existing.isActive = savedBuyer.isActive;
                    existing.buyAmount = savedBuyer.buyAmount;
                    existing.buyDelay = savedBuyer.buyDelay;
                }
            }
        }

        return data;
    }
}