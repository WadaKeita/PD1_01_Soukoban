using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    // 配列の宣言
    int[] map;

    void PrintArray()
    {
        // 文字列の宣言と初期化
        string debugText = "";
        for (int i = 0; i < map.Length; i++)
        {
            // 要素数を一つずつ出力
            debugText += map[i].ToString() + ",";
        }
        // 結合した文字列を出力
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
        // 移動先が範囲外の時
        if (moveTo < 0 || moveTo >= map.Length)
        {
            return false;
        }
        // 移動先に箱がある時
        if (map[moveTo] == 2)
        {
            // 押せる箱の数を一つにする
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
        // 配列の実態の作成と初期化
        map = new int[] { 0, 2, 0, 2, 1, 2, 0, 2, 0 };
        PrintArray();
    }

    // Update is called once per frame
    void Update()
    {
        // 右移動
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            int playerIndex = GetPlayerIndex();

            MoveNumber(1, playerIndex, playerIndex + 1);
            PrintArray();
        }

        // 左移動
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int playerIndex = GetPlayerIndex();

            MoveNumber(1, playerIndex, playerIndex - 1);
            PrintArray();
        }
    }
}
