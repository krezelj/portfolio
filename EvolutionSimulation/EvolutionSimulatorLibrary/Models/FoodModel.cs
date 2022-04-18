using EvolutionSimulatorLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulatorLibrary.Models
{
    public class FoodModel : EntityModel, IEdible
    {
        public double Energy { get; set; }

        private static int[] sizeDistribution = new int[] {1,
                                                           2, 2,
                                                           3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
                                                           3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
                                                           4, 4, 4, 4, 4,
                                                           5, 5, 5,
                                                           6, 6,
                                                           7};

        public FoodModel()
        {
            // Initialize values
            Position = new Vector2(Rng.Next(0, Plane.Width - 1), Rng.Next(0, Plane.Height - 1));
            //Size = sizeDistribution[Rng.Next(0, sizeDistribution.Length)];
            //Energy = Math.Pow(Size, 3) * 2;
            Size = 3;
            Energy = 50;
            // -----------------
            UpdateSector();
        }

        public void UpdateSector()
        {
            if (Sector != null)
                Sector.Food.Remove(this);
            Sector = FindSector();
            Sector.Food.Add(this);
        }

        public override void Delete()
        {
            // TODO - Should this logic be here? (Same as in creature's case)
            Plane.Food.Remove(this);
            Sector.Food.Remove(this);
        }


    }
}
