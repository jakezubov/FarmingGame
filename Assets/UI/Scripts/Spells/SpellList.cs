using UnityEngine;

public class SpellList : MonoBehaviour
{
    public GameObject _prefab;
    public Spell[] _currentSpells;

    void Start()
    {
        foreach (Spell spell in _currentSpells)
        {
            AddToSpellList(spell);
        }
    }

    public void AddToSpellList(Spell spell)
    {
        GameObject newSpell = Instantiate(_prefab, transform);
        newSpell.GetComponent<DraggableSpell>().InitialiseSpell(spell);

        SpellInfo info = newSpell.GetComponentInChildren<SpellInfo>();
        info._icon.sprite = spell.image;
        info._name.SetText(spell.name);
        info._description.SetText(spell.description);       
    }
}
