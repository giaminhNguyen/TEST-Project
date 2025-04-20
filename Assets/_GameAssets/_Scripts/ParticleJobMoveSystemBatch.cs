using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

namespace UltimateHelper
{
    public class ParticleJobMoveSystemBatch : ParticleJobMoveBase
    {
        #region Properties
        //Public
        //Serialized Field
        [SerializeField]
        private Transform[] _destinations;
        [SerializeField]
        private bool _destinationDynamic;
        [SerializeField]
        private float _startInLifeTimePercent;
        [SerializeField]
        private int _batch = 64;
        
        //Private
        private ParticleJobSysBatch  _particleJobSysBatch;
        private NativeArray<Vector3> _destinationNativeArr;
        private JobHandle            _jobHandle;
        private NativeQueue<int>     _particleCountFinish;
        #endregion
        
        private void Start()
        {
            _destinationNativeArr = new(_destinations.Length, Allocator.Persistent);
            for (var i = 0; i < _destinations.Length; i++)
            {
                _destinationNativeArr[i] = transform.InverseTransformPoint(_destinations[i].position);
            }
            _particleCountFinish      = new( Allocator.Persistent);
            _particleJobSysBatch = new()
            {
                    destinations = _destinationNativeArr,
                    startInLifeTimePercent = _startInLifeTimePercent * 100,
                    particleCountFinish = _particleCountFinish.AsParallelWriter(),
            };
        }

        private void OnParticleUpdateJobScheduled()
        {
            if(_particleSystem.particleCount == 0) return;
            if (_destinationDynamic)
            {
                for (var i = 0; i < _destinations.Length; i++)
                {
                    _destinationNativeArr[i] = transform.InverseTransformPoint(_destinations[i].position);
                }
                _particleJobSysBatch.destinations       = _destinationNativeArr;
            }
            
            _particleJobSysBatch.deltaTime           = Time.deltaTime;
            _particleJobSysBatch.particleCountFinish = _particleCountFinish.AsParallelWriter();
            

            _jobHandle                = _particleJobSysBatch.ScheduleBatch(_particleSystem,_batch);
            _jobHandle.Complete();
            var value = 0;

            while (_particleCountFinish.TryDequeue(out var queue))
            {
                value += queue;
            }

            if (value > 0)
            {
                Debug.Log(value);
                onParticleCountFinishAction?.Invoke(value);
                onParticleCountFinish?.Invoke(value);
            }
            _particleCountFinish.Clear();
        }

        private void OnDisable()
        {
            if (_destinationNativeArr.IsCreated)
            {
                _destinationNativeArr.Dispose();
            }
            if (_particleCountFinish.IsCreated)
            {
                _particleCountFinish.Dispose();
            }
        }
    }

    [BurstCompile]
    public struct ParticleJobSysBatch : IJobParticleSystemParallelForBatch
    {
        [ReadOnly]
        public NativeArray<Vector3> destinations;

        [ReadOnly]
        public float deltaTime;

        [ReadOnly]
        public float startInLifeTimePercent;
        public NativeQueue<int>.ParallelWriter particleCountFinish;
        

        public void Execute(ParticleSystemJobData jobData, int startIndex, int count)
        {
            var length          = destinations.Length;
            var position        = jobData.positions;
            var lifeTimePercent = jobData.aliveTimePercent;
            var invertLifeTime  = jobData.inverseStartLifetimes;
            var end             = startIndex + count;
            var countFinish     = 0;
            for (var i = startIndex; i < end; i++)
            {
                if (lifeTimePercent[i] < startInLifeTimePercent) continue;
                var destination      = destinations[i % length];
                var distance = Vector3.Distance(position[i], destination);
                var time     = (100 - lifeTimePercent[i]) * (1 / invertLifeTime[i]) / 100f;
                position[i] = Vector3.MoveTowards(position[i], destination, (distance / time) * deltaTime);

                if (position[i] == destination)
                {
                    countFinish++;
                }
            }

            particleCountFinish.Enqueue(countFinish);
        }
    }
}