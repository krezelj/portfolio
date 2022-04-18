using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulatorLibrary.Models
{
    public class PlaneModel
    {
        #region PROPERTIES
        private List<CreatureModel> _creatures { get; set; } = new List<CreatureModel>();

        public IReadOnlyCollection<CreatureModel> Creatures
        {
            get
            {
                return _creatures.AsReadOnly();
            }
        }

        private List<CreatureModel> _creaturesToKill { get; set; } = new List<CreatureModel>();

        private List<CreatureModel> _creaturesToSpawn { get; set; } = new List<CreatureModel>();

        public List<FoodModel> Food { get; set; } = new List<FoodModel>();

        public SectorModel[,] Sectors { get; set; }

        public SectorModel this[int columneIndex, int rowIndex]
        {
            get { return Sectors[columneIndex, rowIndex]; }
        }

        public Size Size { get; set; }

        public int Width { get { return Size.Width; } }

        public int Height { get { return Size.Height; } }

        public Size SizeInSectors { get; set; }

        public int SectorSize { get; set; }
        
        #endregion

        public PlaneModel(Size size)
        {
            Size = size;
            SectorSize = 120;
            SizeInSectors = new Size(Size.Width / SectorSize, Size.Height / SectorSize);
            CreateSectors();
        }

        #region METHODS
        private void CreateSectors()
        {
            Sectors = new SectorModel[SizeInSectors.Width, SizeInSectors.Width];
            for (int i = 0; i < SizeInSectors.Width; i++)
            {
                for (int j = 0; j < SizeInSectors.Height; j++)
                {
                    Sectors[i, j] = new SectorModel(i, j, SectorSize);
                }
            }
        }

        /// <summary>
        /// Spawns a set amount of new creatures on the plane.
        /// </summary>
        /// <param name="amount">Amount of creatures to spawn.</param>
        public void SpawnCreatures(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                _creatures.Add(new CreatureModel());
            }
        }

        /// <summary>
        /// Spawns all pending creatures on the plane.
        /// </summary>
        public void SpawnCreatures()
        {
            foreach (var creature in _creaturesToSpawn)
            {
                _creatures.Add(creature);
            }
            _creaturesToSpawn.Clear();
        }

        /// <summary>
        /// Sets a creature to be spawned in the next tick.
        /// </summary>
        /// <param name="creature">Creature to spawn.</param>
        public void SpawnCreature(CreatureModel creature)
        {
            _creaturesToSpawn.Add(creature);
        }

        /// <summary>
        /// Kills all pending creatures.
        /// </summary>
        public void KillCreatures()
        {
            foreach (var creature in _creaturesToKill)
            {
                _creatures.Remove(creature);
            }
            _creaturesToKill.Clear();
        }

        /// <summary>
        /// Sets a creature to be killed in the next tick.
        /// </summary>
        /// <param name="creature"></param>
        public void KillCreature(CreatureModel creature)
        {
            _creaturesToKill.Add(creature);
        }

        public void SpawnFood(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Food.Add(new FoodModel());
            }
        }

        public CreatureModel GetClosestCreature(int x, int y, int range)
        {
            Vector2 position = new Vector2(x, y);
            double distanceSq;
            double minDistance = range * range;
            CreatureModel closestCreature = null;
            foreach (var creature in Creatures)
            {
                distanceSq = Vector2.DistanceSq(position, creature.Position);
                if (distanceSq < minDistance)
                {
                    minDistance = distanceSq;
                    closestCreature = creature;
                }
            }
            return closestCreature;
        }

        #endregion



    }
}
