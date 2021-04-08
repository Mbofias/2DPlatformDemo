using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : ScriptableObject
{
    public string nameOfCharacter;
    public Sprite image;
    public Sprite healthBarImage;
    public Sprite backGroundImage;
    public string description;
    public float health, maxHealth;
    public float horizontalSpeed;
    public float verticalImpulseFactor;
}
