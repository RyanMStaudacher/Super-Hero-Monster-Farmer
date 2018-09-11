using UnityEngine;
using System.Collections;

public class PostEffect : MonoBehaviour
{
    Camera AttachedCamera;
    public Shader Post_Outline;
    public Shader DrawSimple;
    Camera TempCam;
    Material Post_Mat;

	// Use this for initialization
	void Start ()
    {
        AttachedCamera = GetComponent<Camera>();
        TempCam = new GameObject().AddComponent<Camera>();
        TempCam.enabled = false;
        Post_Mat = new Material(Post_Outline);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        TempCam.CopyFrom(AttachedCamera);
        TempCam.clearFlags = CameraClearFlags.Color;
        TempCam.backgroundColor = Color.black;

        TempCam.cullingMask = 1 << LayerMask.NameToLayer("Outline");

        RenderTexture TempRT = new RenderTexture(source.width, source.height, 0, RenderTextureFormat.R8);

        TempRT.Create();

        TempCam.targetTexture = TempRT;

        TempCam.RenderWithShader(DrawSimple, "");

        Post_Mat.SetTexture("_SceneTex", source);

        Graphics.Blit(TempRT, destination, Post_Mat);

        TempRT.Release();
    }
}
