using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Component pool pattern<br/>
/// To store gameObjects, please use transform component
/// </summary>
[Serializable]
public class PoolPattern<T> where T : Component, IPoolable
{
    [Header("Default values")]
    [SerializeField] private int _defaultQuantity = 5;

    [Header("Prefab reference")]
    [SerializeField] private GameObject _elementPrefab;

    [Header("Desired Parents")]
    [SerializeField] private Transform _availableParent;
    [SerializeField] private Transform _currentlyUsedParent;

    private HashSet<T> m_availablePool = new HashSet<T>();
    private HashSet<T> m_currentlyUsedPool = new HashSet<T>();

    public HashSet<T> InitDefaultQuantity()
    {
        for (int i = 0; i < _defaultQuantity; i++)
        {
            T element = GameObject.Instantiate(_elementPrefab, _availableParent).GetComponent<T>();
            m_availablePool.Add(element);
            //_textElement.gameObject.hideFlags = HideFlags.HideInHierarchy;
        }

        return m_availablePool;
    }

    private void SetParent(T element, Transform newParent)
    {
        element.transform.SetParent(newParent);
    }

    public T GetFromAvailable(Vector3 startPosition, Quaternion startRotation)
    {
        T element;

        if (m_availablePool.Count == 0)
        {
            element = GameObject.Instantiate(_elementPrefab, _availableParent).GetComponent<T>();
            //_textElement.gameObject.hideFlags = HideFlags.HideInHierarchy;
        }
        else
        {
            element = m_availablePool.ElementAt(0);
            m_availablePool.Remove(element);
        }

        m_currentlyUsedPool.Add(element);

        SetParent(element, _currentlyUsedParent);
        element.transform.SetPositionAndRotation(startPosition, startRotation);
        element.gameObject.SetActive(true);
        element.OnGetFromAvailable();

        return element;
    }

    public T ReturnToAvailable(T key)
    {
        T element;
        m_currentlyUsedPool.TryGetValue(key, out element);
        m_currentlyUsedPool.Remove(key);

        // TODO: This is a debug for TypeZombie.cs' ReturnToPool that returns an Enemy to available ?twice? | Should find exactly why it happens
        if (element == null) { return null; } 

        m_availablePool.Add(element);

        element.gameObject.SetActive(false);

        SetParent(element, _availableParent);
        key.OnReturnToAvailable();
        return element;
    }

    /// <summary>
    /// Doesn't remove the key from the data structure
    /// </summary>
    public T FindFromCurrentlyUsed(T key)
    {
        T element;
        m_currentlyUsedPool.TryGetValue(key, out element);
        return element;
    }

    public T FindFromCurrentlyUsed(int index) => m_currentlyUsedPool.ElementAt(index);
}
