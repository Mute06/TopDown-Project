using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/CharacterInfo")]
public class CharacterInfo : ScriptableObject
{
    public string Name;
    public Sprite sprite;
    public Color color;
}
