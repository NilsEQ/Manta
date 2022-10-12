using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RayMaterial : MonoBehaviour
{
    public Material VanGoghMat;

    public Material CourbetMat;

    [SerializeField] GameObject mesh;
    SkinnedMeshRenderer meshRenderer;

    float t = 0.0f;
    bool done = false;

    ParticleSystem particles;
    void Start()
    {
        meshRenderer = mesh.GetComponent<SkinnedMeshRenderer>();
        Debug.Log(CourbetMat);
        meshRenderer.material = CourbetMat  ;
        Debug.Log(meshRenderer.materials[0]);

        particles = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        t += Time.deltaTime;

        if ( t > 10 && !done)
        {
            done = true;
            StartCoroutine(changeMat(VanGoghMat));
        }
    }

    IEnumerator changeMat(Material newmat)
    {
        Material oldMat = meshRenderer.material;
        Color mycol = oldMat.GetColor("_EmissionColor");
        
        for (int i = 0; i < 30; i++)
        {
            oldMat.SetColor("_EmissionColor", mycol * Mathf.Lerp(1, 20, i / 30.0f));
            yield return new WaitForSeconds(0.1f);

        }

        meshRenderer.material = newmat;
        particles.Play();
        oldMat.SetColor("_EmissionColor", mycol);
    }
}

