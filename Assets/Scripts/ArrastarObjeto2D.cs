using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ArrastrarObjeto2D : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("Velocidad con la que el objeto sigue al ratón")]
    [SerializeField] private float velocidadArrastre = 10f;

    private Rigidbody2D rb;
    private Camera camaraPrincipal;
    private bool estaArrastrando = false;
    private Vector3 posicionRaton;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        camaraPrincipal = Camera.main;
    }

    private void OnMouseDown()
    {
        // Se activa al hacer clic izquierdo sobre el Collider del objeto
        estaArrastrando = true;
        rb.gravityScale = 0f; // Quitamos la gravedad temporalmente para moverlo con fluidez
    }

    private void OnMouseUp()
    {
        // Se activa al soltar el clic
        estaArrastrando = false;
        rb.gravityScale = 1f; // Restauramos la gravedad para que vuelva a caer/pesa
    }

    private void FixedUpdate()
    {
        if (estaArrastrando)
        {
            // Convertimos la posición del ratón en pantalla a la posición del mundo 2D
            posicionRaton = camaraPrincipal.ScreenToWorldPoint(Input.mousePosition);
            posicionRaton.z = 0f;

            // Movemos el objeto usando física hacia la posición del ratón
            Vector2 direccion = (posicionRaton - transform.position);
            rb.linearVelocity = direccion * velocidadArrastre;
        }
    }
}
