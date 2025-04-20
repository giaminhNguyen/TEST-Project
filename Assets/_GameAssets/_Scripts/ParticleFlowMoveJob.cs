using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class ParticleFlowMoveJob : MonoBehaviour
{
    public  Transform[]             pointCurves;
    private FlowMoveJob             _flowMoveJob;
    private ParticleSystem          _particleSystem;
    private NativeArray<PointCurve> _nativePointCurves;

    private void Start()
    {
        _nativePointCurves = new(pointCurves.Length, Allocator.Persistent);
        _particleSystem    = GetComponent<ParticleSystem>();
        _flowMoveJob       = new();
        for (var i = 0; i < pointCurves.Length; i++)
        {
            var pointCurve = pointCurves[i];
            _nativePointCurves[i] = new()
            {
                    pos = pointCurve.position,
            };
        }
        _flowMoveJob.pointCurves = _nativePointCurves;
    }

    private void OnParticleUpdateJobScheduled()
    {
        _flowMoveJob.ScheduleBatch(_particleSystem, 32);
    }
    
}

public struct FlowMoveJob : IJobParticleSystemParallelForBatch
{
    [ReadOnly]
    public NativeArray<PointCurve> pointCurves;
    public void Execute(ParticleSystemJobData particles, int startIndex, int count)
    {
        var positions = particles.positions;
        for (var i = startIndex; i < startIndex + count; i++)
        {
            var ii  = i % pointCurves.Length;
            Vector3 pos = pointCurves[ii].pos;
            positions[i] = Vector3.MoveTowards(positions[i], pos, 0.3f);
        }
    }
}

[Serializable]
public struct PointCurve
{
    public Vector3 pos;
}
