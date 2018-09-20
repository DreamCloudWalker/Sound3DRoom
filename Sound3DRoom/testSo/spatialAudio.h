// ================================================
// Oppo 3D Audio (o3da) API
// ================================================

#ifndef __NativeCode_H__
#define __NativeCode_H__

#if 0
#define EXPORT_DLL __declspec(dllexport) //导出dll声明
#else
#define EXPORT_DLL
#endif

extern "C" {
    EXPORT_DLL int initO3d();
    EXPORT_DLL void deinitO3d();
    EXPORT_DLL void playO3d();
    EXPORT_DLL void stopO3d();
    EXPORT_DLL void setListenerPosition(float x, float y, float z);
    EXPORT_DLL void setAudioPosition(long id, float x, float y, float z);
}

#endif
