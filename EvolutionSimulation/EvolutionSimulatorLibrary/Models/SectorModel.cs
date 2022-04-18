using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulatorLibrary.Models
{
    public class SectorModel
    {
        public Vector2 Coordinates { get; set; }

        public int Size { get; set; }

        public List<CreatureModel> Creatures { get; set; } = new List<CreatureModel>();

        public List<FoodModel> Food { get; set; } = new List<FoodModel>();


        public SectorModel(int column, int row, int size)
        {
            Coordinates = new Vector2(column, row);
            Size = size;
        }
    }
}
