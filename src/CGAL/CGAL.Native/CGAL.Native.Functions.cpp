#include "pch.h"
#include "CGAL.Native.Functions.h"

void OrientedBoundingBoxBySurfaceMesh(
	double* vert_xyz_array, size_t vert_count,
	int* face_index_array, size_t faces_count,
	double*& obb_pts_xyz
)
{
	// declare the surface mesh
	Surface_mesh mesh;
	mesh.clear();
	mesh.reserve(vert_count, 0, faces_count);

	// fill the info of vertices
	for (size_t i = 0; i < vert_count; i++)
	{
		mesh.add_vertex(
			Point(vert_xyz_array[3 * i + 0], //x
				vert_xyz_array[3 * i + 1], //y
				vert_xyz_array[3 * i + 2]  //z
				)
		);

	}

	// fill the info of mesh faces
	for (size_t i = 0; i < faces_count; i++)
	{
		mesh.add_face(
			Vertex_index(face_index_array[i * 3 + 0]),
			Vertex_index(face_index_array[i * 3 + 1]),
			Vertex_index(face_index_array[i * 3 + 2])
		);
	}

	// declare output
	std::array<Point, 8> obb_points;
	CGAL::oriented_bounding_box(mesh, obb_points, CGAL::parameters::use_convex_hull(true));

	obb_pts_xyz = new double[obb_points.size() * 3];

	int i = 0;
	for (Point pt : obb_points)
	{
		obb_pts_xyz[i++] = double(pt.x());
		obb_pts_xyz[i++] = double(pt.y());
		obb_pts_xyz[i++] = double(pt.z());
	}

	return;
}


void ReleaseDoubleArray(double* arr)
{
	delete[] arr;
}