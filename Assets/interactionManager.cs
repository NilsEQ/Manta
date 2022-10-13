using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionManager : MonoBehaviour
{
    [SerializeField] ILikeToMoveIt mover;


    [SerializeField] GameObject fish;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GameObject myfish = Instantiate(fish);
            mover.huntMode = true;
            mover.huntTarget = myfish;
            StartCoroutine(mover.hunt());
        }
    }
}
