using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [Header("Objetivo a seguir")]
    [Tooltip("Arrastra aquí el Transform de tu personaje")]
    public Transform target;

    [Header("Configuración de Suavizado")]
    [Tooltip("Tiempo aproximado que tarda la cámara en alcanzar al personaje (menor valor = más rápida)")]
    public float smoothTime = 0.25f;

    [Tooltip("Desplazamiento de la cámara respecto al personaje (ej. para centrar o subir la vista)")]
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    [Header("Límites de Pantalla (Opcional)")]
    public bool useBounds = false;
    public Vector2 minBounds;
    public Vector2 maxBounds;

    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (target == null) return;

        // Calculamos la posición destino agregando el offset
        Vector3 targetPosition = target.position + offset;

        // Si tenemos límites de mapa activados, restringimos la posición X e Y
        if (useBounds)
        {
            targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);
        }

        // Movimiento suave con SmoothDamp
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
