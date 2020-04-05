using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_NodeManager : MonoBehaviour
{
    enum BlockStyle
    {
        Empty,
        Number,
        Block,
        Plus,
    }

    enum TurnState
    {
        Stay,
        Moving,
        Fusion,
        End,//필요없으면 뺄 예정
    }

    TurnState GameTurn = TurnState.Stay;

    BlockStyle[,] BlockArr = new BlockStyle[5,5];
    int[,] NumberArr = new int[5, 5];

    private Swipe m_Swipe;

    void Start()
    {
        for(int x = 0; x < 5; x++)//배열 초기화
        {
            for (int y = 0; y < 5; y++)
            {
                BlockArr[y, x] = BlockStyle.Empty;
                NumberArr[y, x] = 0;
                Debug.Log("BlockArr" +y+","+x +" = "+BlockArr[y, x]);
            }
        }
    }

    void Update()
    {
        Turn();

        if ( m_Swipe.IsSwipeing)//스와이프하면
        {
            GameTurn = TurnState.Moving;
        }
    }

    void Swipe()//X블럭에도 문제가 없는가?, 벽과의 문제가 없는가? 합체를 하기위해서 무리가 없는가?
    {
        bool isMoved = false;

        if(m_Swipe.SwipeLeft)
        {
            for (int x = 0; x < 5; x++)//배열 초기화
            {
                for (int y = 0; y < 5; y++)
                {
                    if (x!=4 && (BlockArr[y,x] != BlockStyle.Empty|| BlockArr[y, x] != BlockStyle.Block))//x가 0이 아니고, 블럭이랑 빈공간이 아닐때
                    {
                        BlockArr[y, x] = BlockArr[y, x+1];//배열 이동
                        isMoved = true;//움직였어
                    }
                }
            }
        }
        else if (m_Swipe.SwipeRight)
        {
            for (int x = 4; x > -1; x--)//배열 초기화
            {
                for (int y = 0; y < 5; y++)
                {
                    if (x != 0 && (BlockArr[y, x] != BlockStyle.Empty || BlockArr[y, x] != BlockStyle.Block))//x가 0이 아니고, 블럭이랑 빈공간이 아닐때
                    {
                        BlockArr[y, x] = BlockArr[y, x - 1];//배열 이동
                        isMoved = true;//움직였어
                    }
                }
            }
        }
        else if (m_Swipe.SwipeDown)
        {
            for (int x = 0; x < 5; x++)//배열 초기화
            {
                for (int y = 4; y > -1; y--)
                {
                    if (y != 0 && (BlockArr[y, x] != BlockStyle.Empty || BlockArr[y, x] != BlockStyle.Block))//x가 0이 아니고, 블럭이랑 빈공간이 아닐때
                    {
                        BlockArr[y, x] = BlockArr[y-1, x];//배열 이동
                        isMoved = true;//움직였어
                    }
                }
            }
        }
        else if (m_Swipe.SwipeUp)
        {
            for (int x = 0; x < 5; x++)//배열 초기화
            {
                for (int y = 0; y < 5; y++)
                {
                    if (y != 4 && (BlockArr[y, x] != BlockStyle.Empty || BlockArr[y, x] != BlockStyle.Block))//x가 0이 아니고, 블럭이랑 빈공간이 아닐때
                    {
                        BlockArr[y, x] = BlockArr[y+1, x];//배열 이동
                        isMoved = true;//움직였어
                    }
                }
            }
        }

        if (isMoved)//이동했으면
        {
            isMoved = false;//초기화
            Swipe();//다시시작(재귀인데... 흐음..)
        }

    }

    void Turn()
    {
        switch (GameTurn)
        {
            case TurnState.Stay:

                break;
            case TurnState.Moving:
                Swipe();
                break;
            case TurnState.Fusion:

                break;
            case TurnState.End:

                break;
        }
    }
}
