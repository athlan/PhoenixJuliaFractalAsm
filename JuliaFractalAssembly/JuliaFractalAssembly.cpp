// JuliaFractalAssembly.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "JuliaFractalAssembly.h"


// This is an example of an exported variable
JULIAFRACTALASSEMBLY_API int nJuliaFractalAssembly=0;

// This is an example of an exported function.
JULIAFRACTALASSEMBLY_API int fnJuliaFractalAssembly(void)
{
	return 42;
}

// This is the constructor of a class that has been exported.
// see JuliaFractalAssembly.h for the class definition
CJuliaFractalAssembly::CJuliaFractalAssembly()
{
	return;
}
