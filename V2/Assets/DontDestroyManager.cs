using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyManager : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
