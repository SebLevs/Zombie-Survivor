using UnityEngine;

public class DeactivateObjectOutsideEditor : MonoBehaviour
{
    private void Awake()
    {
#if !UNITY_EDITOR
GameObject.SetActive(false);
#endif
    }
}
