using UnityEngine;

public class SpriteBreathing : MonoBehaviour
{
    [Header("Ajustes de Respiración")]
    [Tooltip("Velocidad a la que el sprite respira")]
    public float breatheSpeed = 2f;

    [Tooltip("Intensidad de la escala al inhalar (ej. 0.05 suma un 5% a la escala original)")]
    public float breatheAmount = 0.05f;

    [Header("Ejes Afectados")]
    [Tooltip("Si es verdadero, también se expandirá suavemente a lo ancho al respirar")]
    public bool affectX = true;

    private Vector3 originalScale;

    private void Start()
    {
        // Guardamos la escala inicial del objeto
        originalScale = transform.localScale;
    }

    private void Update()
    {
        // Calculamos el factor de respiración mediante una onda senoidal (-1 a 1)
        float sinValue = Mathf.Sin(Time.time * breatheSpeed);

        // Escalamos principalmente en Y (pecho/cuerpo subiendo)
        float scaleY = originalScale.y + (sinValue * breatheAmount);

        // Opcionalmente podemos expandir X levemente
        float scaleX = affectX 
            ? originalScale.x + (sinValue * (breatheAmount * 0.5f)) 
            : originalScale.x;

        // Aplicamos la nueva escala manteniendo la Z original
        transform.localScale = new Vector3(scaleX, scaleY, originalScale.z);
    }
}
