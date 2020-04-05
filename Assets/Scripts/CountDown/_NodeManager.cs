using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

enum map
{
    ONE = 1,
    TWO,
    THREE,
    FOUR,
    FIVE
}
public enum _NODE_KIND
{
    EMPTY,
    NUMBER,
    STOP,
}
public enum _TURN_STATE
{
    STAY,
    SWIPE,
    MOVE
}

public class _NodeArray
{
    private _NODE_KIND node_kind;
    public Vector2 position;
    public bool isNodeChange;
    public int nodeNumber;

    #region GetSet
    internal _NODE_KIND Node_Kind
    {
        get
        {
            return node_kind;
        }

        set
        {
            node_kind = value;
        }
    }
    #endregion

}

public class _NodeManager : MonoBehaviour
{
    private float nodeDistance = 2.1f;
    private int PlateSize = 5;

    private Swipe m_swipe;

    public List<List<_NodeArray>> _nodeArr = new List<List<_NodeArray>>();
    private List<List<GameObject>> _nodeObjectList = new List<List<GameObject>>();

    private bool wait;

    [SerializeField]
    private GameObject NumberNode;


    [SerializeField]
    private _TURN_STATE turn;

    [SerializeField]
    private Text swipeCount;
    [SerializeField]
    private Text StageLevel;

