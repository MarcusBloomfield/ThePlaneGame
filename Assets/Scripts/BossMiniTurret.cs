using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMiniTurret : Turret
{
    private void Start()
    {
        base.SetTarget = PlayerJet.Instance.ActualJet;
    }
}
