using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TEST : MonoBehaviour
{
    private CancellationTokenSource _cancellationTokenSource;

    private void Awake()
    {
        _cancellationTokenSource = new CancellationTokenSource();
    }

    private void Start()
    {
        var token = _cancellationTokenSource.Token;
        Run(token, "go");
        Run(token, "run");
        Run(token, "play");
    }

    [ContextMenu("Cancel Token")]
    public void Cancel()
    {
        _cancellationTokenSource.Cancel();
    }

    public async UniTask Run(CancellationToken token, string name)
    {
        
        for (int i = 0; i < 1000; i++)
        {
            // token.ThrowIfCancellationRequested();
            Debug.Log($"{name} : {i}");
            await UniTask.Delay(1000, cancellationToken: token);
        }
    }

    private void OnDestroy()
    {
        _cancellationTokenSource?.Cancel();
    }
}
