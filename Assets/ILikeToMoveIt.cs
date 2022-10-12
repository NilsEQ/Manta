using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ILikeToMoveIt : MonoBehaviour
{
    public float speed = 0.01f;
    public float maxheight;
    public float minheight;
    public float goodheight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0.01f, 0);
        transform.position += speed * 0.01f * transform.forward;
    }
}
