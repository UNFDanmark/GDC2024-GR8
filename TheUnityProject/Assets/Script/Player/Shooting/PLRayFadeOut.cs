using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLRayFadeOut : MonoBehaviour
{
    MeshRenderer rayMesh;
    void Awake()
    {
        rayMesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rayMesh.material.color = new Color(rayMesh.material.color.r, rayMesh.material.color.g, rayMesh.material.color.b, rayMesh.material.color.a - 0.1f);
    }
}
