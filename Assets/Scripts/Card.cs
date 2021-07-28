using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New card", menuName = "ScriptableObjects/Card")]
public class Card : ScriptableObject
{
    public string label;
    public Sprite image;
}
