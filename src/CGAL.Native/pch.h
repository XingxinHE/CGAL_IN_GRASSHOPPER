// pch.h: This is a precompiled header file.
// Files listed below are compiled only once, improving build performance for future builds.
// This also affects IntelliSense performance, including code completion and many code browsing features.
// However, files listed here are ALL re-compiled if any one of them is updated between builds.
// Do not add files here that you will be updating frequently as this negates the performance advantage.

#ifndef PCH_H
#define PCH_H

// add headers that you want to pre-compile here
#include "framework.h"

// macros
// Windows build
#if defined (_WIN32)
#if defined (CGALNATIVE_DLL_EXPORTS)
#define CGALNATIVE_CPP_CLASS __declspec(dllexport)
#define CGALNATIVE_CPP_FUNCTION __declspec(dllexport)
#define CGALNATIVE_C_FUNCTION extern "C" __declspec(dllexport)
#else
#define CGALNATIVE_CPP_CLASS __declspec(dllimport)
#define CGALNATIVE_CPP_FUNCTION __declspec(dllimport)
#define CGALNATIVE_C_FUNCTION extern "C" __declspec(dllimport)
#endif // CGALNATIVE_DLL_EXPORTS
#endif // _WIN32

// Apple build
#if defined(__APPLE__)
#define CGALNATIVE_CPP_CLASS __attribute__ ((visibility ("default")))
#define CGALNATIVE_CPP_FUNCTION __attribute__ ((visibility ("default")))
#define CGALNATIVE_C_FUNCTION extern "C" __attribute__ ((visibility ("default")))
#endif // __APPLE__


// CGAL Library
#include <CGAL/Exact_predicates_inexact_constructions_kernel.h>
#include <CGAL/Surface_mesh.h>
#include <CGAL/optimal_bounding_box.h>

typedef CGAL::Exact_predicates_inexact_constructions_kernel    K;
typedef K::Point_3                                             Point;
typedef CGAL::Surface_mesh<Point>                              Surface_mesh;
typedef CGAL::SM_Vertex_index                                  Vertex_index;

#endif //PCH_H
