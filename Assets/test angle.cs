using UnityEngine;

public class testangle : MonoBehaviour
{
    private void Update()
    {
        Debug.Log($"Angle to player: {TrigonometryUtilities.GetSignedAngle2D(Entity_Player.Instance.transform, this.transform)}");
    }
}
