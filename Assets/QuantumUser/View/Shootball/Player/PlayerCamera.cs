using Quantum;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    

    [SerializeField]
    Vector3 offset;

    public Transform Target { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 400;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (!Target)
            return;
       

        transform.position = Target.position + offset;
        transform.rotation = Target.rotation;
    }
}
