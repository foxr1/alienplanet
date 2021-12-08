using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.Profiling;
using UnityEngine;

// Adapted from code found at https://thegamedev.guru/unity-memory/profiler-module-metrics-programatically/
public class MemoryProfiler : MonoBehaviour
{
    ProfilerRecorder _totalUsedMemoryRecorder;

    void OnEnable()
    {
        _totalUsedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Total Used Memory");
    }

    void OnDisable()
    {
        _totalUsedMemoryRecorder.Dispose();
    }

    public string GetTotalMemoryUsed()
    {
        return $"<size=13>Total Used Memory:</size> {System.Math.Round(_totalUsedMemoryRecorder.LastValue / Mathf.Pow(1024, 3), 2)} GB";
    }
}