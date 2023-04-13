using UnityEngine;

public interface IAutomatedTestPlayer
{
    public bool ExecuteTest(PlayerAutomatedTestController testController);

#if UNITY_EDITOR
    public void DrawHandleGizmo(Transform drawFrom);
#endif
}
