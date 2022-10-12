using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ILikeToMoveIt : MonoBehaviour
{
    public float minspeed = 0.05f;
    public float maxspeed = 0.5f;
    public float currentSpeed = 0.5f;

    public float maxheight;
    public float minheight;
    public float goodheight;

    public float maxXhigh = 3.1f;
    public float maxXlow = 1.2f;


    public float maxZlow = 0.22f;
    public float maxZhigh= 0.4f;


    public float rotateSpeed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0);
        transform.position += currentSpeed * 0.01f * transform.forward;
    }
}
