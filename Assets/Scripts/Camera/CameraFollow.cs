using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    //let camera follow target
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private Transform target;
        [SerializeField] private float lerpSpeed = 1.0f;
        [SerializeField] private Vector3 offset;

        private Vector3 targetPos;

        private void Start()
        {
            if (target == null) return;

            offset = cam.transform.position - target.position;
        }

        private void Update()
        {
            if (target == null) return;

            targetPos = target.position + offset;
            cam.transform.position = Vector3.Lerp(cam.transform.position, targetPos, lerpSpeed * Time.deltaTime);
        }

    }
}
