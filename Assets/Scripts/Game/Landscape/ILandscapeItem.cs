using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILandscapeItem
{
    GameObject Prefab { get; }
    Sprite Image { get; }
    long CreationTime { get; }
}
