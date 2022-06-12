using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeSkin : MonoBehaviour
{
    public AnimatorOverrideController Valentine;

    public void ValentineSkin()
    {
        GetComponent<Animator>().runtimeAnimatorController = Valentine as RuntimeAnimatorController;
    }
}
