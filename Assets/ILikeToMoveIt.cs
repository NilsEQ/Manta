using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ILikeToMoveIt : MonoBehaviour
{
    public float minspeed;
    public float maxspeed;
    public float turnSpeed;

    public float maxheight;
    public float minheight;
    public float goodheight;

    public float maxXhigh = 3.1f;
    public float maxXlow = 1.2f;


    public float maxZlow = 0.22f;
    public float maxZhigh= 0.4f;


    public float rotateSpeed = 30;

    public bool huntMode;

    private Vector3 Target;
    private float CurrentSpeed;
    // Start is called before the first frame update
    void Start()
    {
        float x = calcX(goodheight);
        float z = calcZ(goodheight);
        Target = new Vector3(Random.Range(-x, x), goodheight, Random.Range(-z, z));
        CurrentSpeed = Random.Range(minspeed, maxspeed);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 diff = Target - transform.position;

        if (diff.magnitude < 0.01)
        {
            newTarget();
        }
        else
        {
            Vector2 diffPlane = new Vector2(diff.x, diff.z);
            Vector2 forwardPlane = new Vector2(transform.forward.x, transform.forward.z);

            if (Vector3.Angle(diffPlane, forwardPlane) > 1)
            {
                transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
                transform.position += turnSpeed * Time.deltaTime * 0.01f * transform.forward;
            }
            else
            {
                transform.position += CurrentSpeed * Time.deltaTime * 0.01f * (Target - transform.position).normalized;
            }
        }



        

    }

    void newTarget()
    {
        float y = Random.Range(minheight, maxheight);
        float x = calcX(goodheight);
        float z = calcZ(goodheight);
        Target = new Vector3(Random.Range(-x, x), y, Random.Range(-z, z));

        CurrentSpeed = Random.Range(minspeed, maxspeed);
    }

    float calcX(float y)
    {
        return (maxXhigh - maxXlow) * (y - minheight) / (maxheight - minheight) + maxXlow;
    }

    float calcZ(float y)
    {
        return (maxZhigh - maxZlow) * (y - minheight) / (maxheight - minheight) + maxZlow;
    }
}
