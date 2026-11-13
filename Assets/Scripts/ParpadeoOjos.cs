using System.Collections; // Forma correcta de poner
using UnityEngine;

public class ParpadeoOjos : MonoBehaviour
{
    [Header("Configuración de Tiempos")]
    [Tooltip("Tiempo mínimo entre cada parpadeo (segundos)")]
    [SerializeField] private float tiempoMinimo = 2f;

    [Tooltip("Tiempo máximo entre cada parpadeo (segundos)")]
    [SerializeField] private float tiempoMaximo = 5f;

    [Tooltip("Duración de la animación del parpadeo (segundos)")]
    [SerializeField] private float duracionParpadeo = 0.12f;

    [Header("Referencias")]
    [Tooltip("El objeto de los ojos (si se deja vacío, usará este mismo objeto)")]
    [SerializeField] private Transform ojosTransform;

    private Vector3 escalaOriginal;

    private void Start()
    {
        // Si no asignas un transform, toma el del propio objeto
        if (ojosTransform == null)
        {
            ojosTransform = transform;
        }

        // Guarda la escala inicial para poder restaurarla
        escalaOriginal = ojosTransform.localScale;

        // Inicia el bucle de parpadeo
        StartCoroutine(RutinaParpadeo());
    }

    private IEnumerator RutinaParpadeo()
    {
        while (true)
        {
            // Espera un tiempo aleatorio antes del siguiente parpadeo
            float tiempoEspera = Random.Range(tiempoMinimo, tiempoMaximo);
            yield return new WaitForSeconds(tiempoEspera);

            // 1. Cerrar ojos (Reducir la escala Y a casi cero)
            float tiempoTranscurrido = 0f;
            float mitadDuracion = duracionParpadeo / 2f;

            Vector3 escalaCerrada = new Vector3(escalaOriginal.x, escalaOriginal.y * 0.05f, escalaOriginal.z);

            while (tiempoTranscurrido < mitadDuracion)
            {
                ojosTransform.localScale = Vector3.Lerp(escalaOriginal, escalaCerrada, tiempoTranscurrido / mitadDuracion);
                tiempoTranscurrido += Time.deltaTime;
                yield return null;
            }

            // 2. Abrir ojos (Volver a la escala original)
            tiempoTranscurrido = 0f;
            while (tiempoTranscurrido < mitadDuracion)
            {
                ojosTransform.localScale = Vector3.Lerp(escalaCerrada, escalaOriginal, tiempoTranscurrido / mitadDuracion);
                tiempoTranscurrido += Time.deltaTime;
                yield return null;
            }

            // Asegurar que quede exactamente en la escala original
            ojosTransform.localScale = escalaOriginal;
        }
    }
}
