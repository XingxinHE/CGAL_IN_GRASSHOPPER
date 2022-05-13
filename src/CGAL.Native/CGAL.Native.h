// CGAL.Native.h : main header file for the CGAL.Native DLL
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'pch.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols


// CCGALNativeApp
// See CGAL.Native.cpp for the implementation of this class
//

class CCGALNativeApp : public CWinApp
{
public:
	CCGALNativeApp();

// Overrides
public:
	virtual BOOL InitInstance();

	DECLARE_MESSAGE_MAP()
};
