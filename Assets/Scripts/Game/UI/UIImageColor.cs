using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Graphic))]
public class UIImageColor : MonoBehaviour
{
    [SerializeField]
    private List<Color> _colors;

    private Graphic _graphic;


    private void Awake()
    {
        _graphic = GetComponent<Graphic>();
    }

    public void ChangeColor(int idx)
    {
        _graphic.color = _colors[idx];
    }
}
