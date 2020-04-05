using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EditNodeManager : MonoBehaviour
{


    //private float nodeDistance = 2.1f;
    private float nodeDistance = 1.95f;

    private int NodeSize = 5;


    private Swipe m_Swipe;
    private Vector3 desiredPosition;
    public GameObject testobj;
    public GameObject plusobj;
    public GameObject minusobj;
    public GameObject multipleobj;
    public GameObject Result;
    public List<List<NodeArray>> nodeArr = new List<List<NodeArray>>();
    private List<List<GameObject>> nodeObjList = new List<List<GameObject>>();
    private GameManager manager;
    private bool wait = false;
    public Text swipe;
    public Text StageLevelCount;
    public bool isGame;
    private string ArrayDataText;

    public Image success;
    public static bool isRestart = false;

    int temp;


    bool isMove;
    public static bool isClear;

    public TURN_STATE turn;

    public enum TURN_STATE
    {
        STAY,
        SWIPE,
        MOVE,
        END,
    }
    public static EditNodeManager instance = null;

    public void Awake()
    {

        if (instance != null)
            Destroy(this);

        instance = this;

        myStatic.SwipeCount = 0;
    }
    
    void StartSetting()
    {
        Check.isOneSheck = true;
        Check.Once = false;
        PauseButton.isPause = false;
        myStatic.SwipeCount = 0;
        isRestart = false;
        isClear = false;
        wait = true;
        m_Swipe = GetComponent<Swipe>();
        InitNodeArr(NodeSize);
        temp = 0;

        isGame = true;

        #region 스테이지 케이스문

        if (isGame)
        {
            switch (myStatic.stageC)
            {
                case 0:
                    SceneManager.LoadScene("TestScene");
                    break;
                case 1:

                    //Clear();
                    myStatic.siwpeC = EditManager.LeftText;//최대 횟수
                    myStatic.MinimumConut = EditManager.AtLeastText;//최소 횟수

                    for(int y = 0; y<5; y++)
                    {
                        for (int x = 0; x < 5; x++)
                        {
                            switch(EditManager.TestArray[y,x])
                            {
                                case 0:

                                    break;
                                case 1:
                                    InitNode(y, x, 1);
                                    break;
                                case 2:
                                    InitNode(y, x, 2);
                                    break;
                                case 3:
                                    InitNode(y, x, 3);
                                    break;
                                case 4:
                                    InitNode(y, x, 4);
                                    break;
                                case 255:
                                    InitStopNode(y, x);
                                    break;
                                default:

                                    break;
                            }
                        }
                    }
                    break;
                  default:
                      myStatic.stageC = 1;
                      SceneManager.LoadScene("TestScene");
                      break;
            }
        }
        #endregion

        turn = TURN_STATE.STAY;
        myStatic.stageC += 1;
        StartCoroutine(UIChange());
    }

    public void GetText()
    {
        ArrayDataText = null;
        ArrayDataText = "------------------------------------------------------------------\n";
        ArrayDataText = ArrayDataText + "myStatic.siwpeC = " + EditManager.LeftText + ";\n";
        ArrayDataText = ArrayDataText + "myStatic.MinimumConut = " + EditManager.AtLeastText + ";\n\n";

        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                switch (EditManager.TestArray[y, x])
                {
                    case 0:

                        break;
                    case 1:
                        ArrayDataText = ArrayDataText + "InitNode(" + x + "," + y + ", 1);\n" +
                            "";
                        break;
                    case 2:
                        ArrayDataText = ArrayDataText + "InitNode(" + x + "," + y + ", 2);\n" +
                            "";
                        break;
                    case 3:
                        ArrayDataText = ArrayDataText + "InitNode(" + x + "," + y + ", 3);\n" +
                            "";
                        break;
                    case 4:
                        ArrayDataText = ArrayDataText + "InitNode(" + x + "," + y + ", 4);\n" +
                            "";
                        break;
                    case 255:
                        ArrayDataText = ArrayDataText + "InitStopNode(" + x + "," + y + ");\n" +
                            "";
                        break;
                    default:

                        break;
                }
                
            }
        }
        //Debug.Log(ArrayDataText);
        Maketxt.WriteData(ArrayDataText);


    }

    void Start()
    {
        isRestart = false;
        StartSetting();//시작세팅
        GetText();//텍스트 가져오기
        //GameObject.Find("ArrayData").transform.GetChild(1).transform.GetComponent<Text>().text = ArrayDataText;//이걸 어케할까~여??? 겟챠 1로 해야되는데 글케 하면 텍스트가 안뜸
    }

    void Update()
    {

        //if (myStatic.siwpeC <= -1)//<=
        //{
        //    //swipe.text = "스와이프 부족!";

        //    Clear();//-------------------------------------------마지막 클리어때 11이 안움직이는 원인------------------------------------------------
        //    if (!isClear)
        //    {
        //        StartCoroutine(NoCountRestart());//1초 기다렸다가 다시 시작됨
        //        //Restart();
        //    }

        //}
        TurnState();
    }

    private IEnumerator Information()
    {
        yield return new WaitForSeconds(0.01f);

    }

    private IEnumerator NoCountRestart()
    {
        yield return new WaitForSeconds(0.0f);
        myStatic.stageC -= 1;

        SceneManager.LoadScene("New_EditingScene");
    }

    //노트 배열 초기화
    void InitNodeArr(int size)//일단 5가 들어감
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
                temp.position = transform.position + new Vector3(-3.9f, 3.9f) + new Vector3(j * nodeDistance, -i * nodeDistance);

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

    void InitPlusNode(int x, int y)
    {
        nodeObjList[y][x] = Instantiate(plusobj, transform);
        nodeObjList[y][x].transform.position = nodeArr[y][x].position;
        nodeObjList[y][x].GetComponent<NodeState>().TargetPosition = nodeArr[y][x].position;
        //nodeObjList[y][x].GetComponent<NodeState>().NodeNumber = 0;
        nodeArr[y][x].Node_Kind = NODE_KIND.PLUS;
        //nodeArr[y][x].nodeNumber = 1;
    }

    void InitMinusNode(int x, int y)
    {
        nodeObjList[y][x] = Instantiate(minusobj, transform);
        nodeObjList[y][x].transform.position = nodeArr[y][x].position;
        nodeObjList[y][x].GetComponent<NodeState>().TargetPosition = nodeArr[y][x].position;
        //nodeObjList[y][x].GetComponent<NodeState>().NodeNumber = 0;
        nodeArr[y][x].Node_Kind = NODE_KIND.MINUS;
        //nodeArr[y][x].nodeNumber = 1;
    }

    void InitMultipleNode(int x, int y)
    {
        nodeObjList[y][x] = Instantiate(multipleobj, transform);
        nodeObjList[y][x].transform.position = nodeArr[y][x].position;
        nodeObjList[y][x].GetComponent<NodeState>().TargetPosition = nodeArr[y][x].position;
        //nodeObjList[y][x].GetComponent<NodeState>().NodeNumber = 0;
        nodeArr[y][x].Node_Kind = NODE_KIND.MULTIPLE;
        //nodeArr[y][x].nodeNumber = 1;
    }


    private IEnumerator UIChange()
    {
        yield return new WaitForSeconds(0.0001f);
        if (isClear)
        {
            swipe.text = "Clear";
        }
        else if (myStatic.siwpeC <= 0)
        {
            swipe.text = "No Count!";
            if (!isClear)
            {
                Debug.Log("??");
                Debug.Log(isClear);
                StartCoroutine(NoCountRestart());
            }
            m_Swipe = null;
            //GameObject.Find("EventSystem").SetActive(false);
        }
        else
        {
            if (myStatic.stageC - 1 == 0)
            {
                swipe.text = myStatic.siwpeC.ToString();
                StageLevelCount.text = "Tutorial".ToString();//"-LEVEL" + (myStatic.stageC - 1) + "-".ToString();
            }
            else
            {
                swipe.text = myStatic.siwpeC.ToString();
                StageLevelCount.text = "-LEVEL" + (myStatic.stageC - 1) + "-".ToString();
            }
        }
    }



    void ChangeScene()
    {
        SceneManager.LoadScene("New_EditingScene");//안쓸거같음
    }


    void TurnState()
    {
        switch (turn)
        {
            case TURN_STATE.STAY:
                test();
                if (m_Swipe.IsSwipeing)
                {
                    turn = TURN_STATE.SWIPE;
                    TurnState();
                }
                break;
            case TURN_STATE.SWIPE:
                GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Swipe");
                SwipeNodes();
                turn = TURN_STATE.MOVE;
                //Debug.Log(myStatic.SwipeCount);
                break;
            case TURN_STATE.MOVE:
                myStatic.SwipeCount += 1;//------------------------------------------------------------------------

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
                    CheckeNodeState();
                    Clear();
                }
                //Debug.Log(_test);
                //if (_test == 25)
                //    myStatic.SwipeCount -= 1;
                //else _test = 0;
                StartCoroutine(UIChange());
                break;

        }
    }

    private void SwipeNodes()
    {
        if (m_Swipe.SwipeLeft)
        {
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
                for (int j = 4; j > -1; j--)
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
                for (int j = 4; j > -1; j--)
                {
                    CompareNode(j, i, +1, 0);
                }
            }
        }
    }


    int _test = 0;
    private void CompareNode(int y, int x, int dy, int dx)
    {
        if (x + dx < 0 || x + dx > NodeSize - 1)
        {
            _test += 1;

            return;

        }
        if (y + dy < 0 || y + dy > NodeSize - 1)
        {
            _test += 1;
            return;// 닿는지 않닿았는지
        }
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
                    //현재노드 삭제
                    nodeArr[y][x].nodeNumber = 0;
                    nodeArr[y][x].Node_Kind = NODE_KIND.EMPTY;

                    nodeObjList[y + dy][x + dx] = null;
                    nodeArr[y + dy][x + dx].nodeNumber = 0;
                    nodeArr[y + dy][x + dx].Node_Kind = NODE_KIND.EMPTY;
                }//------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                else if (nodeArr[y + dy][x + dx].nodeNumber == nodeArr[y][x].nodeNumber)
                {
                    //Debug.Log("넘버");
                    //Debug.Log("같은 숫자끼리 합쳐짐");
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
            else
            {

            }
            #region
            /*
            else if (nodeArr[y + dy][x + dx].Node_Kind == NODE_KIND.PLUS && nodeArr[y + dy][x + dx].isNodechange == false)//현재방법: 노드플러스를 노드넘버로 바꾸고 현재 노드를 지운다.
            {
                //Debug.Log("주체가 +가 아닌데 더하기");
                //합체된 노드 접근방지
                nodeArr[y + dy][x + dx].isNodechange = true;
                nodeArr[y + dy][x + dx].Node_Kind = NODE_KIND.NUMBER;
                nodeArr[y + dy][x + dx].nodeNumber = nodeArr[y][x].nodeNumber + 1;
                //현재 노드 정보를 다음 노드로 이동
                //nodeObjList[y + dy][x + dx].gameObject.tag = "Untagged";//태그를 지움
                nodeObjList[y][x].GetComponent<NodeState>().TargetPosition = nodeArr[y + dy][x + dx].position;
                nodeObjList[y + dy][x + dx].GetComponent<NodeState>().TransNode = nodeObjList[y][x];
                nodeObjList[y][x] = null;
                //현재노드 삭제
                nodeArr[y][x].nodeNumber = 0;
                nodeArr[y][x].Node_Kind = NODE_KIND.EMPTY;
                return;
            }
            else if (nodeArr[y + dy][x + dx].Node_Kind == NODE_KIND.MINUS && nodeArr[y + dy][x + dx].isNodechange == false)
            {
                if (nodeArr[y][x].nodeNumber <= 1)
                {
                    //Debug.Log("주체가 -가 아닌데 더하기");
                    //합체된 노드 접근방지
                    nodeArr[y + dy][x + dx].isNodechange = true;
                    nodeArr[y + dy][x + dx].Node_Kind = NODE_KIND.NUMBER;
                    nodeArr[y + dy][x + dx].nodeNumber = nodeArr[y][x].nodeNumber - 1;
                    //현재 노드 정보를 다음 노드로 이동
                    //nodeObjList[y + dy][x + dx].gameObject.tag = "Untagged";//태그를 지움
                    nodeObjList[y][x].GetComponent<NodeState>().TargetPosition = nodeArr[y + dy][x + dx].position;
                    nodeObjList[y + dy][x + dx].GetComponent<NodeState>().TransNode = nodeObjList[y][x];



                    nodeObjList[y][x] = null;
                    nodeObjList[y + dy][x + dx] = null;

                    Destroy(nodeObjList[y][x].gameObject);
                    Destroy(nodeObjList[y + dy][x + dx].gameObject);
                    nodeArr[y][x].nodeNumber = 0;
                    nodeArr[y][x].Node_Kind = NODE_KIND.EMPTY;
                    nodeArr[y + dy][x + dx].nodeNumber = 0;
                    nodeArr[y + dy][x + dx].Node_Kind = NODE_KIND.EMPTY;
                    Debug.Log("빼버리기(1일때, 숫자가 주체)");
                }
                else
                {
                    //  ("주체가 -가 아닌데 더하기");
                    //합체된 노드 접근방지
                    nodeArr[y + dy][x + dx].isNodechange = true;
                    nodeArr[y + dy][x + dx].Node_Kind = NODE_KIND.NUMBER;
                    nodeArr[y + dy][x + dx].nodeNumber = nodeArr[y][x].nodeNumber - 1;
                    //현재 노드 정보를 다음 노드로 이동
                    //nodeObjList[y + dy][x + dx].gameObject.tag = "Untagged";//태그를 지움
                    nodeObjList[y][x].GetComponent<NodeState>().c = nodeArr[y + dy][x + dx].position;
                    nodeObjList[y + dy][x + dx].GetComponent<NodeState>().TransNode = nodeObjList[y][x];
                    nodeObjList[y][x] = null;
                    nodeArr[y][x].nodeNumber = 0;
                    nodeArr[y][x].Node_Kind = NODE_KIND.EMPTY;
                    Debug.Log("빼버리기(1아님, 숫자가 주체)");
                }

                return;
            }
            else if (nodeArr[y + dy][x + dx].Node_Kind == NODE_KIND.MULTIPLE && nodeArr[y + dy][x + dx].isNodechange == false)
            {
                //Debug.Log("주체가 *가 아닌데 더하기");
                //합체된 노드 접근방지
                nodeArr[y + dy][x + dx].isNodechange = true;
                nodeArr[y + dy][x + dx].Node_Kind = NODE_KIND.NUMBER;
                nodeArr[y + dy][x + dx].nodeNumber = nodeArr[y][x].nodeNumber * 2;
                //현재 노드 정보를 다음 노드로 이동
                //nodeObjList[y + dy][x + dx].gameObject.tag = "Untagged";//태그를 지움
                nodeObjList[y][x].GetComponent<NodeState>().TargetPosition = nodeArr[y + dy][x + dx].position;
                nodeObjList[y + dy][x + dx].GetComponent<NodeState>().TransNode = nodeObjList[y][x];
                nodeObjList[y][x] = null;
                //현재노드 삭제
                nodeArr[y][x].nodeNumber = 0;
                nodeArr[y][x].Node_Kind = NODE_KIND.EMPTY;
                return;
            }
        }
        else if (nodeArr[y][x].Node_Kind == NODE_KIND.PLUS)//PLUS도 움직이게 하자
        {
            if (nodeArr[y + dy][x + dx].Node_Kind == NODE_KIND.EMPTY)
            {
                //다음노드로 현재노드 정보이동
                nodeArr[y + dy][x + dx].Node_Kind = NODE_KIND.PLUS;
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
            else if (nodeArr[y + dy][x + dx].Node_Kind == NODE_KIND.NUMBER && nodeArr[y + dy][x + dx].isNodechange == false)//노드의 변화는 없을때
            {
                //Debug.Log("주체가 +고 더하기");
                //합체된 노드 접근방지
                nodeArr[y + dy][x + dx].isNodechange = true;
                nodeArr[y + dy][x + dx].nodeNumber += 1;
                //현재 노드 정보를 다음 노드로 이동
                //nodeObjList[y + dy][x + dx].gameObject.tag = "Untagged";//태그를 지움
                nodeObjList[y][x].GetComponent<NodeState>().TargetPosition = nodeArr[y + dy][x + dx].position;
                nodeObjList[y + dy][x + dx].GetComponent<NodeState>().TransNode = nodeObjList[y][x];
                nodeObjList[y][x] = null;
                //현재노드 삭제
                nodeArr[y][x].nodeNumber = 0;
                nodeArr[y][x].Node_Kind = NODE_KIND.EMPTY;
                return;
            }
        }
        else if (nodeArr[y][x].Node_Kind == NODE_KIND.MINUS)//MINUS도 움직이게 하자
        {
            if (nodeArr[y + dy][x + dx].Node_Kind == NODE_KIND.EMPTY)
            {
                //다음노드로 현재노드 정보이동
                nodeArr[y + dy][x + dx].Node_Kind = NODE_KIND.MINUS;
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
            else if (nodeArr[y + dy][x + dx].Node_Kind == NODE_KIND.NUMBER && nodeArr[y + dy][x + dx].isNodechange == false)//노드의 변화는 없을때
            {
                if (nodeArr[y + dy][x + dx].nodeNumber <= 1)
                {
                    //Debug.Log("주체가 -고 더하기");
                    //합체된 노드 접근방지
                    nodeArr[y + dy][x + dx].isNodechange = true;
                    //  nodeArr[y + dy][x + dx].nodeNumber -= 1;
                    //현재 노드 정보를 다음 노드로 이동
                    //nodeObjList[y + dy][x + dx].gameObject.tag = "Untagged";//태그를 지움
                    nodeObjList[y][x].GetComponent<NodeState>().TargetPosition = nodeArr[y + dy][x + dx].position;
                    nodeObjList[y + dy][x + dx].GetComponent<NodeState>().TransNode = nodeObjList[y][x];
                    //nodeObjList[y][x] = null;
                    //nodeObjList[y][x] = null;
                    //현재노드 삭제
                    nodeArr[y][x].nodeNumber = 0;
                    nodeArr[y][x].Node_Kind = NODE_KIND.EMPTY;
                    //넘겨주는 노드도 같이 삭제
                    nodeArr[y + dy][x + dx].nodeNumber = 0;
                    nodeArr[y + dy][x + dx].Node_Kind = NODE_KIND.EMPTY;
                    Destroy(nodeObjList[y + dy][x + dx].gameObject); //이걸 키거나 저 아랫걸 키거나
                    Destroy(nodeObjList[y][x].gameObject);
                    Debug.Log("빼버리기(1일때, -가 주체)");
                }
                else
                {
                    //Debug.Log("주체가 -고 더하기");
                    //합체된 노드 접근방지
                    nodeArr[y + dy][x + dx].isNodechange = true;
                    nodeArr[y + dy][x + dx].nodeNumber -= 1;
                    //현재 노드 정보를 다음 노드로 이동
                    //nodeObjList[y + dy][x + dx].gameObject.tag = "Untagged";//태그를 지움
                    nodeObjList[y][x].GetComponent<NodeState>().TargetPosition = nodeArr[y + dy][x + dx].position;
                    nodeObjList[y + dy][x + dx].GetComponent<NodeState>().TransNode = nodeObjList[y][x];
                    nodeObjList[y][x] = null;
                    //현재노드 삭제
                    nodeArr[y][x].nodeNumber = 0;
                    nodeArr[y][x].Node_Kind = NODE_KIND.EMPTY;
                    Debug.Log("빼버리기(1이아님, -가 주체)");
                }

                //if(nodeArr[y][x].Node_Kind == NODE_KIND.NUMBER && nodeArr[y][x].nodeNumber <= 0)
                //{
                //    nodeArr[y][x].nodeNumber = 0;
                //    nodeArr[y][x].Node_Kind = NODE_KIND.EMPTY;
                //    Destroy(nodeObjList[y][x].gameObject);
                //}
                //if (nodeArr[y+dy][x+dx].Node_Kind == NODE_KIND.NUMBER && nodeArr[y][x].nodeNumber <= 0)
                //{
                //    nodeArr[y+dy][x+dx].nodeNumber = 0;
                //    nodeArr[y+dy][x+dx].Node_Kind = NODE_KIND.EMPTY;
                //    Destroy(nodeObjList[y+dy][x+dx].gameObject);
                //}
                return;
            }
        }
        else if (nodeArr[y][x].Node_Kind == NODE_KIND.MULTIPLE)//MULTIPLE도 움직이게 하자
        {
            if (nodeArr[y + dy][x + dx].Node_Kind == NODE_KIND.EMPTY)
            {
                //다음노드로 현재노드 정보이동
                nodeArr[y + dy][x + dx].Node_Kind = NODE_KIND.MULTIPLE;
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
            else if (nodeArr[y + dy][x + dx].Node_Kind == NODE_KIND.NUMBER && nodeArr[y + dy][x + dx].isNodechange == false)//노드의 변화는 없을때
            {
                //Debug.Log("주체가 *고 더하기");
                //합체된 노드 접근방지
                nodeArr[y + dy][x + dx].isNodechange = true;
                nodeArr[y + dy][x + dx].nodeNumber *= 2;
                //현재 노드 정보를 다음 노드로 이동
                //nodeObjList[y + dy][x + dx].gameObject.tag = "Untagged";//태그를 지움
                nodeObjList[y][x].GetComponent<NodeState>().TargetPosition = nodeArr[y + dy][x + dx].position;
                nodeObjList[y + dy][x + dx].GetComponent<NodeState>().TransNode = nodeObjList[y][x];
                nodeObjList[y][x] = null;
                //현재노드 삭제
                nodeArr[y][x].nodeNumber = 0;
                nodeArr[y][x].Node_Kind = NODE_KIND.EMPTY;
                return;
            }
        */
        }
        #endregion
    }
    private void test()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {

                //nodeObjList[i][j].GetComponent<NodeState>().TargetPosition = new Vector2(0, 0);
                //if (nodeObjList[i][j] == null)
                //nodeObjList[y][x].GetComponent<NodeState>().TargetPosition = nodeArr[y][x].position;
                //nodeArr[i][j].
                //Destroy(nodeObjList[i][j]);

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


    private void CheckeNodeState()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                //Debug.Log(nodeObjList[i][j]);
            }
        }
        //Debug.Log("줄끝");
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
            //Debug.Log("클리어");
            isClear = true;
            myStatic.isGameClear = true;

        }
    }
    public void FadeinFadeOut()
    {
        myStatic.isGameClear = true;
        isRestart = true;
        myStatic.stageC = 1;
        GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Touch");
        //Debug.Log(myStatic.stageC);

        //SceneManager.LoadScene("CountDownScene");//다시 게임 씬 불러옴



        for (int i = 0; i < transform.childCount; i++)
        {
            Debug.Log(transform.GetChild(i).name);
            Debug.Log("지우기");
            Destroy(transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < 5; i++)
        {

            for (int j = 0; j < 5; j++)
            {
                nodeArr[i][j].nodeNumber = 0;
                nodeArr[i][j].Node_Kind = NODE_KIND.EMPTY;
            }
        }

        Result.SetActive(false);
        StartSetting();
    }

    public void FadeinFadeOut_NextScene()
    {
        myStatic.isGameClear = true;
        isRestart = true;
        GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Touch");
        //Debug.Log(myStatic.stageC);



        //SceneManager.LoadScene("CountDownScene");//다시 게임 씬 불러옴



        for (int i = 0; i < transform.childCount; i++)
        {
            Debug.Log(transform.GetChild(i).name);
            Debug.Log("지우기");
            Destroy(transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < 5; i++)
        {

            for (int j = 0; j < 5; j++)
            {
                nodeArr[i][j].nodeNumber = 0;
                nodeArr[i][j].Node_Kind = NODE_KIND.EMPTY;
            }
        }

        Result.SetActive(false);
        StartSetting();
    }


    public void Restart()
    {
        if (PlayerPrefs.GetFloat("VibeProgress") == 1)
        {
            //Handheld.Vibrate();
            Debug.Log("Doit");
        }
        //myStatic.siwpeC = 0;
        GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Touch");
        //Debug.Log(myStatic.stageC);
        myStatic.stageC = 1;
        //timeSpan += Time.deltaTime;
        //  if(timeSpan>checkTime)

        //SceneManager.LoadScene("CountDownScene");//다시 게임 씬 불러옴
        Result.SetActive(false);
        StartSetting();

    }
}
