using Cinemachine;
using UnityEngine;

public class CinemachineConfinerSetter : MonoBehaviour
{
    [SerializeField] private Collider2D m_boudingShape;
    private void OnEnable()
    {
        SetCinemachineConfinerBoundingShape(m_boudingShape);
    }

    private void OnDisable()
    {
        SetCinemachineConfinerBoundingShape(null);
    }

    private void SetCinemachineConfinerBoundingShape(Collider2D boundingShape)
    {
        CinemachineConfiner2D cinemachineConfiner = Camera.main.gameObject?.GetComponentInChildren<CinemachineConfiner2D>();
        cinemachineConfiner.m_BoundingShape2D = boundingShape;
    }
}
