using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RayMaterial : MonoBehaviour
{
    public Material VanGoghMat;
    public Material CourbetMat;
    private Material nextMat;

    [SerializeField] GameObject mesh;
    SkinnedMeshRenderer meshRenderer;

    float t = 0.0f;
    bool done = false;

    ParticleSystem particles;
    void Start()
    {
        meshRenderer = mesh.GetComponent<SkinnedMeshRenderer>();
        meshRenderer.material = VanGoghMat;

        particles = GetComponent<ParticleSystem>();

        nextMat = CourbetMat;
    }



    public IEnumerator changeMat()
    {
        
        Material oldMat = meshRenderer.material;
        Color mycol = oldMat.GetColor("_EmissionColor");
        
        for (int i = 0; i < 30; i++)
        {
            oldMat.SetColor("_EmissionColor", mycol * Mathf.Lerp(1, 20, i / 30.0f));
            yield return new WaitForSeconds(0.1f);

        }

        meshRenderer.material = nextMat;
        particles.Play();
        oldMat.SetColor("_EmissionColor", mycol);

        nextMat = oldMat;
    }
}

