using UnityEngine;

namespace Aetherium.Combat.Data
{
    [CreateAssetMenu(fileName = "NewComboData", menuName = "Aetherium/Combat/Combo Sequence")]
    public class ComboDataSO : ScriptableObject
    {
        [SerializeField] private AttackDataSO[] comboAttacks;

        public int AttackCount => comboAttacks != null ? comboAttacks.Length : 0;

        public AttackDataSO GetAttack(int index)
        {
            if (comboAttacks == null || index < 0 || index >= comboAttacks.Length)
            {
                return null;
            }

            return comboAttacks[index];
        }
    }
}
