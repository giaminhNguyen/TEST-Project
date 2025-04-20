using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

namespace _GameAssets._Scripts
{
    public class ParticleJobSystem : ParticleJob
    {
        [SerializeField]
        private Transform[] _destinations;

        [SerializeField]
        private float _speed;
        private ParticleJobSys       _particleJobSys;
        private NativeArray<Vector3> _destinationNativeArr;
        private JobHandle            _jobHandle;
        
        private void Start()
        {
            _destinationNativeArr = new NativeArray<Vector3>(_destinations.Length, Allocator.Persistent);

            for (var i = 0; i < _destinations.Length; i++)
            {
                _destinationNativeArr[i] = transform.InverseTransformPoint(_destinations[i].position);
            }
            _particleJobSys = new ParticleJobSys()
            {
                    destinations = _destinationNativeArr,
                    deltaTime    = Time.deltaTime,
                    speed = _speed,
            };
        }

        private void OnParticleUpdateJobScheduled()
        {
            if(_particleSystem.particleCount == 0) return;
            _jobHandle = _particleJobSys.Schedule(_particleSystem);
            _jobHandle.Complete();
            for (var i = 0; i < _destinations.Length; i++)
            {
                _destinationNativeArr[i] = transform.InverseTransformPoint(_destinations[i].position);
            }
            _particleJobSys.speed       =  _speed;
            _particleJobSys.deltaTime   =  Time.deltaTime;
        }
    }

    [BurstCompile]
    public struct ParticleJobSys : IJobParticleSystem
    {
        [ReadOnly]
        public NativeArray<Vector3> destinations;

        [ReadOnly]
        public float deltaTime;

        [ReadOnly]
        public float speed;
        
        public void Execute(ParticleSystemJobData jobData)
        {
            var length          = destinations.Length;
            var position        = jobData.positions;
            var lifeTimePercent = jobData.aliveTimePercent;
            var customData      = jobData.customData1;
            for (int i = 0; i < jobData.count; i++)
            {
                var cData = customData[i];

                if (cData.x == 0)
                {
                    cData.x = i % length;
                }
                var des   = destinations[(int)cData.x];
                position[i]        = Vector3.MoveTowards(position[i], des, speed * deltaTime);

                if (des == position[i])
                {
                    lifeTimePercent[i] = 100;
                }
                else
                {
                    lifeTimePercent[i] = 0;
                }
            }
        }
    }
}