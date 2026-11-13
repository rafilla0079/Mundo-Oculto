using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class BotonMenu3D : MonoBehaviour
{
    [Header("Configuración de Escena")]
    public string nombreEscenaDestino;
    public bool esSalir = false;

    [Header("Configuración de Animación (Hover)")]
    public float velocidadEscala = 12f;
    private Vector3 escalaOriginal;
    private Vector3 escalaObjetivo;

    [Header("Configuración de Sonidos")]
    public AudioClip sonidoHover; // Sonido al pasar el ratón por encima
    public AudioClip sonidoClick; // Sonido al hacer clic
    private AudioSource audioSource;

    private void Start()
    {
       
        escalaOriginal = transform.localScale;
        escalaObjetivo = escalaOriginal;

        
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void Update()
    {
       
        transform.localScale = Vector3.Lerp(transform.localScale, escalaObjetivo, Time.deltaTime * velocidadEscala);
    }

    private void OnMouseEnter()
    {
       
        escalaObjetivo = escalaOriginal * 1.15f;

      
        if (sonidoHover != null)
        {
            audioSource.PlayOneShot(sonidoHover);
        }
    }

    private void OnMouseExit()
    {
        // Regresa a su tamaño original
        escalaObjetivo = escalaOriginal;
    }

    private void OnMouseDown()
    {
       
        escalaObjetivo = escalaOriginal * 0.90f;

        
        if (sonidoClick != null)
        {
            audioSource.PlayOneShot(sonidoClick);
        }
    }

    private void OnMouseUp()
    {
       
        escalaObjetivo = escalaOriginal * 1.15f;

        if (esSalir)
        {
            Debug.Log("Saliendo del juego...");
            Application.Quit();
        }
        else if (!string.IsNullOrEmpty(nombreEscenaDestino))
        {
            SceneManager.LoadScene(nombreEscenaDestino);
        }
    }
}