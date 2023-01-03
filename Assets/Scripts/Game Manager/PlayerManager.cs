using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager _instance;
    public Stats _health, _mana, _stamina;
    public Skill _combat, _magic, _farming, _mining, _woodcutting, _fishing, _crafting;
    public Reputation _queen, _wizard, _blacksmith, _carpenter, _knights, _merchants, _farmers;


    private void Awake()
    {
        _instance = this;
    }

    public void FreeExp()
    {
        int _requiredExpBase = 100;
        _combat.GainExp(_requiredExpBase * _combat.GetLevel());
        _magic.GainExp(_requiredExpBase * _magic.GetLevel());
        _farming.GainExp(_requiredExpBase * _farming.GetLevel());
        _mining.GainExp(_requiredExpBase * _mining.GetLevel());
        _woodcutting.GainExp(_requiredExpBase * _woodcutting.GetLevel());
        _fishing.GainExp(_requiredExpBase * _fishing.GetLevel());
        _crafting.GainExp(_requiredExpBase * _crafting.GetLevel());
    }

    public void FreeReputation()
    {
        int gainedRep = 1000;
        _queen.GainRep(gainedRep);
        _wizard.GainRep(gainedRep);
        _blacksmith.GainRep(gainedRep);
        _carpenter.GainRep(gainedRep);
        _knights.GainRep(gainedRep);
        _merchants.GainRep(gainedRep);
        _farmers.GainRep(gainedRep);
    }
}
