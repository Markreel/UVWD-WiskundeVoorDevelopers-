using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevMath
{
    public class Line
    {
        public Vector2 Position
        {
            get; set;
        }

        public Vector2 Direction
        {
            get; set;
        }

        public float Length
        {
            get; set;
        }

        public bool IntersectsWith(Circle circle)
        {
            //#NOTE: ik probeerde hopeloos om dit werkend te krijgen maar het lukte me steeds maar net niet. Ik snap de theorie maar heb het helaas
            //niet kunnen implementeren ;_;

            Vector2 _endPointOfLine = Position + new Vector2(Length,0);

            Vector2 _perpendicular = ClosestPointOnline(Position.x, Position.y,
                _endPointOfLine.x, _endPointOfLine.y, circle.Position.x, circle.Position.y);

            float _perpendicularDistance = Vector2.Distance(_perpendicular, circle.Position);
       
            if(_perpendicularDistance <= circle.Radius) { return true; }
            else { return false; }
        }

        public Vector2 ClosestPointOnline(float lx1, float ly1, float lx2, float ly2, float x0, float y0)
        {
            float A1 = ly2 - ly1;
            float B1 = lx1 - lx2;

            double C1 = (ly2 - ly1) * lx1 + (lx1 - lx2) * ly1;
            double C2 = -B1 * x0 + A1 * y0;
            double det = A1 * A1 - -B1 * B1;
            double cx = 0;
            double cy = 0;

            if (det != 0)
            {
                cx = (float)((A1 * C1 - B1 * C2) / det);
                cy = (float)((A1 * C2 - -B1 * C1) / det);
            }
            else
            {
                cx = x0;
                cy = y0;
            }
            return new Vector2((float)cx, (float)cy);
        }
    }
}
