using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    // �z��̐錾
    int[] map;

    void PrintArray()
    {
        // ������̐錾�Ə�����
        string debugText = "";
        for (int i = 0; i < map.Length; i++)
        {
            // �v�f��������o��
            debugText += map[i].ToString() + ",";
        }
        // ����������������o��
        Debug.Log(debugText);
    }

    int GetPlayerIndex()
    {
        for (int i = 0; i < map.Length; i++)
        {
            if (map[i] == 1)
            {
                return i;
            }
        }
        return -1;
    }

    bool MoveNumber(int number, int moveFrom, int moveTo)
    {
        // �ړ��悪�͈͊O�̎�
        if (moveTo < 0 || moveTo >= map.Length)
        {
            return false;
        }
        // �ړ���ɔ������鎞
        if (map[moveTo] == 2)
        {
            // �����锠�̐�����ɂ���
            //if (number == 2) { return false; }

            int velocity = moveTo - moveFrom;
            bool success = MoveNumber(2, moveTo, moveTo + velocity);

            if (!success)
            {
                return false;
            }
        }
        map[moveTo] = number;
        map[moveFrom] = 0;
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        // �z��̎��Ԃ̍쐬�Ə�����
        map = new int[] { 0, 2, 0, 2, 1, 2, 0, 2, 0 };
        PrintArray();
    }

    // Update is called once per frame
    void Update()
    {
        // �E�ړ�
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            int playerIndex = GetPlayerIndex();

            MoveNumber(1, playerIndex, playerIndex + 1);
            PrintArray();
        }

        // ���ړ�
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int playerIndex = GetPlayerIndex();

            MoveNumber(1, playerIndex, playerIndex - 1);
            PrintArray();
        }
    }
}
