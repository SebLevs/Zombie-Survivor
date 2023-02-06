using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_StateController : StateController<Player_StateController>
{
    public Player_StateController(State<Player_StateController> m_initialState) : base(m_initialState)
    {
    }
}
