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

        long StartTime { get; }

        float RemainTime { get; set; }

        List<ITreasure> Items { get; }

        // �_�c���]
        ITreasureBuff Buff { get; }

        bool IsEmpty { get; }
        bool IsFull { get; }
        bool IsSealed { get; }
        bool HasBuff { get; }

        TreasureBoxState State { get; }

        // �[�J�_��
        void Add(ITreasure treasure, long addTime);

        // �ˬd�O�_��[�J�_��
        bool TryAdd(ITreasure treasure);

        //���W�_�c
        void Seal();

        void AddBuff(ITreasureBuff buff);
        void RemoveBuff(ITreasureBuff buff);
        // ��s
        bool Update();

        // ��o�_��
        void Obtain();

        Sprite GetSprite();
    }
}
