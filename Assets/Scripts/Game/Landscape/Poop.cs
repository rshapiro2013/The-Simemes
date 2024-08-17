using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Poop
{
    public class Poop : MonoBehaviour, ILandscapeItem
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Sprite image;

        public GameObject Prefab => prefab;

        public Sprite Image => image;

        public long CreationTime { get; set; }
    }
}
