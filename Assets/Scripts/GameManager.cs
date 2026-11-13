using UnityEngine;
using TMPro; // Usamos TextMeshPro para textos de UI limpios y de alta calidad

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Estadísticas del Jugador")]
    [SerializeField] private int monedas = 0;
    [SerializeField] private int vidasMaximas = 3;
    private int vidasActuales;

    [Header("Referencias de Interfaz de Usuario (UI)")]
    [Tooltip("Arrastra aquí el texto de TextMeshPro para el contador de monedas")]
    [SerializeField] private TextMeshProUGUI textoMonedas;

    [Tooltip("Arrastra aquí el texto de TextMeshPro para mostrar las vidas")]
    [SerializeField] private TextMeshProUGUI textoVidas;

    private void Awake()
    {
        // Configuración del patrón Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        vidasActuales = vidasMaximas;
        ActualizarUI();
    }

    #region Gestión de Monedas
    public void SumarMonedas(int cantidad)
    {
        monedas += cantidad;
        ActualizarUI();
    }
    #endregion

    #region Gestión de Vidas
    public void ModificarVidas(int cantidad)
    {
        vidasActuales += cantidad;
        vidasActuales = Mathf.Clamp(vidasActuales, 0, vidasMaximas);

        ActualizarUI();

        if (vidasActuales <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("¡Juego Terminado! El personaje ha perdido todas sus vidas.");
        // Reiniciar Escena si muere el personaje
    }
    #endregion

    #region Actualización de UI
    private void ActualizarUI()
    {
        if (textoMonedas != null)
        {
            textoMonedas.text = "Monedas: " + monedas;
        }

        if (textoVidas != null)
        {
            textoVidas.text = "Vidas: " + vidasActuales;
        }
    }
    #endregion
}
