using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;

public class FireballRune
    : SpellBase
{
    protected override void Start()
    {
        base.Start();
        
    }
    // Like shield, the basic "projectile fire" covers whats needed for this spell.
    // so this needs no unique behaviour from the base
}
