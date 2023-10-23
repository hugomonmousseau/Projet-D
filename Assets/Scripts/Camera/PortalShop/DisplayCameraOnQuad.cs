using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class DisplayCameraOnQuad : MonoBehaviour
{
    public Camera sourceCamera;  // La caméra dont le rendu sera affiché sur le Quad

    private RenderTexture cameraRenderTexture;

    [ContextMenu("prev")]
    void Start()
    {
        if (sourceCamera == null)
        {
            Debug.LogError("Source Camera not set!");
            return;
        }

        // Création de la RenderTexture
        cameraRenderTexture = new RenderTexture(Screen.width, Screen.width, 24);
        sourceCamera.targetTexture = cameraRenderTexture;

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            Debug.LogError("No MeshRenderer found on this object!");
            return;
        }

        // Assignation de la RenderTexture au material du Quad
        meshRenderer.material.mainTexture = cameraRenderTexture;
    }

}
