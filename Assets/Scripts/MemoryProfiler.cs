using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.Profiling;
using UnityEngine;

// Adapted from code found at https://thegamedev.guru/unity-memory/profiler-module-metrics-programatically/
public class MemoryProfiler : MonoBehaviour
{
    string _statsText;
    ProfilerRecorder _totalUsedMemoryRecorder;
    ProfilerRecorder _totalReservedMemoryRecorder;
    ProfilerRecorder _gcReservedMemoryRecorder;
    ProfilerRecorder _textureMemoryRecorder;
    ProfilerRecorder _meshMemoryRecorder;

    void OnEnable()
    {
        _totalUsedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Total Used Memory");
        _totalReservedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Total Reserved Memory");
        _gcReservedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Reserved Memory");
        _textureMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Texture Memory");
        _meshMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Mesh Memory");
    }

    void OnDisable()
    {
        _totalUsedMemoryRecorder.Dispose();
        _totalReservedMemoryRecorder.Dispose();
        _gcReservedMemoryRecorder.Dispose();
        _textureMemoryRecorder.Dispose();
        _meshMemoryRecorder.Dispose();
    }

    void Update()
    {
        var sb = new StringBuilder(500);
        if (_totalUsedMemoryRecorder.Valid)
            sb.AppendLine($"Total Used Memory: {_totalUsedMemoryRecorder.LastValue / Mathf.Pow(1024, 3)} GB");
        if (_totalReservedMemoryRecorder.Valid)
            sb.AppendLine($"Total Reserved Memory: {_totalReservedMemoryRecorder.LastValue}");
        if (_gcReservedMemoryRecorder.Valid)
            sb.AppendLine($"GC Reserved Memory: {_gcReservedMemoryRecorder.LastValue}");
        if (_textureMemoryRecorder.Valid)
            sb.AppendLine($"Texture Used Memory: {_textureMemoryRecorder.LastValue}");
        if (_meshMemoryRecorder.Valid)
            sb.AppendLine($"Mesh Used Memory: {_meshMemoryRecorder.LastValue}");
        _statsText = sb.ToString();
    }

    public string GetTotalMemoryUsed()
    {
        return $"<size=13>Total Used Memory:</size> {System.Math.Round(_totalUsedMemoryRecorder.LastValue / Mathf.Pow(1024, 3), 2)} GB";
    }
}