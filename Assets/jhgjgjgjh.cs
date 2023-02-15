using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jhgjgjgjh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float angle = TrigonometryUtilities.GetSignedAngle2D(Entity_Player.Instance.transform, this.transform);
        int index = TrigonometryUtilities.GetAngleAsIndex2D_Quad(angle);
        TrigonometryUtilities.FlipSpriteHorizontally2D(transform, angle);
        Debug.Log($"Angle as index: {TrigonometryUtilities.GetAngleAsIndex2D_Quad(angle)}");
        
        //Debug.Log($"Angle to player: {TrigonometryUtilities.GetSignedAngle2D(Entity_Player.Instance.transform, this.transform)}");
    }
}
