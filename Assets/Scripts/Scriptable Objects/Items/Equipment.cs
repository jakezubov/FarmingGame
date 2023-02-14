using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Item/Equipment")]
public class Equipment : Item
{
    public EquipmentType equipmentType;
}

public enum EquipmentType
{
    NA,
    Ring,
    Belt,
    Necklace,
    Arrows,
    ArcaneFocus
}
