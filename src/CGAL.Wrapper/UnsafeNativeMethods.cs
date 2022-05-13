using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CGAL.Wrapper
{
    internal class UnsafeNativeMethods
    {
        private const string DLL_NAME = "CGAL.Native.dll";

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OrientedBoundingBoxBySurfaceMesh(
            [MarshalAs(UnmanagedType.LPArray)]double[] vert_xyz_array, ulong vert_count,  /* input - mesh vertices */
            [MarshalAs(UnmanagedType.LPArray)] int[] face_index_array, ulong faces_count, /* input - mesh faces */
            ref IntPtr obb_pts_xyz                                                        /* output - obb as points */
        );

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ReleaseDoubleArray(IntPtr arr);
    }
}
