# Sound Randomizer Unity Script

This repository contains a Unity script designed for randomizing and playing audio clips within a Unity game environment.

## Features

- **Randomized Audio Playback:** Utilize an array of audio clips to play random sounds with adjustable volume and pitch variations.
- **Flexible Integration:** Easily integrate the script into Unity projects by specifying a sound player prefab and adjusting volume/pitch ranges for randomized playback.
- **Disabling Functionality:** Includes an option to disable sound playback when necessary within the game environment.
- **Audio Template:** Utilizes a template to enable the utilization of audio mixers, spatial blend, and enhanced visualization of the 3D sound settings, without overcomplicating the scripts settings.
- **Efficient Cleanup:** Incorporates an `IEnumerator` coroutine to manage the destruction of the instantiated sound player GameObject after the audio finishes playing, ensuring efficient resource management.

## Usage

To use this script in your Unity project, follow these steps:
1. Add the `SoundRandomizer.cs` script to your project.
2. Create a prefab or (child) gameobject with an `audio source` that the script will use as a basis.
3. Customize the settings for audio clips, volume, and pitch ranges within the Unity Inspector.
4. Call the `PlayRandomizedSound()` method to trigger random sound playback in your game.

## Code

```csharp
using System.Collections;
using UnityEngine;

public class SoundRandomizer : MonoBehaviour
{
    [Header("Sound Player Settings")]
    [Tooltip("Prefab of the sound player GameObject")]
    public GameObject soundPlayerPrefab;

    [Space(10)]

    [Tooltip("Array of audio clips to be randomized")]
    public AudioClip[] sounds;

    [Tooltip("Minimum and maximum volume for randomization")]
    [Range(0.0f, 1.0f)]
    public float minVolume = 0.5f;
    [Range(0.0f, 1.0f)]
    public float maxVolume = 1.0f;

    [Tooltip("Minimum and maximum pitch for randomization")]
    [Range(0.1f, 3.0f)]
    public float minPitch = 0.8f;
    [Range(0.1f, 3.0f)]
    public float maxPitch = 1.2f;

    [Space(10)]

    [Header("Extra")]
    public bool disabled;

    public void PlayRandomizedSound()
    {
        if (disabled) return;

        GameObject soundPlayer = Instantiate(soundPlayerPrefab, transform.position, Quaternion.identity);
        AudioSource audioSource = soundPlayer.GetComponent<AudioSource>();

        // Randomize AudioClip, volume, and pitch
        audioSource.clip = sounds[Random.Range(0, sounds.Length)];
        audioSource.volume = Random.Range(minVolume, maxVolume);
        audioSource.pitch = Random.Range(minPitch, maxPitch);

        // Play the sound
        audioSource.Play();

        // Start coroutine to handle destruction after sound finishes playing
        StartCoroutine(DestroyAfterSoundFinished(soundPlayer, audioSource.clip.length));
    }

    IEnumerator DestroyAfterSoundFinished(GameObject soundPlayer, float soundLength)
    {
        yield return new WaitForSeconds(soundLength + 0.1f); // Adjust delay if needed
        Destroy(soundPlayer);
    }
}
