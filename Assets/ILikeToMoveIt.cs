using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

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
    public bool starthuntmode;
    public GameObject huntTarget;
    

    private Vector3 Target;
    private float CurrentSpeed;
    private int rotateDir;
    private float timer;

    [SerializeField] RayMaterial MatScript;
    private bool eaten;
    // Start is called before the first frame update
    void Start()
    {
        float x = calcX(goodheight);
        float z = calcZ(goodheight);
        Target = new Vector3(Random.Range(-x, x), goodheight, Random.Range(-z, z));
        CurrentSpeed = Random.Range(minspeed, maxspeed);

        var arr1 = new[] { -1, 1 };
        rotateDir = arr1[Random.Range(0,2)];

        huntMode = false;
        eaten = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!huntMode)
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
                    transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f);

                    transform.Rotate(0, rotateDir * rotateSpeed * Time.deltaTime, 0);
                    transform.position += (turnSpeed * Time.deltaTime * 0.01f * transform.forward);
                    timer = 0.0f;
                }
                else
                {
                    timer += Time.deltaTime;
                    float angleTarget = Mathf.Asin(diff.y / Mathf.Sqrt(Mathf.Pow(diff.x, 2) + Mathf.Pow(diff.y, 2)));
                    transform.rotation = Quaternion.Euler(-Mathf.Lerp(0.0f, angleTarget * 180 / Mathf.PI, timer / 5.0f), transform.rotation.eulerAngles.y, 0.0f);
                    transform.position += CurrentSpeed * Time.deltaTime * 0.01f * (Target - transform.position).normalized;
                }

            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == huntTarget)
        {
            Debug.Log("ye");
            eaten = true;
            Destroy(other.gameObject);
        }

    }

    public IEnumerator hunt()
    {
        transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f);


        float timerSurprise = 0.0f;
        while (timerSurprise < 1.0f)
        {
            yield return 0;
            timerSurprise += Time.deltaTime;
        }

        Vector3 outsideBounds = new Vector3(4, -1, 0);
        Vector3 diff = outsideBounds - transform.position;
        Vector2 diffPlane = new Vector2(diff.x, diff.z);
        Vector2 forwardPlane = new Vector2(transform.forward.x, transform.forward.z);

        float angle = Vector3.Angle(diffPlane, forwardPlane);


        while (angle > 1)
        {
            transform.Rotate(0, 100 * Time.deltaTime, 0);
            yield return 0;

            diff = outsideBounds - transform.position;
            diffPlane = new Vector2(diff.x, diff.z);
            forwardPlane = new Vector2(transform.forward.x, transform.forward.z);
            angle = Vector3.Angle(diffPlane, forwardPlane);
        }


        while (transform.position.x < 4)
        {
            transform.position += 80 * Time.deltaTime * 0.01f * transform.forward;
            yield return 0;
        }

        transform.position = new Vector3(3.5f, -1.55f, 0);

        transform.rotation = Quaternion.Euler(0, -90, 0);

        float waiting = 0.0f;
        while (waiting < 1.0f)
        {
            yield return 0;
            waiting += Time.deltaTime;
        }

        while (!eaten)
        {
            transform.position += 80 * Time.deltaTime * 0.01f * transform.forward;
            yield return 0;

        }
        eaten = false;

        float finaltimer = 0.0f;
        while (finaltimer < 1.0f)
        {
            finaltimer += Time.deltaTime;
            transform.position += 20 * Time.deltaTime * 0.01f * transform.forward;
            yield return 0;

        }

        StartCoroutine(MatScript.changeMat());

        finaltimer = 0.0f;
        while (finaltimer < 4.0f)
        {
            finaltimer += Time.deltaTime;
            transform.position += 20 * Time.deltaTime * 0.01f * transform.forward;
            yield return 0;

        }

        newTarget();
        huntMode = false;

    }


    void newTarget()
    {
        float y = Random.Range(minheight, maxheight);
        float x = calcX(goodheight);
        float z = calcZ(goodheight);
        Target = new Vector3(Random.Range(-x, x), y, Random.Range(-z, z));

        CurrentSpeed = Random.Range(minspeed, maxspeed);

        var arr1 = new[] { -1, 1 };
        rotateDir = arr1[Random.Range(0, 2)];
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
