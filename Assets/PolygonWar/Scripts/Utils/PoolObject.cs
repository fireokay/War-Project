using UnityEngine;
using UnityEngine.Pool;
public class PoolObject : MonoBehaviour
{
    public ObjectPool<GameObject> myPool;
    public void DestroyPoolObject()
    {
        myPool.Release(gameObject);
    }
}