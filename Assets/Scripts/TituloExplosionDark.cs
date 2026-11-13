using UnityEngine;
using System.Collections;

public class TituloExplosionDark : MonoBehaviour
{
    [Header("Configuración de Gotas Dark")]
    [Tooltip("Arrastra un prefab opcional, o déjalo vacío para crear gotas negras automáticas")]
    public GameObject prefabGota;
    public int cantidadGotas = 25;
    public float fuerzaExplosion = 6f;

    [Header("Tiempos del Efecto")]
    [Tooltip("Tiempo que el texto permanece invisible antes de reconstruirse")]
    public float tiempoInvisible = 1.5f;

    private Renderer[] renderersDelTitulo;
    private bool estaExplotando = false;

    private void Start()
    {
 
        renderersDelTitulo = GetComponentsInChildren<Renderer>();

    }

    private void OnMouseEnter()
    {
       
        if (estaExplotando) return;

        StartCoroutine(SecuenciaExplosionYReaparicion());
    }

    private IEnumerator SecuenciaExplosionYReaparicion()
    {
        estaExplotando = true;

      
        LanzarGotasDark();

       
        AlternarRenderers(false);

        yield return new WaitForSeconds(tiempoInvisible);

     
        AlternarRenderers(true);

        estaExplotando = false;
    }

    private void AlternarRenderers(bool estado)
    {
        foreach (Renderer r in renderersDelTitulo)
        {
            if (r != null)
            {
                r.enabled = estado;
            }
        }
    }

    private void LanzarGotasDark()
    {
        for (int i = 0; i < cantidadGotas; i++)
        {
            GameObject gota;

            if (prefabGota != null)
            {
                gota = Instantiate(prefabGota, transform.position, Quaternion.identity);
            }
            else
            {
               
                gota = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                gota.transform.position = transform.position;
                gota.transform.localScale = Vector3.one * 0.12f; // Tamaño de gota

                Renderer rend = gota.GetComponent<Renderer>();
                if (rend != null)
                {
                    rend.material.color = new Color(0.08f, 0.08f, 0.08f); // Color negro/dark
                }
            }

          
            Rigidbody rb = gota.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = gota.AddComponent<Rigidbody>();
            }

          
            Vector3 direccionAleatoria = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(0.1f, 1f),
                Random.Range(-1f, 1f)
            ).normalized;

            rb.AddForce(direccionAleatoria * Random.Range(fuerzaExplosion * 0.5f, fuerzaExplosion), ForceMode.Impulse);

            
            Destroy(gota, 2f);
        }
    }
}