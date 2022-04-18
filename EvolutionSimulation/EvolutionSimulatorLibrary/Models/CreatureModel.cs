using EvolutionSimulatorLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulatorLibrary.Models
{
    public class CreatureModel : EntityModel, IEdible
    {
        #region STATIC BASE VALUES
        private static int _baseSize = 4;
        private static double _baseSpeed = 0.75;
        private static double _baseRange = 10;
        private static double _baseExplorationRange = 30;
        private static double _basePredatoriness = 1;
        private double _mutationRate = 1;
        private double _mutationChance = 0.15;

        public static double MinSpeed = 0.25;
        public static double MaxSpeed = 3;

        public static double MinPredatoriness = 0;
        public static double MaxPredatoriness = 2;
        #endregion

        // Generic creature info ----

        public Vector2 Velocity { get; set; }

        public List<SectorModel> SectorsInRange { get; set; }

        public EntityModel Target { get; set; }

        public double Energy { get; set; }

        public int Generation { get; set; }

        // Genes ----

        public double Speed { get; set; }

        public double HuntSpeed { get; set; }

        public double Range { get; set; }

        public double ExplorationRange { get; set; }

        public double Predatoriness { get; set; }

        public double Efficiency { get; set; }

        private double _foodEnergyMultiplier { get; set; }

        private double _creatureEnergyMultiplier { get; set; }

        private int _explorationThreshold { get; set; } = 10;

        private bool _isHungry { get; set; }

        // ----------

        public CreatureModel()
        {
            // Initialize values
            Position = new Vector2(Rng.Next(0, Plane.Width - 1), Rng.Next(0, Plane.Height - 1));
            Generation = 0;

            Energy = 200;
            Size = _baseSize;
            Speed = _baseSpeed;
            Range = _baseRange;
            ExplorationRange = _baseExplorationRange;
            Predatoriness = _basePredatoriness; // + (Rng.NextDouble() > 0.8 ? 0.2 : -0.2);

            HuntSpeed = Speed * (Math.Pow(Predatoriness, 2) + 1);
            _foodEnergyMultiplier = GaussianCurve(Predatoriness, 4, 0, 0.5);
            _creatureEnergyMultiplier = GaussianCurve(Predatoriness, 1, MaxPredatoriness, 0.8);

            EvaluateEfficiency();
            UpdateSector();
        }

        public CreatureModel(CreatureModel parent)
        {
            // Initialize values
            Position = parent.Position + Vector2.Decompose(50, Rng.NextDouble() * 2 * Math.PI);
            Position.Round();
            Position.Limit(0, Plane.Width - 1, 0, Plane.Height - 1);
            Generation = parent.Generation + 1;

            Energy = 200;
            Size = parent.Size;
            Speed = parent.Speed;
            Range = parent.Range;
            ExplorationRange = parent.ExplorationRange;
            Predatoriness = parent.Predatoriness;

            Mutate();
            HuntSpeed = Speed * (Math.Pow(Predatoriness, 2) + 1);
            _foodEnergyMultiplier = GaussianCurve(Predatoriness, 4, 0, 0.5);
            _creatureEnergyMultiplier = GaussianCurve(Predatoriness, 1, MaxPredatoriness, 0.8);

            EvaluateEfficiency();
            UpdateSector();
        }

        

        public void Update()
        {
            // Check if eaten.
            if (Energy <= 0)
            {
                Delete();
                return;
            }
            // Check if hungry.
            if (Energy < 300)
            {
                _isHungry = true;
            }

            FindSectorsInRange();
            FindTarget();
            SetVelocity();
            Move();
            UpdateSector();
            Energy -= Efficiency;

            // Eat target if edible and in range.
            if (CheckIfInRange(Target))
            {
                if (Target is IEdible)
                    Eat(Target as IEdible);
                Target = null;
            }
            // Check if energy depleted.
            if (Energy <= 0)
            {
                Delete();
            }
            // Check if can reproduce.
            else if (Energy >= 600)
            {
                _isHungry = false;
                Reproduce();
            }
        }

        private void Move()
        {
            (Position += Velocity).Limit(0, Plane.Width - 1, 0, Plane.Height - 1);
        }

        private void Eat(IEdible edible)
        {
            // TODO - Take a second look at this here later.
            if (edible is CreatureModel)
            {
                Energy += ((double)Size / 4) * edible.Energy * _creatureEnergyMultiplier;
            }
            if (edible is FoodModel)
            {
                Energy += ((double)Size / 4) * edible.Energy * _foodEnergyMultiplier;
            }
            edible.Energy = 0;
            (edible as EntityModel)?.Delete();
        }

        private void Reproduce()
        {
            Energy -= 250;
            Plane.SpawnCreature(new CreatureModel(this));
        }

        private void Mutate()
        {
            Size =              Math.Max(Size + (Rng.NextDouble() > _mutationChance ? 0 : (int)_mutationRate * (Rng.Next(0, 2) * 2 - 1)), 1);

            Speed =             Speed + (Rng.NextDouble() > _mutationChance ? 0 : 0.25 * _mutationRate * (Rng.Next(0, 2) * 2 - 1));
            Speed =             Math.Max(Math.Min(Speed, MaxSpeed), MinSpeed);

            Range =             Math.Max(Range + (Rng.NextDouble() > _mutationChance ? 0 : 5 * _mutationRate * (Rng.Next(0, 2) * 2 - 1)), 10);

            ExplorationRange =  Math.Max(ExplorationRange + (Rng.NextDouble() > _mutationChance ? 0 : 5 * _mutationRate * (Rng.Next(0, 2) * 2 - 1)), 5);

            Predatoriness =     Predatoriness + (Rng.NextDouble() > _mutationChance ? 0 : 0.1 * _mutationRate * (Rng.Next(0, 2) * 2 - 1));
            Predatoriness =     Math.Max(Math.Min(Predatoriness, MaxPredatoriness), MinPredatoriness);
        }

        private void EvaluateEfficiency()
        {
            double efficiencyModifer = (Math.Pow(_baseSize, 2) * Math.Pow(_baseSpeed, 2) +
                                        Math.Pow(_baseRange / 5, 1) +
                                        Math.Pow(_baseExplorationRange / 30, 1.5)) * 7;

            Efficiency = (Math.Pow(Size, 2) * Math.Pow(Speed, 2) + 
                          Math.Pow(Range / 5, 1.5) + 
                          Math.Pow(ExplorationRange / 30, 1)) / efficiencyModifer;
        }

        public override void Delete()
        {
            // TODO - Should this logic be here
            Plane.KillCreature(this);
            Sector.Creatures.Remove(this);
        }

        private void SetVelocity()
        {
            Vector2 distanceVector = Vector2.FindVector(Position, Target.Position);
            if (distanceVector.Lenght != 0)
            {
                if (Target is CreatureModel)
                    Velocity = distanceVector * (HuntSpeed / distanceVector.Lenght);
                else
                    Velocity = distanceVector * (Speed / distanceVector.Lenght);
            }
            else
                Velocity = Vector2.Decompose(Speed, 2 * Math.PI * Rng.NextDouble());
        }

        private void FindTarget()
        {
            double minFoodDistance = Math.Pow(Range * (MaxPredatoriness - Predatoriness), 2);
            double minCreatureDistance = Math.Pow(Range * (2 * Predatoriness), 2);
            double distanceSq;
            FoodModel foodTarget = null;
            CreatureModel creatureTarget = null;
            // Find closest energy source
            foreach (var sector in SectorsInRange)
            {
                if (_isHungry)
                {
                    foreach (var food in sector.Food)
                    {
                        distanceSq = Vector2.DistanceSq(Position, food.Position);
                        if (distanceSq < minFoodDistance)
                        {
                            minFoodDistance = distanceSq;
                            foodTarget = food;
                        }
                    }
                }
                foreach (var creature in sector.Creatures)
                {
                    distanceSq = Vector2.DistanceSq(Position, creature.Position);
                    if (_isHungry && distanceSq < minCreatureDistance && (Predatoriness - creature.Predatoriness) > 0.15)
                    {
                        minCreatureDistance = distanceSq;
                        creatureTarget = creature;
                    }
                    if (distanceSq < minCreatureDistance && (Predatoriness - creature.Predatoriness) < -0.15)
                    {
                        minCreatureDistance = distanceSq;
                        creatureTarget = creature;
                    }
                }
            }
            // Choose target
            // If predator detected, flee.
            if (creatureTarget != null && creatureTarget.Predatoriness > Predatoriness)
            {
                CreateFleeTartget(creatureTarget);
            }
            // If prey detected, hunt.
            else if (creatureTarget != null)
            {
                Target = creatureTarget;
            }
            // If no prey/predator but food detected.
            else if (foodTarget != null)
            {
                Target = foodTarget;
            }
            // If no targets found create new target.
            else if (Target == null)
            {
                if (_explorationThreshold < 10)
                {
                    CreateNewTarget();
                    _explorationThreshold++;
                }
                else
                {
                    CreateExplorationTarget();
                    _explorationThreshold = Math.Max(_explorationThreshold - 2, 0);
                }
            }
        }

        private void CreateNewTarget()
        {
            Vector2 newTargetPosition = Position + Vector2.Decompose(ExplorationRange, Rng.NextDouble() * 2 * Math.PI);
            newTargetPosition.Round();
            newTargetPosition.Limit(0, Plane.Width - 1, 0, Plane.Height - 1);
            Target = new EntityModel(newTargetPosition, 3);
        }

        private void CreateFleeTartget(CreatureModel predator)
        {
            //double angle = Vector2.Angle(Position, predator.Position);
            //angle += (Rng.NextDouble() + 0.5) * Math.PI;
            //Vector2 newTargetPosition = Position - Vector2.Decompose(ExplorationRange, angle);
            Vector2 newTargetPosition = Position - Vector2.FindVector(Position, predator.Position);
            newTargetPosition.Round();
            newTargetPosition.Limit(0, Plane.Width - 1, 0, Plane.Height - 1);
            Target = new EntityModel(newTargetPosition, 3);
        }

        private void CreateExplorationTarget()
        {
            Vector2 newTargetPosition = Position + Vector2.Decompose(ExplorationRange * 4, Rng.NextDouble() * 2 * Math.PI);
            newTargetPosition.Round();
            newTargetPosition.Limit(0, Plane.Width - 1, 0, Plane.Height - 1);
            Target = new EntityModel(newTargetPosition, 3);
        }

        public List<SectorModel> FindSectorsInRange()
        {
            int minI = (int)Math.Max(Position.X - Range, 0) / Plane.SectorSize;
            int maxI = (int)Math.Min(Position.X + Range, Plane.Width - 1) / Plane.SectorSize;
            int minJ = (int)Math.Max(Position.Y - Range, 0) / Plane.SectorSize;
            int maxJ = (int)Math.Min(Position.Y + Range, Plane.Height - 1) / Plane.SectorSize;
            List<SectorModel> sectors = new List<SectorModel>();
            for (int i = minI; i <= maxI; i++)
            {
                for (int j = minJ; j <= maxJ; j++)
                {
                    sectors.Add(Plane[i, j]);
                }
            }
            SectorsInRange = sectors;
            return sectors;

        }

        public void UpdateSector()
        {
            if (Sector != null)
                Sector.Creatures.Remove(this);
            Sector = FindSector();
            Sector.Creatures.Add(this);
        }

        private double GaussianCurve(double x, double a, double b, double c)
        {
            return a * Math.Pow(Math.E, -0.5 * Math.Pow((x - b) / c, 2));
        }

    }
}
