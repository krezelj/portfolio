using DataStructuresLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchedConicsLibrary
{
    public class BodyModel
    {
        

        #region PROPERTIES

        public Vector2 PositionLocal { get; set; }

        public Vector2 PositionGlobal { get; set; }

        public Vector2 VelocityLocal { get { return Orbit == null ? new Vector2() : Orbit.Velocity; } }

        public Vector2 VelocityGlobal { get; set; }

        public double Radius { get; set; }

        public double Mass { get; set; }

        public double GParameter { get; set; }

        public double SOIRadius { get; set; }

        public List<BodyModel> ChildrenBodies { get; set; } = new List<BodyModel>();

        public OrbitModel Orbit { get; set; }

        public bool IsDynamic { get; set; } = true;

        public bool OnRails { get; set; } = true;

        #endregion

        #region UI PROPERTIES

        public Color BodyColor { get; set; }

        #endregion

        #region CONSTRUCTORS

        public BodyModel(Vector2 positionGlobal, Vector2 velocity, double radius, double mass, Color bodyColor)
        {
            PositionGlobal = positionGlobal;
            PositionLocal = positionGlobal;
            VelocityGlobal = velocity;

            Radius = radius;
            Mass = mass;
            GParameter = Mass * Space.G;
            IsDynamic = false;
            SOIRadius = double.MaxValue;

            BodyColor = bodyColor;
        }

        public BodyModel(BodyModel parentBody, Vector2 positionLocal, Vector2 velocity, double radius, double mass, Color bodyColor, Color orbitColor)
        {
            parentBody.ChildrenBodies.Add(this);

            PositionLocal = positionLocal;
            PositionGlobal = positionLocal + parentBody.PositionGlobal;
            VelocityGlobal = velocity + parentBody.VelocityGlobal;

            Radius = radius;
            Mass = mass;
            GParameter = Mass * Space.G;
            Orbit = new OrbitModel(parentBody, positionLocal, velocity, orbitColor);
            SOIRadius = Orbit.A * Math.Pow((mass / parentBody.Mass), 0.4);

            BodyColor = bodyColor;
        }

        public BodyModel(OrbitModel orbit, double radius, double mass, Color bodyColor)
        {
            orbit.ParentBody.ChildrenBodies.Add(this);

            PositionLocal = orbit.Radius;
            PositionGlobal = orbit.ParentBody.PositionGlobal + PositionLocal;
            VelocityGlobal = VelocityLocal + orbit.ParentBody.VelocityGlobal;

            Radius = radius;
            Mass = mass;
            GParameter = Mass * Space.G;
            SOIRadius = Orbit.A * Math.Pow((mass / orbit.ParentBody.Mass), 0.4);

            BodyColor = bodyColor;
        }

        #endregion

        public void UpdateSOI()
        {
            BodyModel newParent;
            if (Orbit.Radius.Lenght > Orbit.ParentBody.SOIRadius)
            {
                newParent = Orbit.ParentBody.Orbit.ParentBody;
                Orbit = new OrbitModel(newParent, PositionGlobal - newParent.PositionGlobal, VelocityLocal + Orbit.ParentBody.VelocityLocal, Orbit.OrbitColor);
                Orbit.ParentBody.ChildrenBodies.Remove(this);
                newParent.ChildrenBodies.Add(this);
                return;
            }
            foreach (var neighbourBody in Orbit.ParentBody.ChildrenBodies)
            {
                if (neighbourBody != this && (Orbit.Radius - neighbourBody.Orbit.Radius).Lenght < neighbourBody.SOIRadius)
                {
                    newParent = neighbourBody;
                    Orbit = new OrbitModel(newParent, PositionGlobal - newParent.PositionGlobal, VelocityLocal - newParent.VelocityLocal, Orbit.OrbitColor);
                    Orbit.ParentBody.ChildrenBodies.Remove(this);
                    newParent.ChildrenBodies.Add(this);
                    return;
                }
            }
        }

        public void UpdateLocalPosition(double deltaTime)
        {
            if (Orbit != null)
            {
                Orbit.Update(deltaTime);
                PositionLocal = Orbit.Radius;
            }
        }

        public void UpdateGlobalPosition()
        {
            if (Orbit != null)
                PositionGlobal = Orbit.ParentBody.PositionGlobal + PositionLocal;
            foreach (var child in ChildrenBodies)
            {
                child.UpdateGlobalPosition();
            }
        }

        public void UpdateGlobalVelocity()
        {
            if (Orbit != null)
                VelocityGlobal = Orbit.ParentBody.VelocityGlobal + VelocityLocal;
            else
                VelocityGlobal = VelocityLocal;
            foreach (var child in ChildrenBodies)
            {
                child.UpdateGlobalVelocity();
            }

        }
    }
}
