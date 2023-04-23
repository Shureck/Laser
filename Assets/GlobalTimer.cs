using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTimer : MonoBehaviour
{
    static public float time = 0;

    public void Update() {
        time += Time.deltaTime; 
    }
    
}
