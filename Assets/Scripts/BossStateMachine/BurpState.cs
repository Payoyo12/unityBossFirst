using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurpState : State
{
    public BurpState(BossController boss) : base(boss) {}

    public override void Entry()
    {
        base.Entry();
        Debug.Log("Burp State Entered");
        //burp(eructar)
        Boss.starBurp();
        // siguiente estado
        Boss.ChangeStateKey(States.Rage);
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        
    }
}
