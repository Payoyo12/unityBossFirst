using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageState : State
{
    public RageState(BossController Boss) : base(Boss) {}

    public override void Entry()
    {
        base.Entry();
        Boss.StartCoroutine(Burp());
        Debug.Log("Rage State Entered");
    }

    IEnumerator Burp()
    {
        yield return new WaitForSeconds(5f);
        Boss.ChangeStateKey(States.Burp);
    }


}
