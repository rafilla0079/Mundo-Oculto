using UnityEngine;

public class SunRays2D : MonoBehaviour
{
    [Header("Componente")]
    [SerializeField] private SpriteRenderer sunRaysSpriteRenderer;

    [Header("Intensidad y Parpadeo")]
    [SerializeField] private float pulseSpeed = 1.5f;
    [Range(0f, 1f)] [SerializeField] private float minOpacity = 0.2f;
    [Range(0f, 1f)] [SerializeField] private float maxOpacity = 0.7f;

    [Header("Movimiento sutil")]
    [SerializeField] private bool enableWiggle = true;
    [SerializeField] private float wiggleAngle = 2.5f;
    [SerializeField] private float wiggleSpeed = 0.8f;

    private Color initialColor;

    private void Start()
    {
        if (sunRaysSpriteRenderer == null)
            sunRaysSpriteRenderer = GetComponent<SpriteRenderer>();

        if (sunRaysSpriteRenderer != null)
            initialColor = sunRaysSpriteRenderer.color;
    }

    private void Update()
    {
        if (sunRaysSpriteRenderer == null) return;

        // 1. Variación suave de la transparencia (intensidad de la luz)
        float alpha = Mathf.Lerp(minOpacity, maxOpacity, (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f);
        sunRaysSpriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);

        // 2. Oscilación suave en el ángulo (simula movimiento de hojas/viento)
        if (enableWiggle)
        {
            float zRotation = Mathf.Sin(Time.time * wiggleSpeed) * wiggleAngle;
            transform.localRotation = Quaternion.Euler(0, 0, zRotation);
        }
    }
}