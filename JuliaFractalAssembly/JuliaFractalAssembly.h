// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the JULIAFRACTALASSEMBLY_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// JULIAFRACTALASSEMBLY_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef JULIAFRACTALASSEMBLY_EXPORTS
#define JULIAFRACTALASSEMBLY_API __declspec(dllexport)
#else
#define JULIAFRACTALASSEMBLY_API __declspec(dllimport)
#endif

// This class is exported from the JuliaFractalAssembly.dll
class JULIAFRACTALASSEMBLY_API CJuliaFractalAssembly {
public:
	CJuliaFractalAssembly(void);
	// TODO: add your methods here.
};

extern JULIAFRACTALASSEMBLY_API int nJuliaFractalAssembly;

JULIAFRACTALASSEMBLY_API int fnJuliaFractalAssembly(void);
