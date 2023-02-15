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
        float angle = MathAngleUtilities.GetSignedAngle2D(Entity_Player.Instance.transform, this.transform);
        int index = MathAngleUtilities.GetAngleAsIndex2D_Quad(angle);
        MathAngleUtilities.FlipLocalScale2D(transform, angle);
        Debug.Log($"Angle as index: {MathAngleUtilities.GetAngleAsIndex2D_Quad(angle)}");
        
        //Debug.Log($"Angle to player: {MathAngleUtilities.GetSignedAngle2D(Entity_Player.Instance.transform, this.transform)}");
    }
}
