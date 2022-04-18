using DataStructuresLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchedConicsLibrary
{
    public static class Space
    {
        public static double G = 6.67408 * Math.Pow(10, -11);

        private static List<BodyModel> _bodies = new List<BodyModel>();

        private static List<BodyModel> _bodiesToAdd = new List<BodyModel>();
        public static IReadOnlyList<BodyModel> Bodies { get { return _bodies.AsReadOnly(); } }

        public static BodyModel CentralBody { get; set; }

        public static void AddBody(BodyModel bodyToAdd)
        {
            _bodiesToAdd.Add(bodyToAdd);
        }

        public static void UpdateBodyList()
        {
            foreach (var body in _bodiesToAdd)
            {
                _bodies.Add(body);
            }
            _bodiesToAdd.Clear();
        }

        public static void Update(double deltaTime)
        {
            // Update SOI
            foreach (var body in _bodies)
            {
                if (body.Orbit != null)
                {
                    body.UpdateSOI();
                }
            }
            // Update local positions of bodies
            foreach (var body in _bodies)
            {
                body.UpdateLocalPosition(deltaTime);
            }
            // Update global positions of bodies
            CentralBody.UpdateGlobalPosition();
            // Update global velocities of bodies
            CentralBody.UpdateGlobalVelocity();
        }

        public static void UpdatePredictions()
        {
            foreach (var body in _bodies)
            {
                body.Orbit?.UpdatePredictions();
            }
        }



    }
}
