// ================================================
// Oppo 3D Audio (o3da) API
// ================================================

#include "spatialAudio.h"
#include <stdio.h>

extern "C" {
    int initO3d()
    {
        printf("cpp initO3d");
        return 0;
    }
    
    void deinitO3d()
    {
        printf("deinitO3d");
    }
    
    void playO3d()
    {
        printf("playO3d");
    }
    
    void stopO3d()
    {
        printf("stopO3d");
    }
    
    void setListenerPosition(float x, float y, float z)
    {
        printf("setListenerPosition");
    }
    
    void setAudioPosition(long id, float x, float y, float z)
    {
        printf("setAudioPosition");
    }
}
