using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public enum BlockState
{
    Empty,
    Stop,
    Number,
    Calculate,
}

public class Block
{
    public BlockState block;
    public int Number;
}



public class Init : MonoBehaviour
{
    const float CenterVec_X = 0;
    const float CenterVec_Y = 0;


    public GameObject Node;
    //Block[,] BlockPlace = new Block[5, 5];
    //List<Block> BlockPlace = new List<Block>();
    List<List<Block>> BlockPlace = new List<List<Block>>();


    public void InitBlock(int x, int y, BlockState block, int number)
    {

        BlockPlace[x][y].block = block;//블럭 형태 지정
        if(BlockPlace[x][y].block == BlockState.Number)//블럭이 넘버면
            BlockPlace[x][y].Number = number;//값 넣기
    }

    void Start()
    {
        //for (int y = 0; y < 5; y++)
        //{
        //    for (int x = 0; x < 5; x++)
        //    {
        //        BlockPlace[x][y].block = BlockState.Empty;
        //    }
        //}

        InitBlock(0, 0, BlockState.Number, 2);
        InitBlock(3, 2, BlockState.Number, 3);

        CreateObject();
    }

    void CreateObject()
    {
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                if (BlockPlace[x][y].block == BlockState.Number)
                    Instantiate(Node, new Vector3(x * 5,y * 5,0), new Quaternion(0,0,0,0));
            }
        }
    }
}
