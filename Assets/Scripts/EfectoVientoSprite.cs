using UnityEngine;

public class EfectoVientoSprite : MonoBehaviour
{
    [Header("Configuración del Viento")]
    [Tooltip("Velocidad con la que el sprite oscila")]
    public float velocidad = 2.0f;

    [Tooltip("Ángulo máximo de rotación (grados)")]
    public float anguloMaximo = 5.0f;

    [Tooltip("Intensidad del estiramiento/deformación en X")]
    public float deformacionEscalaX = 0.05f;

    [Tooltip("Desfase opcional para que varios sprites no se muevan idénticos")]
    public float desfaseInicial = 0.0f;

    private Vector3 escalaOriginal;
    private Quaternion rotacionOriginal;

    void Start()
    {
        escalaOriginal = transform.localScale;
        rotacionOriginal = transform.localRotation;
        
        // Asigna un desfase aleatorio si prefieres variedad entre varios objetos
        if (desfaseInicial == 0f)
        {
            desfaseInicial = Random.Range(0f, 100f);
        }
    }

    void Update()
    {
        // Calculamos la onda con el tiempo transcurrido
        float onda = Mathf.Sin((Time.time + desfaseInicial) * velocidad);

        // 1. Aplicamos una ligera inclinación/rotación
        float rotacionZ = onda * anguloMaximo;
        transform.localRotation = rotacionOriginal * Quaternion.Euler(0f, 0f, rotacionZ);

        // 2. Aplicamos una pequeña deformación en la escala (efecto flexión)
        float nuevaEscalaX = escalaOriginal.x + (onda * deformacionEscalaX);
        transform.localScale = new Vector3(nuevaEscalaX, escalaOriginal.y, escalaOriginal.z);
    }
}
