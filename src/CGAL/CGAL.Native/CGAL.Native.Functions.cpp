#include "pch.h"
#include "CGAL.Native.Functions.h"

//implement bounding box function
void OrientedBoundingBoxBySurfaceMesh(
	double* vert_xyz_array, size_t vert_count,
	int* face_index_array, size_t faces_count,
	double*& obb_xyz_array, int& obb_pts_count
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
			Point(
				vert_xyz_array[3 * i + 0],  //x
				vert_xyz_array[3 * i + 1],  //y
				vert_xyz_array[3 * i + 2]   //z
				)
		);
	}

	// fill the info of mesh faces
	for (size_t i = 0; i < faces_count; i++)
	{
		mesh.add_face(
			Vertex_index(face_index_array[3 * i + 0]),
			Vertex_index(face_index_array[3 * i + 1]),
			Vertex_index(face_index_array[3 * i + 2])
		);
	}

	// declare the 8 points as output
	std::array<Point, 8> obb_points;

	// processing
	CGAL::oriented_bounding_box(mesh, obb_points, CGAL::parameters::use_convex_hull(true));

	// [OUTPUT] extract the xyz value from 8 points
	obb_xyz_array = new double[obb_points.size() * 3];
	int i = 0, count = 0;
	for (Point pt : obb_points)
	{
		obb_xyz_array[i++] = double(pt.x());
		obb_xyz_array[i++] = double(pt.y());
		obb_xyz_array[i++] = double(pt.z());
		count++;
	}
	obb_pts_count = count;
}



void ReleaseDoubleArray(double* arr)
{
	delete[] arr;
}