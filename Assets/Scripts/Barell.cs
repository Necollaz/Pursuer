using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barell : AbstractHealth
{
    internal override void Die()
    {
        Destroy(gameObject);
    }
}
