using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Decision : ScriptableObject {

    // Use this for initialization

    public abstract bool Decide(StateController controller);


}
