using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulatorLibrary.Models
{
    public class EntityModel
    {
        protected PlaneModel Plane { get { return Manager.Plane; } }

        public Random Rng { get { return Manager.Rng; } }

        public Vector2 Position { get; set; }

        public SectorModel Sector { get; set; }

        public int Size { get; set; }


        public EntityModel()
        {
            Position = new Vector2();
            Size = 1;
        }

        public EntityModel(Vector2 position, int size)
        {
            Position = position;
            Size = size;
        }


        public virtual SectorModel FindSector()
        {
            return Plane[(int)Position.X / Plane.SectorSize, (int)Position.Y / Plane.SectorSize];
        }

        protected bool CheckIfInRange(EntityModel entity)
        {
            return (Size * Size + entity.Size * entity.Size > Vector2.DistanceSq(Position, entity.Position));
        }

        public virtual void Delete()
        {

        }
    }
}
