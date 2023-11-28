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