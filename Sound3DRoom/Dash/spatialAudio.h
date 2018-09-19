// ================================================
// Oppo 3D Audio (o3da) API
// ================================================


#ifndef SPATIAL_AUDIO_H
#define SPATIAL_AUDIO_H

#ifdef __cplusplus
extern "C" {
#endif

#define MAX_AUDIO_SOURCE  8
#define REVERB_PRESET 1

//The Orientation Frame defines the direction that the Listener is facing, in 3D.
//The elements are the elements of a Rotation Matrix,
//which is a common way to represent rotations - read
//about it here for example: https://en.wikipedia.org/wiki/Rotation_matrix
struct OrientationFrame {
    float e11;
    float e12;
    float e13;
    float e21;
    float e22;
    float e23;
    float e31;
    float e32;
    float e33;
};

typedef struct SpatialAudioSession_ SpatialAudioSession;
typedef struct OrientationFrame OrientationFrame;
typedef struct AudioSourceWrapper AudioSourceWrapper;
typedef struct ReverbWrapper ReverbWrapper;

typedef enum SpatialAudioStatus {
    SPATIAL_AUDIO_FAILED = 0,
    SPATIAL_AUDIO_SUCCESS = 1,
    SPATIAL_AUDIO_ERROR_INVALID_ARGUMENT = -2,    //One of the arguments was invalid, either null or not appropriate for the operation requested.
} spatial_audio_status;

typedef enum SampleRate {
    S_44100 = 44100,
    S_48000 = 48000,
} sample_rate;

/**
 * @brief Create an 3d audio session
 */
int AudioSession_create(void *env, void *application_context,
                        SpatialAudioSession **out_session_pointer, const char *package_name);

// ================================================
// Oppo 3D Audio (o3da) API
// ================================================

/**
 * @brief Allocate a spatial context
 * @param sampleRate 44100 Hz or 48000 Hz.
 * @param blockSize the size of each frame. Must be greater than zero.
 * @return An instance of a spatial context with the listener looking the refrence direction.
 */
int
AudioSession_initialize(SpatialAudioSession *session, const int sampleRate, const int blockSize);

/**
 * @brief Set the head's orientation in relation to the reference orientation.
 * @param instance An instance of a spatial context.
 * @param a row-major 3x3 matrix forming the frame of reference for the head in the world coordinate frame.
 */
int AudioSession_setListenerOrientation(SpatialAudioSession *session, OrientationFrame orientation);

int AudioSession_createAudioSource(SpatialAudioSession *session, AudioSourceWrapper **source);

int AudioSession_setAudioSourcePosition(SpatialAudioSession *session, AudioSourceWrapper *as,
                                        float xPos,
                                        float yPos, float zPos);

int AudioSession_stopAudioSource(SpatialAudioSession *session, AudioSourceWrapper *as);


/**
 * @brief Creates and registeres a reverb in a context. There can only be one reverb for each context.
 * @param context A context that does not already have a reverb
 * @param preset Determines what kind of reverb effect is set in the reverb.
    Currently only supports one preset: 1 - A recording studio.
 * @return A pointer to the reverb or null[ptr on error.
 */
int
AudioSession_createReverb(SpatialAudioSession *session, ReverbWrapper **reverb, const int preset);

int
AudioSession_processSourceAndReverbPCM16(SpatialAudioSession *session, ReverbWrapper *reverb,
                                         AudioSourceWrapper *as, short *input, int num_samples,
                                         float xPos, float yPos, float zPos);

/**
 * @brief Perform spatialization of the input samples
 * @param input A pointer to the samples to spatialize. The maximum number of samples processed is detremined by the blockSize parmaeter
 * when instatinating a context.
 * @param num_samples Number of samples to process in the input buffer. This number can be less or equal to the blockSize parmaeter.
 * @return 1 on success else 0.
 */
int
AudioSession_processSourceAndReverbPCM32FP(SpatialAudioSession *session, ReverbWrapper *reverb,
                                           AudioSourceWrapper *as, float *input, int num_samples,
                                           float xPos, float yPos, float zPos);

/**
 * @brief pause the track output which associated with the provided player context.
 * @param context pointer to a player context.
 * @return  1 if context is released else 0.
 */
int AudioSession_pause(SpatialAudioSession *session);

/**
 * @brief start the track output which associated with the provided player context.
 * @param context pointer to a player context.
 * @return  1 if context is released else 0.
 */
int AudioSession_start(SpatialAudioSession *session);

/**
 * @brief flush the track output which associated with the provided player context.
 * @param context pointer to a player context.
 * @return  1 if context is released else 0.
 */
//int AudioSession_flush(SpatialAudioSession *session, AudioPlayerCtx *ctx);

/**
 * @brief stop the track output which associated with the provided player context.
 * @param context pointer to a player context.
 * @return  1 if context is released else 0.
 */
int AudioSession_stop(SpatialAudioSession *session);

/**
* @brief Unregister a reverb from a context. After this call it is possible to register a new reverb.
* @param context The context which has a registered record of the reverb.
* @param audio_source A pointer to the reverb.
* @return 1 in success else 0
*/
int AudioSession_destroyReverb(SpatialAudioSession *session, ReverbWrapper *reverb);

/**
 * @brief Deallocate memory held by the spatial context.
 * @param context pointer to a context.
 * @return  1 if context is released else 0.
 */
int AudioSession_shutdown(SpatialAudioSession *session);

int AudioSession_destroyAudioSource(SpatialAudioSession *session, AudioSourceWrapper *audio_source);

int AudioSession_get3DAudioVersion(SpatialAudioSession *session, const char **version);

#ifdef __cplusplus
}
#endif
#endif //SPATIAL_AUDIO_H
