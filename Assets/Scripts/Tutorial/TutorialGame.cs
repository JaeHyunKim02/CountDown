using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialGame : MonoBehaviour
{
    public static bool _isClear = false;

    private float nodeDistance = 1.95f;

    private int NodeSize = 5;

    private t_Swipe m_Swipe;
    private Vector3 desiredPosition;
    public GameObject testobj;

    public List<List<NodeArray>> nodeArr = new List<List<NodeArray>>();
    private List<List<GameObject>> nodeObjList = new List<List<GameObject>>();

    private bool wait = false;

    public Image success;
    bool isRestart = false;

    int temp;

    public static int StageNumber;

    public TURN_STATE turn;

    public enum TURN_STATE
    {
        STAY,
        SWIPE,
        MOVE,
        END,
    }
    public static TutorialGame instance = null;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;

        //InitNodeArr(5);
        _isClear = false;
        InitNodeArr(5);
        m_Swipe = GetComponent<t_Swipe>();

    }
    private void Start()
    {
        InitStage(myStatic.TutorialStage);
    }
    private void Update()
    {
        TurnState();
    }

    #region 게임 시스템 여기서는 순서에 신경을 쓰자

    //노트 배열 초기화
    public void InitNodeArr(int size)//일단 5가 들어감
    {
        for (int i = 0; i < size; i++)
        {
            nodeArr.Add(new List<NodeArray>());
            nodeObjList.Add(new List<GameObject>());
            for (int j = 0; j < size; j++)
            {
                NodeArray temp = new NodeArray();
                temp.Node_Kind = NODE_KIND.EMPTY;
                //temp.position = transform.position + new Vector3(-4.2f, 1.5f) + new Vector3(j * nodeDistance, -i * nodeDistance);
                //temp.position = transform.position + new Vector3(-4.2f, 4.2f) + new Vector3(j * nodeDistance, -i * nodeDistance);
                temp.position = transform.position + new Vector3(-3.9f, 3.2f) + new Vector3(j * nodeDistance, -i * nodeDistance);

                temp.isNodechange = false;
                temp.nodeNumber = 0;
                nodeArr[i].Add(temp);
                nodeObjList[i].Add(null);
            }
        }
    }

    void InitNode(int x, int y, int number)
    {
        nodeObjList[y][x] = Instantiate(testobj, transform);
        nodeObjList[y][x].transform.position = nodeArr[y][x].position;
        nodeObjList[y][x].GetComponent<NodeState>().TargetPosition = nodeArr[y][x].position;
        nodeObjList[y][x].GetComponent<NodeState>().NodeNumber = number;
        nodeArr[y][x].Node_Kind = NODE_KIND.NUMBER;
        nodeArr[y][x].nodeNumber = number;
    }
    void InitStopNode(int x, int y)
    {
        nodeObjList[y][x] = Instantiate(Resources.Load<GameObject>("Prefabs/CountDown/_Block"), transform);
        nodeObjList[y][x].transform.position = nodeArr[y][x].position;
        nodeArr[y][x].Node_Kind = NODE_KIND.STOP;
        nodeArr[y][x].nodeNumber = 0;
    }

    void TurnState()
    {
        switch (turn)
        {
            case TURN_STATE.STAY:
                if (m_Swipe.IsSwipeing)
                {
                    turn = TURN_STATE.SWIPE;
                    TurnState();
                }
                break;
            case TURN_STATE.SWIPE:
                SwipeNodes();
                turn = TURN_STATE.MOVE;
                
                break;
            case TURN_STATE.MOVE:
                int number = 0;
                for (int i = 0; i < NodeSize; i++)
                {
                    for (int j = 0; j < NodeSize; j++)
                    {
                        if (MoveNodes(i, j) == false)
                            ++number;//하나씩 늘려주다가

                        //Debug.Log(number);
                    }
                }
                if (number == NodeSize * NodeSize)//넘버가 25가 되는 순간 모든 노드가 움직인거임
                {
                    //Clear();
                    //Debug.Log("쨘");//그래서 여기서 쨘 하고 디버그가 찍힘
                    turn = TURN_STATE.STAY;//다시 턴 상태를 스테이로 바꿔줌

                    for (int i = 0; i < NodeSize; i++)
                    {
                        for (int j = 0; j < NodeSize; j++)
                        {

                            SetNodeMoveEnd(i, j);
                            nodeArr[i][j].isNodechange = false;
                        }
                    }
                    Clear();
                }

                break;

        }
    }
    private void SwipeNodes()
    {
        if (m_Swipe.SwipeLeft)
        {
            Debug.Log("!!!");
            for (int i = 0; i < 5; i++)
            {
                for (int j = 1; j < 5; j++)
                {
                    CompareNode(i, j, 0, -1);
                }
            }
        }
        else if (m_Swipe.SwipeRight)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 3; j > -1; j--)
                {
                    CompareNode(i, j, 0, +1);
                }
            }
        }
        else if (m_Swipe.SwipeUp)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 1; j < 5; j++)
                {
                    CompareNode(j, i, -1, 0);
                }
            }
        }
        else if (m_Swipe.SwipeDown)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 3; j > -1; j--)
                {
                    CompareNode(j, i, +1, 0);
                }
            }
        }
    }

    private void CompareNode(int y, int x, int dy, int dx)
    {
        if (x + dx < 0 || x + dx > NodeSize - 1) return;
        if (y + dy < 0 || y + dy > NodeSize - 1) return;// 닿는지 않닿았는지

        if (nodeArr[y][x].Node_Kind == NODE_KIND.NUMBER)
        {
            //CompareNode(0, 1, 0, -1);
            //다음 이동이 비었다면
            if (nodeArr[y + dy][x + dx].Node_Kind == NODE_KIND.EMPTY)
            {
                //Debug.Log("여기");

                //다음노드로 현재노드 정보이동
                //Debug.Log("아무일 없이 이동");
                nodeArr[y + dy][x + dx].Node_Kind = NODE_KIND.NUMBER;
                nodeArr[y + dy][x + dx].nodeNumber = nodeArr[y][x].nodeNumber;
                //현재노드 삭제
                nodeArr[y][x].nodeNumber = 0;
                nodeArr[y][x].Node_Kind = NODE_KIND.EMPTY;
                //실제 노드 오브젝트 타겟 포지션 변경
                nodeObjList[y][x].GetComponent<NodeState>().TargetPosition = nodeArr[y + dy][x + dx].position;
                nodeObjList[y + dy][x + dx] = nodeObjList[y][x];
                nodeObjList[y][x] = null;
                //재귀함수

                CompareNode(y + dy, x + dx, dy, dx);
            }
            else if (nodeArr[y + dy][x + dx].isNodechange == false && nodeArr[y + dy][x + dx].nodeNumber == nodeArr[y][x].nodeNumber)
            {
                //Debug.Log("같은 숫자끼리 합쳐짐");

                if (nodeArr[y + dy][x + dx].nodeNumber == 1 && nodeArr[y][x].nodeNumber == 1 && nodeArr[y + dy][x + dx].nodeNumber == nodeArr[y][x].nodeNumber)//숫자블록 2개가 합쳤는데 둘다 1일때---------------------------
                {
                    //Debug.Log("1끼리 만났기 때문에 서로 비워줌");
                    nodeArr[y + dy][x + dx].isNodechange = true;
                    //nodeArr[y + dy][x + dx].nodeNumber -= 1;
                    //현재 노드 정보를 다음 노드로 이동
                    nodeObjList[y][x].GetComponent<NodeState>().TargetPosition = nodeArr[y + dy][x + dx].position;
                    nodeObjList[y][x].GetComponent<NodeState>().ThisNode = nodeObjList[y + dy][x + dx];
                    nodeObjList[y + dy][x + dx].GetComponent<NodeState>().TransNode = nodeObjList[y][x];
                    nodeObjList[y][x] = null;
                    //nodeObjLi-st[y + dy][x + dx] = null;
                    //현재노드 삭제
                    nodeArr[y + dy][x + dx].nodeNumber = 0;
                    nodeArr[y + dy][x + dx].Node_Kind = NODE_KIND.EMPTY;
                    nodeArr[y][x].nodeNumber = 0;
                    nodeArr[y][x].Node_Kind = NODE_KIND.EMPTY;
                }//------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                else if (nodeArr[y + dy][x + dx].nodeNumber == nodeArr[y][x].nodeNumber)
                {
                    //Debug.Log("넘버");

                    Debug.Log("같은 숫자끼리 합쳐짐");
                    GetComponent<AudioSource>().Play();
                    //합체된 노드 접근방지
                    nodeArr[y + dy][x + dx].isNodechange = true;
                    nodeArr[y + dy][x + dx].nodeNumber -= 1;
                    //현재 노드 정보를 다음 노드로 이동
                    nodeObjList[y][x].GetComponent<NodeState>().TargetPosition = nodeArr[y + dy][x + dx].position;
                    nodeObjList[y][x].gameObject.transform.localScale = new Vector3(1.078f, 1.078f, 1.078f);
                    nodeObjList[y][x].GetComponent<NodeState>().ThisNode = nodeObjList[y + dy][x + dx];
                    nodeObjList[y + dy][x + dx].GetComponent<NodeState>().TransNode = nodeObjList[y][x];
                    nodeObjList[y][x] = null;
                    //현재노드 삭제
                    nodeArr[y][x].nodeNumber = 0;
                    nodeArr[y][x].Node_Kind = NODE_KIND.EMPTY;
                }
                return;
            }
        }
    }
    private bool MoveNodes(int y, int x)
    {

        if (nodeObjList[y][x] == null) return false;
        if (nodeObjList[y][x].GetComponent<NodeController>() == null) return false;
        nodeObjList[y][x].GetComponent<NodeController>().MoveNode();//원래는 MoveNode 테스트용 test
        if (nodeObjList[y][x].GetComponent<NodeState>().IsMoveEnd)
            return false;
        return true;
    }

    private void SetNodeMoveEnd(int y, int x)
    {
        if (nodeObjList[y][x] == null) return;
        if (nodeObjList[y][x].GetComponent<NodeState>() == null) return;
        nodeObjList[y][x].GetComponent<NodeState>().IsMoveEnd = false;
    }

    void Clear()
    {
        int count = 0;
        for (int i = 0; i < NodeSize; i++)
        {
            for (int j = 0; j < NodeSize; j++)
            {
                NodeArray temp = new NodeArray();
                //if (nodeArr[i][j].Node_Kind == NODE_KIND.EMPTY || nodeArr[i][j].Node_Kind == NODE_KIND.STOP)//현재 클리어조건이 비어있거나 스톱블록일때
                if (nodeArr[i][j].Node_Kind == NODE_KIND.EMPTY || nodeArr[i][j].Node_Kind == NODE_KIND.STOP)
                {

                    count++;

                }
            }
        }
        if (count == 25)
        {
            Debug.Log("클리어");
            //_isClear = true;
            //InitNodeArr(5);
            //turn = TURN_STATE.STAY;
            //wait = true;
            TutorialManager.isClear = true;
            myStatic.TutorialStage += 1;
        }
    }

    #endregion

    #region 스테이지 생성 인자(스테이지 넘버 0~2)
    public void InitStage(int _stageNumber) 
    {
        switch(_stageNumber)
        {
            case 0:
                Debug.Log("1번");
                InitNode(0, 2, 4);
                InitNode(1, 2, 4);
                InitNode(2, 2, 3);
                InitNode(3, 2, 2);
                InitNode(4, 2, 1);

                InitStopNode(0, 1);
                InitStopNode(1, 1);
                InitStopNode(2, 1);
                InitStopNode(3, 1);
                InitStopNode(4, 1);

                InitStopNode(0, 3);
                InitStopNode(1, 3);
                InitStopNode(2, 3);
                InitStopNode(3, 3);
                InitStopNode(4, 3);
                break;

            case 1:
                Debug.Log("2번");

                InitNode(2, 0, 4);
                InitNode(2, 1, 4);
                InitNode(2, 2, 3);
                InitNode(2, 3, 2);
                InitNode(2, 4, 1);

                InitStopNode(1, 0);
                InitStopNode(1, 1);
                InitStopNode(1, 2);
                InitStopNode(1, 3);
                InitStopNode(1, 4);

                InitStopNode(3, 0);
                InitStopNode(3, 1);
                InitStopNode(3, 2);
                InitStopNode(3, 3);
                InitStopNode(3, 4);

                break;

            case 2:
                Debug.Log("3번");

                InitNode(2, 0, 4);
                InitNode(2, 2, 4);
                InitNode(0, 0, 3);
                InitNode(0, 4, 2);
                InitNode(4, 4, 1);

                InitStopNode(3, 0);
                InitStopNode(3, 1);
                InitStopNode(3, 2);
                InitStopNode(3, 3);

                InitStopNode(1, 1);
                InitStopNode(1, 2);
                InitStopNode(1, 3);

                InitStopNode(2, 3);


                break;

			case 3:

				Debug.Log("4번");
				InitNode(0, 2, 2);
				InitStopNode(0, 3);
				InitNode(0, 4, 1);
				InitNode(1, 0, 2);
				InitStopNode(1, 1);
				InitStopNode(2, 0);
				InitStopNode(2, 1);
				InitStopNode(2, 2);
				InitStopNode(2, 3);
				InitStopNode(2, 4);
				InitStopNode(3, 3);
				InitNode(3, 4, 1);
				InitNode(4, 0, 1);


				break;


			case 4:
				InitStopNode(0, 3);
				InitNode(0, 4, 2);
				InitStopNode(1, 1);
				InitStopNode(2, 1);
				InitStopNode(2, 2);
				InitStopNode(2, 3);
				InitNode(3, 0, 2);
				InitStopNode(3, 1);
				InitNode(3, 2, 1);
				InitStopNode(3, 3);
				InitNode(4, 0, 2);
				InitStopNode(4, 1);


				break;
            default:

                break;
        }


    }
    #endregion


}