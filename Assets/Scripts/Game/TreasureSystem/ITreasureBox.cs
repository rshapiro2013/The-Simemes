using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simemes.TreasureSystem
{
    public interface ITreasureBox
    {
        // �c�l�e�q�����_����Weight
        int Capacity { get; }

        // �[�J�_��
        void Add(ITreasure treasure);
    }
}
