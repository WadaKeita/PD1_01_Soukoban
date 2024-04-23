using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject boxPrefab;
    int[,] map;          // ���x���f�U�C���p�̔z��
    GameObject[,] field; // �Q�[���Ǘ��p�̔z��

    void PrintArray()
    {
        //// ������̐錾�Ə�����
        //string debugText = "";
        //for (int y = 0; y < map.GetLength(0); y++)
        //{
        //    for (int x = 0; x < map.GetLength(1); x++)
        //    {
        //        debugText += map[y, x].ToString() + ",";
        //    }
        //    debugText += "\n";
        //}
        //// ����������������o��
        //Debug.Log(debugText);
    }

    Vector2Int GetPlayerIndex()
    {
        for (int y = 0; y < field.GetLength(0); y++)
        {
            for (int x = 0; x < field.GetLength(1); x++)
            {
                if (field[y, x] == null) { continue; }
                if (field[y, x].tag == "Player")
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return new Vector2Int(-1, -1);
    }

    bool MoveNumber(string tag, Vector2Int moveFrom, Vector2Int moveTo)
    {
        // �c�������̔z��O�Q�Ƃ����Ă��Ȃ���
        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0)) { return false; }
        if (moveTo.x < 0 || moveTo.x >= field.GetLength(1)) { return false; }

        // Box�^�O�������Ă�����ċA����
        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            // �����锠�̐�����ɂ���
            //if (field[moveFrom.y,moveFrom.x].tag == "Box") { return false; }

            Vector2Int velocity = moveTo - moveFrom;
            bool success = MoveNumber(tag, moveTo, moveTo + velocity);

            if (!success) { return false; }
        }
        // GameObject�̍��W(position)���ړ������Ă���C���f�b�N�X�̓���ւ�
        field[moveFrom.y, moveFrom.x].transform.position =
            new Vector3(moveTo.x - field.GetLength(1) / 2, -moveTo.y + field.GetLength(0) / 2, 0);
        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];
        field[moveFrom.y, moveFrom.x] = null;
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        //GameObject instance = Instantiate(
        //    playerPrefab,
        //    new Vector3(0, 0, 0),
        //    Quaternion.identity
        //);

        // �z��̎��Ԃ̍쐬�Ə�����
        map = new int[,] {
            { 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0},
            { 0, 2, 2, 0, 0, 0, 0},
            { 0, 2, 2, 1, 0, 2, 0},
            { 0, 0, 0, 0, 0, 2, 0},
            { 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0}
        };
        field = new GameObject
        [
            map.GetLength(0),
            map.GetLength(1)
        ];

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 1)
                {
                    //GameObject instance = Instantiate(playerPrefab, new Vector3(x, map.GetLength(0) - y, 0), Quaternion.identity);
                    field[y, x] = Instantiate(
                        playerPrefab,
                        new Vector3(x - map.GetLength(1) / 2, -y + map.GetLength(0) / 2, 0),
                        Quaternion.identity
                    );
                }
                if (map[y, x] == 2)
                {
                    field[y, x] = Instantiate(
                        boxPrefab,
                        new Vector3(x - map.GetLength(1) / 2, -y + map.GetLength(0) / 2, 0),
                        Quaternion.identity
                    );
                }
            }
        }


        //PrintArray();
    }

    // Update is called once per frame
    void Update()
    {
        // �E�ړ�
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();

            MoveNumber("Player", playerIndex, playerIndex + new Vector2Int(1, 0));
            //PrintArray();
        }

        // ���ړ�
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();

            MoveNumber("Player", playerIndex, playerIndex + new Vector2Int(-1, 0));
            //PrintArray();
        }

        // ��ړ�
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();

            MoveNumber("Player", playerIndex, playerIndex + new Vector2Int(0, -1));
            //PrintArray();
        }

        // ���ړ�
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();

            MoveNumber("Player", playerIndex, playerIndex + new Vector2Int(0, 1));
            //PrintArray();
        }
    }
}
