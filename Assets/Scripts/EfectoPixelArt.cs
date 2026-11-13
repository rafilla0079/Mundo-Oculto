using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class EfectoPixelArt : MonoBehaviour
{
    [Header("Configuración del Pixelado")]
    [Tooltip("Ancho de la pantalla simulada en píxeles (ej: 320 o 640)")]
    [SerializeField] private int anchoPixel = 320;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        // Calcula el alto proporcional para no deformar la imagen
        int altoPixel = Mathf.RoundToInt(anchoPixel / cam.aspect);

        // Crea una textura temporal de baja resolución
        RenderTexture bufferBajo = RenderTexture.GetTemporary(anchoPixel, altoPixel, 0, source.format);
        bufferBajo.filterMode = FilterMode.Point; // Mantiene los bordes de píxel nítidos

        // Dibuja la pantalla en baja resolución y luego la escala de vuelta
        Graphics.Blit(source, bufferBajo);
        Graphics.Blit(bufferBajo, destination);

        // Libera la textura temporal de la memoria
        RenderTexture.ReleaseTemporary(bufferBajo);
    }
}
