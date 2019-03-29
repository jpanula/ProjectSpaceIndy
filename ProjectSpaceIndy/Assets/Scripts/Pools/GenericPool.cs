using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenericPool<TPooledItem> : MonoBehaviour 
    where TPooledItem : MonoBehaviour
{
    public class PoolEntry
    {
        private readonly TPooledItem _item;

        public TPooledItem Item
        {
            get { return _item; }
        }
        
        public bool IsActive { get; set; }

        public PoolEntry(TPooledItem item, bool isActive)
        {
            _item = item;
            IsActive = isActive;
        }
    }

    public int Size
    {
        get { return _size; }
        set
        {
            _size = value;
            if (_pool.Count > _size)
            {
                foreach (PoolEntry entry in _pool)
                {
                    if (_pool.Count > _size)
                    {
                        _pool.Remove(entry);
                    }
                }
            }

            while (_pool.Count < _size)
            {
                AddObject();
            }
        }
    }

    public TPooledItem ObjectPrefab;
    private List<PoolEntry> _pool;
    [SerializeField]
    private int _size;

    private void Awake()
    {
        _pool = new List<PoolEntry>(Size);
        for (int i = 0; i < Size; i++)
        {
            AddObject();
        }
    }
    
    private TPooledItem AddObject(bool isActive = false)
    {
        TPooledItem item = Instantiate(ObjectPrefab);
        if (isActive)
        {
            Activate(item);
        }
        else
        {
            Deactivate(item);
        }
        _pool.Add(new PoolEntry(item, isActive));
        
        return item;
    }

    protected virtual void Activate(TPooledItem item)
    {
        SetState(item, true);
    }

    protected virtual void Deactivate(TPooledItem item)
    {
        SetState(item, false);
    }

    private void SetState(TPooledItem item, bool isActive)
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            PoolEntry current = _pool[i];
            if (current.Item == item)
            {
                current.IsActive = isActive;
            }
        }
    }

    public TPooledItem GetPooledItem()
    {
        TPooledItem result = null;
        for (int i = 0; i < _pool.Count; i++)
        {
            PoolEntry current = _pool[i];
            if (!current.IsActive)
            {
                result = current.Item;
                Activate(result);
                break;
            }
        }

        return result;
    }

    public bool ReturnPooledItem(TPooledItem item)
    {
        bool result = false;
        foreach (PoolEntry entry in _pool)
        {
            if (entry.Item == item)
            {
                Deactivate(item);
                result = true;
                break;
            }
        }

        return result;
    }
}
