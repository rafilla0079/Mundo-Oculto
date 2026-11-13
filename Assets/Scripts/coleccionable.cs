using UnityEngine;

public class Coleccionable : MonoBehaviour
{
    [Header("Configuración del Coleccionable")]
    [SerializeField] private int valorMoneda = 1;

//Serializefield publica pero no puede editarla otro script

    [Header("Efectos")]
    [Tooltip("El prefab del efecto de partículas de polvo al recolectar")]
    [SerializeField] private GameObject efectoPolvoPrefab;

    [Tooltip("Sonido opcional al recolectar (AudioClip)")]
    [SerializeField] private AudioClip sonidoRecolectar;

    private bool recolectado = false; // Evita doble recolección accidental

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si quien toca la moneda es el Jugador y si no ha sido recolectada aún
        if (!recolectado && collision.CompareTag("Player"))
        {
            recolectado = true;
            Recolectar();
        }
    }

    private void Recolectar()
    {
        // 1. Sumar monedas al GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SumarMonedas(valorMoneda);
        }

        // 2. Crear efecto de polvo
        if (efectoPolvoPrefab != null)
        {
            GameObject polvo = Instantiate(efectoPolvoPrefab, transform.position, Quaternion.identity);
            Destroy(polvo, 2f); // Limpieza automática del sistema de partículas
        }

        // 3. Reproducir sonido si está asignado
        if (sonidoRecolectar != null)
        {
            AudioSource.PlayClipAtPoint(sonidoRecolectar, transform.position);
        }

        // 4. Destruir el objeto
        Destroy(gameObject);
    }
}
