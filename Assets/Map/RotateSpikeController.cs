using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSpikeController : MonoBehaviour
{
    //돌아라 회전 스파이크~~~~~~~~~
    //까시좋아까시좋아까시좋아까시좋아까시좋아까시좋아까시좋아까시좋아까시좋아까시좋아까시좋아까시좋아까시좋아까시좋아까시좋아까시좋아까시좋아까시좋아까시좋아까시좋아

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1) * 180 * Time.deltaTime);
    }
}
