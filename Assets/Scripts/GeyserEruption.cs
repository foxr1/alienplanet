using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Adapted from code found at http://www.mirzabeig.com/tutorials/rewinding-particle-systems/
public class GeyserEruption : MonoBehaviour
{
    ParticleSystem[] particleSystems;

    float[] simulationTimes;

    public float startTime = 0.0f, stopTime = 5.0f;
    public float simulationSpeedScale = 0.5f;

    void Initialize()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>(false);
        simulationTimes = new float[particleSystems.Length];
    }

    void OnEnable()
    {
        if (particleSystems == null)
        {
            Initialize();
        }

        for (int i = 0; i < simulationTimes.Length; i++) { simulationTimes[i] = 0.0f; }
        particleSystems[0].Simulate(startTime, true, false, true);
    }

    void Update()
    {
        particleSystems[0].Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear); 
        
        for (int i = particleSystems.Length - 1; i >= 0; i--)
        {
            bool useAutoRandomSeed = particleSystems[i].useAutoRandomSeed;
            particleSystems[i].useAutoRandomSeed = false;

            particleSystems[i].Play(false);

            float deltaTime = particleSystems[i].main.useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            simulationTimes[i] -= (deltaTime * particleSystems[i].main.simulationSpeed) * simulationSpeedScale;

            float currentSimulationTime = startTime + simulationTimes[i];
            particleSystems[i].Simulate(currentSimulationTime, false, false, true);

            // Once time is reached, reverse particle system
            if (currentSimulationTime < 0.0f || currentSimulationTime > stopTime)
            {
                simulationSpeedScale *= -1;
            }
        }
    }
}
