using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Enemy")]
public class Enemy : ScriptableObject
{
    [Header("Stats")]
    public float walkSpeed;
    public float health;

    [Header("Attack")]
    public float cooldown;
    public float damage;
    public DamageType damageType;
    public DamageType weakness;
    public DamageType resistace;

    [Header("GFX")]
    public Sprite image;
    public RuntimeAnimatorController animationController;

    [Header("Other")]
    public Item[] droppedItems;
}



