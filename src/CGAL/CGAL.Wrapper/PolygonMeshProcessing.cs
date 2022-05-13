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
            // value: 100 010 001
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

            // declare output pointer
            IntPtr obb_xyz_pointer = IntPtr.Zero;

            // CGAL obb processing
            UnsafeNativeMethods.OrientedBoundingBoxBySurfaceMesh(
                vertXyzArray,
                vertCount,
                faceIndexArray,
                facesCount,
                ref obb_xyz_pointer);

            // c++ double* => c# double[]
            double[] obb_xyz_array = new double[8 * 3];
            Marshal.Copy(obb_xyz_pointer, obb_xyz_array, 0, 8 * 3);

            // double[] => List<Point3d>
            List<Point3d> points = new List<Point3d>();
            for (int i = 0; i < obb_xyz_array.Length; i+=3)
            {
                points.Add(new Point3d(
                    obb_xyz_array[i + 0],
                    obb_xyz_array[i + 1],
                    obb_xyz_array[i + 2]));
            }

            // delete the pointer
            UnsafeNativeMethods.ReleaseDoubleArray(obb_xyz_pointer);

            return points;
        }

        public static Box ObbAsBox(Mesh mesh)
        {
            var pts = ObbAsPoint3d(mesh);
            var plane = new Plane(pts[0], pts[1], pts[2]);
            var box = new Box(plane, pts);

            return box;
        }

        public static Brep ObbAsBrep(Mesh mesh)
        {
            return PolygonMeshProcessing.ObbAsBox(mesh).ToBrep();
        }
    }
}
