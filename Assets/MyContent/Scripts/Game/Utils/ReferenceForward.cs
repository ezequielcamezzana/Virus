using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomGizmos{
    public class ReferenceForward : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            var originPosition = transform.position;
            var targetPosition = transform.position + (transform.forward * 5);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(originPosition, 0.3f);
            Gizmos.DrawLine(originPosition, targetPosition);
        }
    }
}
