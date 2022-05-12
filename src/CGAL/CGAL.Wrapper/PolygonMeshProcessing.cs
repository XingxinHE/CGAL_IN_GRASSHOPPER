using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CGAL.Wrapper
{
    public class PolygonMeshProcessing
    {
        public static List<Point3d> ObbAsPoint3d(Mesh mesh)
        {
            // clean and combine the mesh
            Mesh m = mesh.DuplicateMesh();
            m.Vertices.UseDoublePrecisionVertices = true;
            m.Faces.ConvertQuadsToTriangles();
            m.Vertices.CombineIdentical(true, true);
            m.Vertices.CullUnused();
            m.Weld(Math.PI);
            m.FillHoles();
            m.RebuildNormals();

            if (!m.IsValid || !m.IsClosed)
            {
                return null;
            }

            // info of vertices
            // the xyz coordinates of vertices of a mesh
            // mesh
            // 0: 1,0,0
            // 1: 0,1,0
            // 2: 0,0,1
            // value: 1,0,0 ,0,1,0, 0,0,1
            double[] vertXyzArray = new double[m.Vertices.Count * 3];
            for (int i = 0; i < m.Vertices.Count; i++)
            {
                vertXyzArray[i * 3 + 0] = m.Vertices.Point3dAt(i).X;
                vertXyzArray[i * 3 + 1] = m.Vertices.Point3dAt(i).Y;
                vertXyzArray[i * 3 + 2] = m.Vertices.Point3dAt(i).Z;
            }
            var vertCount = (ulong)m.Vertices.Count;


            // info of faces
            // the face indices of a mesh
            // mesh
            // 0: 3,0,2
            // 1: 3,0,1
            // 2: 0,2,1
            // 3: 2,1,3
            // value: 302 301 021 213
            int[] faceIndexArray = m.Faces.ToIntArray(true);
            var facesCount = (ulong)m.Faces.Count;

            // declare the output
            IntPtr obb_xyz_pointer = IntPtr.Zero;
            int obb_pts_count = 0;

            // CGAL Obb Processing
            UnsafeNativeMethods.OrientedBoundingBoxBySurfaceMesh(
                vertXyzArray,
                vertCount,
                faceIndexArray,
                facesCount,
                ref obb_xyz_pointer,
                ref obb_pts_count
                );

            // C++ double* => C# double[]
            double[] obb_xyz_array = new double[obb_pts_count * 3];
            Marshal.Copy(obb_xyz_pointer, obb_xyz_array, 0, obb_xyz_array.Length);

            // double[] => Point3d
            List<Point3d> points = new List<Point3d>();
            for (int i = 0; i < obb_xyz_array.Length; i+=3)
            {
                points.Add(
                    new Point3d(
                        obb_xyz_array[i + 0],   //x
                        obb_xyz_array[i + 1],   //y
                        obb_xyz_array[i + 2])); //z
            }

            // delete the obb_xyz_pointer
            UnsafeNativeMethods.ReleaseDoubleArray(obb_xyz_pointer);

            return points;
        }

        public static Box ObbAsBox(Mesh mesh)
        {
            var points = ObbAsPoint3d(mesh);
            Plane plane = new Plane(points[0], points[1], points[2]);
            Box box = new Box(plane, points);
            return box;
        }

        public static Brep ObbAsBrep(Mesh mesh)
        {
            return PolygonMeshProcessing.ObbAsBox(mesh).ToBrep();
        }
    }
}
