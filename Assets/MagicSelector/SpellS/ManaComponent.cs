using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaComponent : MonoBehaviour
{
    [Tooltip("Start and Max mana")][SerializeField] private int _mana;
    [SerializeField] private int manaRegenPerSecond;
    [HideInInspector] public bool isRegeneratingMana;
    private int maxMana;
    public int mana
    { 
        get
        {
            return _mana;
        }
    }

    private void Start()
    {
        isRegeneratingMana = false;
        maxMana = _mana;
    }

    public void SpendMana(int amt)
    {
        _mana -= amt;
    }

    public IEnumerator RegenMana()
    {
        isRegeneratingMana = true;
        float manaRegenRate = 1f / (float)manaRegenPerSecond;

        while (_mana < maxMana)
        {
            _mana = Mathf.Min(_mana+1, maxMana);
            yield return new WaitForSeconds(manaRegenRate);
        }
        isRegeneratingMana = false;
    }
}
