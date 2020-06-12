using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevMath
{
    public class Rigidbody
    {
        public Vector2 Velocity
        {
            get; private set;
        }

        public float mass = 1.0f;
        public float force = 150.0f;
        public float dragCoefficient = .47f;

        public void AddForce(Vector2 forceDirection, float deltaTime)
        {
            Vector2 vector2 = forceDirection * force / mass * deltaTime;
            Velocity = new Vector2()
            {
                x = (float)(1.0 / dragCoefficient * Math.Exp(-dragCoefficient / mass * deltaTime) * (dragCoefficient * Velocity.x + mass * vector2.x) - mass * vector2.x / dragCoefficient),
                y = (float)(1.0 / dragCoefficient * Math.Exp(-dragCoefficient / mass * deltaTime) * (dragCoefficient * Velocity.y + mass * vector2.y) - mass * vector2.y / dragCoefficient)
            };
        }
    }
}
