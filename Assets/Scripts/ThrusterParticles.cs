using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterParticles : MonoBehaviour {

    private ParticleSystem tFwd;
    private ParticleSystem tAft;
    private ParticleSystem tPort;
    private ParticleSystem tStbd;

    void Start()
    {
        tFwd = transform.Find("FwdThrust").GetComponent<ParticleSystem>();
        tAft = transform.Find("AftThrust").GetComponent<ParticleSystem>();
        tPort = transform.Find("StrafeThrustPort").GetComponent<ParticleSystem>();
        tStbd = transform.Find("StrafeThrustStbd").GetComponent<ParticleSystem>();
    }

    public void ThrustAnim (ThrustDir dir)
    {
        if (dir == ThrustDir.fwd)
            tFwd.Play();
        else if (dir == ThrustDir.aft)
            tAft.Play();
        else if (dir == ThrustDir.port)
            tPort.Play();
        else if (dir == ThrustDir.stbd)
            tStbd.Play();
    }
}

public enum ThrustDir { fwd, aft, port, stbd };
