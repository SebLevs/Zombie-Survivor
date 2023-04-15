using UnityEngine;

public class DeactivateObjectOutsideEditor : MonoBehaviour
{
    private void Awake()
    {
#if !UNITY_EDITOR
    gameObject.SetActive(false);
#endif
    }
}
