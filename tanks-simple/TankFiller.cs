using System;
namespace tanks_simple
{
    public class TankFiller
    {
        public List<Tank> tanks = new List<Tank>();
        public int liters;
        public TankFiller(Tank[] availableTanks, int litersToFill)
        {
            liters = litersToFill;
            foreach (Tank tank in availableTanks)
            {
                if (tank.liters <= liters)
                {
                    liters -= tank.liters;
                    tanks.Add(tank);
                }

            }
        }
    }
}

