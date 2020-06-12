using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevMath
{
    public class Matrix4x4
    {
        public float[][] m = new float[4][] { new float[4], new float[4], new float[4], new float[4] };

        public Matrix4x4(float[][] m)
        {
            this.m = m;
        }

        private static float[] multiplyMatrixRow(Matrix4x4 lhs, Matrix4x4 rhs, int i)
        {
            return new float[4] {
                lhs.m[i][0] * rhs.m[0][0] + lhs.m[i][1] * rhs.m[1][0] + lhs.m[i][2] * rhs.m[2][0] + lhs.m[i][3] * rhs.m[3][0],
                lhs.m[i][0] * rhs.m[0][1] + lhs.m[i][1] * rhs.m[1][1] + lhs.m[i][2] * rhs.m[2][1] + lhs.m[i][3] * rhs.m[3][1],
                lhs.m[i][0] * rhs.m[0][2] + lhs.m[i][1] * rhs.m[1][2] + lhs.m[i][2] * rhs.m[2][2] + lhs.m[i][3] * rhs.m[3][2],
                lhs.m[i][0] * rhs.m[0][3] + lhs.m[i][1] * rhs.m[1][3] + lhs.m[i][2] * rhs.m[2][3] + lhs.m[i][3] * rhs.m[3][3] };
        }

        public static Matrix4x4 Identity
        {
            get
            {
                return new Matrix4x4(
                    new float[4][] {
                    new float[4] { 1,0,0,0 },
                    new float[4] { 0,1,0,0 },
                    new float[4] { 0,0,1,0 },
                    new float[4] { 0,0,0,1 } }
                    );
            }
        }

        public float Determinant
        {
            get
            {
                return
               m[0][3] * m[1][2] * m[2][1] * m[3][0] - m[0][2] * m[1][3] * m[2][1] * m[3][0] -
               m[0][3] * m[1][1] * m[2][2] * m[3][0] + m[0][1] * m[1][3] * m[2][2] * m[3][0] +
               m[0][2] * m[1][1] * m[2][3] * m[3][0] - m[0][1] * m[1][2] * m[2][3] * m[3][0] -
               m[0][3] * m[1][2] * m[2][0] * m[3][1] + m[0][2] * m[1][3] * m[2][0] * m[3][1] +
               m[0][3] * m[1][0] * m[2][2] * m[3][1] - m[0][0] * m[1][3] * m[2][2] * m[3][1] -
               m[0][2] * m[1][0] * m[2][3] * m[3][1] + m[0][0] * m[1][2] * m[2][3] * m[3][1] +
               m[0][3] * m[1][1] * m[2][0] * m[3][2] - m[0][1] * m[1][3] * m[2][0] * m[3][2] -
               m[0][3] * m[1][0] * m[2][1] * m[3][2] + m[0][0] * m[1][3] * m[2][1] * m[3][2] +
               m[0][1] * m[1][0] * m[2][3] * m[3][2] - m[0][0] * m[1][1] * m[2][3] * m[3][2] -
               m[0][2] * m[1][1] * m[2][0] * m[3][3] + m[0][1] * m[1][2] * m[2][0] * m[3][3] +
               m[0][2] * m[1][0] * m[2][1] * m[3][3] - m[0][0] * m[1][2] * m[2][1] * m[3][3] -
               m[0][1] * m[1][0] * m[2][2] * m[3][3] + m[0][0] * m[1][1] * m[2][2] * m[3][3];
            }
        }

        public Matrix4x4 Inverse
        {
            get
            {
                float determinant = Determinant;
                return new Matrix4x4(
                    new float[4][]
                    { new float [4] { (((m[1][1] * ((m[2][2] * m[3][3]) - (m[2][3] * m[3][2]))) - (m[1][2] * ((m[2][1] * m[3][3]) - (m[2][3] * m[3][1]))) + (m[1][3] * ((m[2][1] * m[3][2]) - (m[2][2] * m[3][1])))) / determinant)/*0,0*/,
                        -(((m[0][1] * ((m[2][2] * m[3][3]) - (m[2][3] * m[3][2]))) - (m[0][2] * ((m[2][1] * m[3][3]) - (m[2][3] * m[3][1]))) + (m[0][3] * ((m[2][1] * m[3][2]) - (m[2][2] * m[3][1])))) / determinant)/*1,0*/,
                        (((m[0][1] * ((m[1][2] * m[3][3]) - (m[1][3] * m[3][2]))) - (m[0][2] * ((m[1][1] * m[3][3]) - (m[1][3] * m[3][1]))) + (m[0][3] * ((m[1][1] * m[3][2]) - (m[1][2] * m[3][1])))) / determinant)/*2,0*/,
                        -(((m[0][1] * ((m[1][2] * m[2][3]) - (m[1][3] * m[2][2]))) - (m[0][2] * ((m[1][1] * m[2][3]) - (m[1][3] * m[2][1]))) + (m[0][3] * ((m[1][1] * m[2][3]) - (m[1][3] * m[2][1])))) / determinant)/*3,0*/},

                      new float [4] {-(((m[1][0] * ((m[2][2] * m[3][3]) - (m[2][3] * m[3][2]))) - (m[1][2] * ((m[2][0] * m[3][3]) - (m[2][3] * m[3][0]))) + (m[1][3] * ((m[2][0] * m[3][2]) - (m[2][2] * m[3][0])))) / determinant)/*0,1*/,
                          (((m[0][0] * ((m[2][2] * m[3][3]) - (m[2][3] * m[3][2]))) - (m[0][2] * ((m[2][0] * m[3][3]) - (m[2][3] * m[3][0]))) + (m[0][3] * ((m[2][0] * m[3][2]) - (m[2][2] * m[3][0])))) / determinant)/*1,1*/,
                          -(((m[0][0] * ((m[1][2] * m[3][3]) - (m[1][3] * m[3][2]))) - (m[0][2] * ((m[1][0] * m[3][3]) - (m[1][3] * m[3][0]))) + (m[0][3] * ((m[1][0] * m[3][2]) - (m[1][2] * m[3][0])))) / determinant)/*2,1*/,
                          (((m[0][0] * ((m[1][2] * m[2][3]) - (m[1][3] * m[2][2]))) - (m[0][2] * ((m[1][0] * m[2][3]) - (m[1][3] * m[2][0]))) + (m[0][3] * ((m[1][0] * m[2][2]) - (m[1][2] * m[2][0])))) / determinant)/*3,1*/},

                      new float [4] { (((m[1][0] * ((m[2][1] * m[3][3]) - (m[2][3] * m[3][1]))) - (m[1][1] * ((m[2][0] * m[3][3]) - (m[2][3] * m[3][0]))) + (m[1][3] * ((m[2][0] * m[3][1]) - (m[2][1] * m[3][0])))) / determinant)/*0,2*/,
                          -(((m[0][0] * ((m[2][1] * m[3][3]) - (m[2][3] * m[3][1]))) - (m[0][1] * ((m[2][0] * m[3][3]) - (m[2][3] * m[3][0]))) + (m[0][3] * ((m[2][0] * m[3][1]) - (m[2][1] * m[3][0])))) / determinant)/*1,2*/,
                          (((m[0][0] * ((m[1][1] * m[3][3]) - (m[1][3] * m[3][1]))) - (m[0][1] * ((m[1][0] * m[3][3]) - (m[1][3] * m[3][0]))) + (m[0][3] * ((m[1][0] * m[3][1]) - (m[1][1] * m[3][0])))) / determinant)/*2,2*/,
                          -(((m[0][0] * ((m[1][1] * m[2][3]) - (m[1][3] * m[2][1]))) - (m[0][2] * ((m[1][0] * m[2][3]) - (m[1][3] * m[2][0]))) + (m[0][3] * ((m[1][0] * m[2][1]) - (m[1][1] * m[2][0])))) / determinant)/*3,2*/},

                      new float [4] {-(((m[1][0] * ((m[2][1] * m[3][2]) - (m[2][2] * m[3][1]))) - (m[1][1] * ((m[2][0] * m[3][2]) - (m[2][2] * m[3][0]))) + (m[1][2] * ((m[2][0] * m[3][1]) - (m[2][1] * m[3][0])))) / determinant)/*0,3*/,
                          (((m[0][0] * ((m[2][1] * m[3][2]) - (m[2][2] * m[3][1]))) - (m[0][1] * ((m[2][0] * m[3][2]) - (m[2][2] * m[3][0]))) + (m[0][2] * ((m[2][0] * m[3][1]) - (m[2][1] * m[3][0])))) / determinant)/*1,3*/,
                          -(((m[0][0] * ((m[1][1] * m[3][2]) - (m[1][2] * m[3][1]))) - (m[0][1] * ((m[1][0] * m[3][2]) - (m[1][2] * m[3][0]))) + (m[0][2] * ((m[1][0] * m[3][1]) - (m[1][1] * m[3][0])))) / determinant)/*2,3*/,
                          (((m[0][0] * ((m[1][1] * m[2][2]) - (m[1][2] * m[2][1]))) - (m[0][1] * ((m[1][0] * m[2][2]) - (m[1][2] * m[2][0]))) + (m[0][2] * ((m[1][0] * m[2][1]) - (m[1][1] * m[2][0])))) / determinant)/*3,3*/}
                    });
            }
        }

        public static Matrix4x4 Translate(Vector3 translation)
        {
            return new Matrix4x4(
                new float[4][]
                { new float[4] { 1, 0, 0, translation.x },
                  new float[4] { 0, 1, 0, translation.y },
                  new float[4] { 0, 0, 1, translation.z },
                  new float[4] { 0, 0, 0, 1 }
                });
        }

        public static Matrix4x4 Rotate(Vector3 rotation)
        {
            return RotateX(rotation.x) * (RotateY(rotation.y) * RotateZ(rotation.z));
        }

        public static Matrix4x4 RotateX(float rotation)
        {
            return new Matrix4x4(
                new float[4][]
                { new float[4] { 1, 0, 0, 0 },
                  new float[4] { 0, (float)Math.Cos(rotation), (float)-Math.Sin(rotation), 0 },
                  new float[4] { 0, (float)Math.Sin(rotation), (float)Math.Cos(rotation), 0 },
                  new float[4] { 0, 0, 0, 1 }
                });
        }

        public static Matrix4x4 RotateY(float rotation)
        {
            return new Matrix4x4(
                new float[4][]
                { new float[4] { (float)Math.Cos(rotation), 0, (float)-Math.Sin(rotation), 0 },
                  new float[4] { 0, 1, 0, 0 },
                  new float[4] { (float)Math.Sin(rotation), 0, (float)Math.Cos(rotation), 0 },
                  new float[4] { 0, 0, 0, 1 }
                });
        }

        public static Matrix4x4 RotateZ(float rotation)
        {
            return new Matrix4x4(
                new float[4][]
                { new float[4] { (float)Math.Cos(rotation), (float)-Math.Sin(rotation), 0, 0 },
                  new float[4] { (float)Math.Sin(rotation), (float)Math.Cos(rotation), 0, 0 },
                  new float[4] { 0, 0, 1, 0 },
                  new float[4] { 0, 0, 0, 1 }
                });
        }

        public static Matrix4x4 Scale(Vector3 scale)
        {
            return new Matrix4x4(
                new float[4][]
                { new float[4] { scale.x, 0, 0, 0 },
                  new float[4] { 0, scale.y, 0, 0 },
                  new float[4] { 0, 0, scale.z, 0 },
                  new float[4] { 0, 0, 0, 1 }
                });
        }
        public static Matrix4x4 operator *(Matrix4x4 lhs, Matrix4x4 rhs)
        {
            float[][] m =
                        new float[4][]
                        {
                            multiplyMatrixRow(lhs, rhs, 0),
                            multiplyMatrixRow(lhs, rhs, 1),
                            multiplyMatrixRow(lhs, rhs, 2),
                            multiplyMatrixRow(lhs, rhs, 3)
                        };
            return new Matrix4x4(m);
        }

        public static Vector4 operator *(Matrix4x4 lhs, Vector4 rhs)
        {
            return new Vector4(lhs.m[0][0] * rhs.x + lhs.m[0][1] * rhs.y + lhs.m[0][2] * rhs.z + lhs.m[0][3] * rhs.w, lhs.m[1][0] * rhs.x + lhs.m[1][1] * rhs.y + lhs.m[1][2] * rhs.z + lhs.m[1][3] * rhs.w, lhs.m[2][0] * rhs.x + lhs.m[2][1] * rhs.y + lhs.m[2][2] * rhs.z + lhs.m[2][3] * rhs.w, lhs.m[3][0] * rhs.x + lhs.m[3][1] * rhs.y + lhs.m[3][2] * rhs.z + lhs.m[3][3] * rhs.w);
        }
    }
}
