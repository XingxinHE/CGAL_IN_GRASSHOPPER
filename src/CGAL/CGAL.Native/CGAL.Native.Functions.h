#pragma once

#include "pch.h"

CGALNATIVE_C_FUNCTION
void OrientedBoundingBoxBySurfaceMesh(
	double* vert_xyz_array, size_t vert_count,
	int* face_index_array, size_t faces_count,
	double*& obb_xyz_array, int& obb_pts_count
);

CGALNATIVE_C_FUNCTION
void ReleaseDoubleArray(double* arr);