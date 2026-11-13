using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [Header("Lista de Música")]
    [Tooltip("Arrastra aquí todas las canciones que quieras reproduzca el juego")]
    public List<AudioClip> playlist = new List<AudioClip>();

    [Header("Ajustes de Reproducción")]
    [Tooltip("¿Reproducir en orden aleatorio?")]
    public bool shuffle = false;

    [Range(0f, 1f)]
    public float volume = 0.5f;

    private AudioSource audioSource;
    private int currentTrackIndex = -1;

    private void Awake()
    {
        // Patron Singleton: Evita que se duplique el Audio Manager al cambiar de escena
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Hace que el objeto no se destruya al cargar escenas

        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = volume;
    }

    private void Start()
    {
        if (playlist.Count > 0)
        {
            PlayNextTrack();
        }
    }

    private void Update()
    {
        // Actualiza el volumen desde el Inspector en tiempo real
        audioSource.volume = volume;

        // Detecta cuándo ha terminado la canción actual para pasar a la siguiente
        if (!audioSource.isPlaying && playlist.Count > 0)
        {
            PlayNextTrack();
        }
    }

    private void PlayNextTrack()
    {
        if (playlist.Count == 0) return;

        if (shuffle && playlist.Count > 1)
        {
            int nextIndex;
            do
            {
                nextIndex = Random.Range(0, playlist.Count);
            } while (nextIndex == currentTrackIndex); // Evita repetir la misma canción consecutivamente

            currentTrackIndex = nextIndex;
        }
        else
        {
            currentTrackIndex = (currentTrackIndex + 1) % playlist.Count;
        }

        audioSource.clip = playlist[currentTrackIndex];
        audioSource.Play();
    }

    
    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume);
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }
 }