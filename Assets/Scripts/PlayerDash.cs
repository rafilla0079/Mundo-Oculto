using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerDash : MonoBehaviour
{
    [Header("Configuración del Dash")]
    [SerializeField] private float dashForce = 24f;       // Velocidad del dash
    [SerializeField] private float dashDuration = 0.2f;    // Duración del impulso en segundos
    [SerializeField] private float dashCooldown = 1f;     // Tiempo de espera entre dashes

    [Header("Componentes Opcionales")]
    [SerializeField] private TrailRenderer trailRenderer; // Para efecto de estela visual

    private Rigidbody2D rb;
    private bool canDash = true;
    private bool isDashing = false;

    public bool IsDashing => isDashing;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isDashing) return;

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(PerformDash());
        }
    }

    private IEnumerator PerformDash()
    {
        canDash = false;
        isDashing = true;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        float direction = transform.localScale.x > 0 ? 1f : -1f;

        rb.linearVelocity = new Vector2(direction * dashForce, 0f);

        if (trailRenderer != null)
        {
            trailRenderer.emitting = true;
        }

        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = originalGravity;
        rb.linearVelocity = Vector2.zero;
        isDashing = false;

        if (trailRenderer != null)
        {
            trailRenderer.emitting = false;
        }

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}