    public static _NodeManager instance = null;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);

        instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {

        m_swipe = GetComponent<Swipe>();//스와이프


        InitPlate(PlateSize);//판 초기화

        CreateStage();//판 생성

        turn = _TURN_STATE.STAY;

    }
    void Update()
    {
        swipeCount.text = myStatic.SwipeCount.ToString();
        StageLevel.text = "-LEVEL" + (myStatic.stageC - 1) + "-".ToString();

        //if(wait)
        //{
        //    wait = false;
        //    myStatic.stageC += 1;
        //}
        if (myStatic.siwpeC <= -1)
        {
            if (Clear())
            {
                myStatic.isClear = true;
            }
            else
                Restart();
        }
    }

    #region Restart() 다시 시작
    private void Restart()
    {
        if (PlayerPrefs.GetFloat("VibeProgress") == 1)
        {
            //Handheld.Vibrate();
        }

        GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Touch");
        Debug.Log("_NodeManager 131");
        SceneManager.LoadScene("CountDownScene");
        myStatic.stageC -= 1;
    }
    #endregion

    #region InitPlate(size) 게임판 초기화
    void InitPlate(int size)//게임판 초기화
    {
        for (int i = 0; i < size; i++)
        {
            _nodeArr.Add(new List<_NodeArray>());
            _nodeObjectList.Add(new List<GameObject>());
            for (int j = 0; j < size; j++)
            {
                _NodeArray temp = new _NodeArray();
                temp.Node_Kind = _NODE_KIND.EMPTY;
                temp.position = transform.position + new Vector3(-4.2f, 4.2f) + new Vector3(j * nodeDistance, -i * nodeDistance);
                temp.nodeNumber = 0;
                temp.isNodeChange = false;
                _nodeArr[i].Add(temp);
                _nodeObjectList[i].Add(null);
            }
        }
    }

    #endregion

    #region CR_number(x, y, number) 숫자 블럭 생성
    void CR_number(int x, int y, int number)//숫자블럭 생성
    {
        _nodeObjectList[x][y] = Instantiate(NumberNode, transform);
        _nodeObjectList[x][y].transform.position = _nodeArr[x][y].position;
        _nodeObjectList[x][y].GetComponent<NodeState>().TargetPosition = _nodeArr[x][y].position;
        //없어도 될거같음 타켓 포지션을 계속 바꿔줄 생각임

        _nodeObjectList[x][y].GetComponent<NodeState>().NodeNumber = number;
        _nodeArr[x][y].Node_Kind = _NODE_KIND.NUMBER;
        _nodeArr[x][y].nodeNumber = number;

    }
    #endregion

    #region CR_stop(x,y) X블럭 생성
    void CR_stop(int x, int y)
    {

        _nodeObjectList[x][y] = Instantiate(Resources.Load<GameObject>("Prefabs/CountDown/_Block"), transform);
        _nodeObjectList[x][y].transform.position = _nodeArr[x][y].position;
        _nodeArr[x][y].Node_Kind = _NODE_KIND.STOP;
        _nodeArr[x][y].nodeNumber = -1;

    }
    #endregion

    #region  bool형 Clear() 끝났는지 안끝났는지 검사
    private bool Clear()
    {
        int count = 0;
        for (int i = 0; i < PlateSize; i++)
        {
            for (int j = 0; j < PlateSize; j++)
            {
                _NodeArray temp = new _NodeArray();
                if (_nodeArr[i][j].Node_Kind == _NODE_KIND.EMPTY || _nodeArr[i][j].Node_Kind == _NODE_KIND.STOP)
                {
                    count++;

                }
            }
        }
        if (count == PlateSize * PlateSize)
        {
            return true;

        }
        return false;
    }
    #endregion
    void CreateStage()
    {
        switch (myStatic.stageC)
        {
            case 1:
                myStatic.siwpeC = 999;
                myStatic.MinimumConut = 999;

                CR_number(1, 1, 1);

                break;
        }
    }

    void TurnState()
    {
        switch (turn)
        {
            case _TURN_STATE.STAY:

                if (m_swipe.IsSwipeing)
                {
                    turn = _TURN_STATE.SWIPE;
                    TurnState();
                }
                break;

            case _TURN_STATE.SWIPE:
                SwipeNodes();

                turn = _TURN_STATE.MOVE;

                break;

            case _TURN_STATE.MOVE:

                break;
        }
    }

    private void SwipeNodes()
    {
        if (m_swipe.SwipeRight)
        {
            for(int x=0; x<=(PlateSize-1);x++)
            {
                for(int y=0; y<=(PlateSize-1); y++)
                {
                    CompareNode(x, y, 0, 1);
                }
            }
        }
        else if (m_swipe.SwipeLeft)
        {
            for(int x= (PlateSize-1); x<=0; x--)
            {
                for(int y= (PlateSize-1);y<=0;y--)
                {
                    CompareNode(x, y, -1, 0);
                }
            }
        }
        else if (m_swipe.SwipeUp)
        {

        }
        else if (m_swipe.SwipeDown)
        {

        }
    }

    void CompareNode(int x, int y, int dx, int dy)
    {
        if (_nodeArr[x][y].Node_Kind == _NODE_KIND.NUMBER)
        {
            if (_nodeArr[x + dx][y + dy].Node_Kind == _NODE_KIND.EMPTY)//위치 이동
            {
                //옆으로 정보를 넘겨주고
                _nodeArr[x + dx][y + dy].Node_Kind = _NODE_KIND.NUMBER;
                _nodeArr[x + dx][y + dy].nodeNumber = _nodeArr[x][y].nodeNumber;
                //전에 있던걸 지움
                _nodeArr[x][y].Node_Kind = _NODE_KIND.EMPTY;
                _nodeArr[x][y].nodeNumber = 0;

                //_nodeObjectList[x][y].GetComponent<NodeState>().TargetPosition = 
                _nodeObjectList[x + dx][y + dy] = _nodeObjectList[x][y];
                _nodeObjectList[x][y] = null;
                /*
                    nodeObjList[y][x].GetComponent<NodeState>().TargetPosition = nodeArr[y + dy][x + dx].position;
                    nodeObjList[y + dy][x + dx] = nodeObjList[y][x];
                    nodeObjList[y][x] = null;

                */

                CompareNode(y + dy, x + dx, dy, dx);
            }
            else if (!_nodeArr[x + dx][y + dy].isNodeChange && _nodeArr[x + dx][y + dy].Node_Kind == _NODE_KIND.NUMBER)
            {
                if (_nodeArr[x][y].nodeNumber == 1 && _nodeArr[x + dx][y + dy].nodeNumber == 1 && _nodeArr[x][y].nodeNumber == _nodeArr[x + dx][y + dy].nodeNumber)
                {
                    _nodeObjectList[x][y].GetComponent<NodeState>().TargetPosition = _nodeArr[x + dx][y + dy].position;
                    _nodeObjectList[x][y].GetComponent<NodeState>().ThisNode = _nodeObjectList[x + dx][y + dy];
                    _nodeObjectList[x + dx][y + dy].GetComponent<NodeState>().TransNode = _nodeObjectList[x][y];
                    _nodeObjectList[x][y] = null;

                    _nodeArr[x + dx][y + dy].isNodeChange = true;
                    _nodeArr[x + dx][y + dy].nodeNumber -= 1;

                    _nodeArr[x][y].nodeNumber = 0;
                    _nodeArr[x][y].Node_Kind = _NODE_KIND.EMPTY;

                }
                else if(_nodeArr[x][y].nodeNumber == _nodeArr[x + dx][y + dy].nodeNumber)
                {
                    _nodeArr[x + dx][y + dy].isNodeChange = true;
                    _nodeArr[x + dx][y + dy].nodeNumber -= 1;


                    _nodeObjectList[x][y].GetComponent<NodeState>().TargetPosition = _nodeArr[x + dx][y + dy].position;
                    _nodeObjectList[x][y].GetComponent<NodeState>().ThisNode = _nodeObjectList[x + dx][y + dy];
                    _nodeObjectList[x + dx][y + dy].GetComponent<NodeState>().TransNode = _nodeObjectList[x][y];
                    _nodeObjectList[y][x] = null;

                    _nodeArr[x][y].nodeNumber = 0;
                    _nodeArr[x][y].Node_Kind = _NODE_KIND.EMPTY;
                }
            }
        }
    }
}
