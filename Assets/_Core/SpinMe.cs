using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class SpinMe : MonoBehaviour
    {

        void Update()
        {
            float zDegreesPerFrame = Time.deltaTime / 60 * 360;// TODO COMPLETE ME
            transform.RotateAround(transform.position, transform.forward, zDegreesPerFrame);
        }
    }
}

