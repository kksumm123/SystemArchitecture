using System;
using System.Threading;
using UnityEngine;

public abstract partial class RecycleObject : MonoBehaviour
{
    public int PoolSize => poolSize;

    protected CancellationTokenSource cancellationTokenSource;

    public bool useDynamicPool = true;
    [SerializeField] private int poolSize = 4;

    private Action<RecycleObject> _restore;

    public virtual void InitializeByFactory(Action<RecycleObject> restore)
    {
        this._restore = restore;
    }

    public virtual void OnDequeueByFactory() { }

    public virtual void Restore()
    {
        if (_restore == null) Debug.LogError($"{nameof(_restore)} is null");
        cancellationTokenSource?.SafeCancelTask();
        _restore?.Invoke(this);
    }

    private void OnDestroy()
    {
        cancellationTokenSource?.SafeCancelTask();
    }
}