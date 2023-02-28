using Cinemachine;
using UnityEngine;

public class CinemachineConfinerSetter : MonoBehaviour
{
    private Collider2D m_boudingShape;

    private void Awake()
    {
        m_boudingShape = GetComponent<Collider2D>();
    }

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
        Camera cam = Camera.main;

        if (!cam) { return; }

        CinemachineConfiner2D cinemachineConfiner = cam.gameObject.GetComponentInChildren<CinemachineConfiner2D>();
        cinemachineConfiner.m_BoundingShape2D = boundingShape;
    }
}
