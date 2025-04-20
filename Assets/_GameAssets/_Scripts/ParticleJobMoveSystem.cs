using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;
namespace UltimateHelper
{
    public class ParticleJobMoveSystem : ParticleJobMoveBase
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
        //Private
        private ParticleJobSys       _particleJobSys;
        private NativeArray<Vector3> _destinationNativeArr;
        private NativeReference<int> _particleCountFinish;
        private JobHandle            _jobHandle;
        #endregion
        
        private void Start()
        {
            _destinationNativeArr = new(_destinations.Length, Allocator.Persistent);
            _particleCountFinish  = new( Allocator.Persistent);
            for (var i = 0; i < _destinations.Length; i++)
            {
                _destinationNativeArr[i] = transform.InverseTransformPoint(_destinations[i].position);
            }
            _particleJobSys = new()
            {
                    destinations = _destinationNativeArr,
                    startInLifeTimePercent = _startInLifeTimePercent * 100,
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
                _particleJobSys.destinations       = _destinationNativeArr;
            }
            
            _particleJobSys.deltaTime           = Time.deltaTime;
            _particleJobSys.particleCountFinish = _particleCountFinish;

            _jobHandle = _particleJobSys.Schedule(_particleSystem);
            _jobHandle.Complete();
            if (_particleCountFinish.Value > 0)
            {
                onParticleCountFinishAction?.Invoke(_particleCountFinish.Value);
                onParticleCountFinish?.Invoke(_particleCountFinish.Value);
                Debug.Log(_particleCountFinish.Value);
            }
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
    public struct ParticleJobSys : IJobParticleSystem
    {
        [ReadOnly]
        public NativeArray<Vector3> destinations;

        [ReadOnly]
        public float deltaTime;

        [ReadOnly]
        public float startInLifeTimePercent;
        public NativeReference<int> particleCountFinish;
        

        public void Execute(ParticleSystemJobData jobData)
        {
            var length          = destinations.Length;
            var position        = jobData.positions;
            var lifeTimePercent = jobData.aliveTimePercent;
            var invertLifeTime  = jobData.inverseStartLifetimes;
            var countFinish     = 0;
            for (var i = 0; i < jobData.count; i++)
            {
                if (lifeTimePercent[i] < startInLifeTimePercent) continue;
                var destination = destinations[i % length];
                var distance    = Vector3.Distance(position[i], destination);
                var time        = (100 - lifeTimePercent[i]) * (1 / invertLifeTime[i]) / 100f;
                position[i] = Vector3.MoveTowards(position[i], destination, (distance / time) * deltaTime);

                if (position[i] == destination)
                {
                    countFinish++;
                }
            }
            particleCountFinish.Value = countFinish;
        }
    }
}