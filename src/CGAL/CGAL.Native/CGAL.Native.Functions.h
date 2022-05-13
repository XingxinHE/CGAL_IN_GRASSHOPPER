#pragma once

#include "pch.h"

CGALNATIVE_C_FUNCTION
void OrientedBoundingBoxBySurfaceMesh(
	double* vert_xyz_array, size_t vert_count, /* input - mesh vertices */
	int* face_index_array, size_t faces_count, /* input - mesh faces */
	double*& obb_pts_xyz
);

CGALNATIVE_C_FUNCTION
void ReleaseDoubleArray(double* arr);