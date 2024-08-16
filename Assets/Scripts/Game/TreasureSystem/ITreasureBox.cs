using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.Treasures
{
    public interface ITreasureBox
    {
        // �c�lID
        int ID { get; }

        // �c�l�e�q�����_����Weight
        int Capacity { get; }
        // ���}�c�l���һݮɶ�
        int CoolDown { get; }

        ITreasure Item { get; }

        bool IsEmpty { get; }

        // �[�J�_��
        void Add(ITreasure treasure);
    }
}
