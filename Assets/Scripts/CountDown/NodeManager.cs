using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

enum Map
{
	ONE = 1,
	TWO,
	THREE,
	FOUR,
	FIVE
}

public enum NODE_KIND
{
	EMPTY,
	NUMBER,
	STOP,
	PLUS,
	MINUS,
	MULTIPLE,
}

public class NodeArray
{
	public NodeArray() { }
	public NodeArray(NodeArray a)
	{
		node_Kind = a.node_Kind;
		position = a.position;
		isNodechange = a.isNodechange;
		nodeNumber = a.nodeNumber;
	}
	private NODE_KIND node_Kind;
	public Vector2 position;
	public bool isNodechange;
	public int nodeNumber;

	#region GetSet
	internal NODE_KIND Node_Kind
	{
		get
		{
			return node_Kind;
		}

		set
		{
			node_Kind = value;
		}
	}
	#endregion
}

public class NodeManager : MonoBehaviour
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

	public List<List<NodeArray>> nodeCheckArr;// = new List<List<NodeArray>>();


	private List<List<GameObject>> nodeObjList = new List<List<GameObject>>();
	private GameManager manager;
	private bool wait = false;
	public Text swipe;
	public Text StageLevelCount;
	public bool isGame;

	public Image success;
	public static bool isRestart = false;

	bool IsMove;


	bool isMove;
	public static bool isClear;

	public TURN_STATE turn;


	bool _IsOne;

	public enum TURN_STATE
	{
		STAY,
		SWIPE,
		MOVE,
		END,
	}
	public static NodeManager instance = null;

	public void Awake()
	{

		if (instance != null)
			Destroy(this);

		instance = this;

		myStatic.SwipeCount = 0;
		
	}

	void StartSetting()
	{
		_IsOne = true;
		IsMove = true;
		nodeCheckArr = null;
		isRestart = false;
		Check.isOneSheck = true;
		Check.Once = false;
		PauseButton.isPause = false;
		myStatic.SwipeCount = 0;
		isClear = false;
		wait = true;
		m_Swipe = GetComponent<Swipe>();
		InitNodeArr(NodeSize);

		isGame = true;

		myStatic.IsAddShowOne = false;

		#region 스테이지 케이스문

		if (isGame)
        {
            switch (myStatic.stageC)
            {
                case 0:
                    SceneManager.LoadScene("TitleScene_New");
                    break;
                case 1:

                    //Clear();
                    myStatic.siwpeC = 10;//최대 횟수
                    myStatic.MinimumConut = 2;//최소 횟수

                    InitStopNode(0, 1);
                    InitNode(1, 1, 1);
                    InitNode(4, 3, 2);
                    InitNode(4, 2, 2);

                    //InitNode(1, 1, 1);
                    //InitNode(4, 3, 2);  
                    //InitNode(4, 2, 2);
                    //InitNode(3, 2, 1);
                    //InitNode(2, 2, 1);

                    break;
                case 2:
                    myStatic.siwpeC = 10;
                    myStatic.MinimumConut = 4;
                    InitNode(0, 0, 2);
                    InitNode(1, 1, 2);
                    InitNode(4, 3, 1);
                    InitStopNode(1, 0);
                    InitStopNode(2, 1);
                    InitStopNode(4, 1);
                    InitStopNode(2, 3);
                    InitStopNode(2, 4);
                    break;
                case 3:
                    myStatic.siwpeC = 7;
                    myStatic.MinimumConut = 4;
                    InitNode(4, 1, 1);
                    InitNode(4, 4, 1);
                    InitStopNode(1, 0);
                    InitStopNode(0, 2);
                    InitStopNode(1, 2);
                    InitStopNode(3, 2);
                    InitStopNode(4, 2);
                    InitStopNode(3, 4);
                    break;
                case 4:


                    myStatic.siwpeC = 9;
                    myStatic.MinimumConut = 6;
                    InitNode(0, 0, 1);
                    InitNode(1, 1, 2);
                    InitNode(0, 2, 2);
                    InitStopNode(0, 1);
                    InitStopNode(1, 2);
                    InitStopNode(2, 4);
                    break;
                case 5:

                    myStatic.siwpeC = 12;
                    myStatic.MinimumConut = 7;
                    InitNode(0, 0, 2);
                    InitNode(3, 0, 2);
                    InitNode(4, 0, 1);
                    InitStopNode(0, 1);
                    InitStopNode(1, 3);
                    InitStopNode(2, 0);
                    InitStopNode(2, 1);
                    InitStopNode(2, 2);
                    InitStopNode(2, 3);
                    InitStopNode(3, 3);
                    InitStopNode(4, 1);
                    break;
                case 6:


                    myStatic.siwpeC = 10;
                    myStatic.MinimumConut = 4;
                    InitNode(0, 0, 4);
                    InitNode(1, 2, 3);
                    InitNode(3, 0, 1);
                    InitNode(4, 0, 2);
                    InitNode(0, 4, 4);
                    InitStopNode(1, 0);
                    InitStopNode(1, 1);
                    InitStopNode(2, 2);
                    InitStopNode(3, 3);

                    break;

                case 7:


                    myStatic.siwpeC = 8;
                    myStatic.MinimumConut = 5;
                    InitNode(0, 0, 4);
                    InitNode(0, 4, 4);
                    InitNode(2, 2, 1);
                    InitNode(2, 4, 2);
                    InitNode(4, 4, 3);
                    InitStopNode(1, 1);
                    InitStopNode(2, 1);
                    InitStopNode(3, 1);
                    InitStopNode(1, 2);
                    InitStopNode(3, 2);
                    InitStopNode(1, 3);
                    InitStopNode(3, 3);
                    InitStopNode(1, 4);
                    break;
                case 8:


                    myStatic.siwpeC = 7;
                    myStatic.MinimumConut = 4;
                    InitNode(0, 1, 3);
                    InitNode(2, 2, 3);
                    InitNode(0, 3, 1);
                    InitNode(1, 4, 2);
                    InitStopNode(1, 1);
                    InitStopNode(3, 1);
                    InitStopNode(1, 2);
                    InitStopNode(3, 2);
                    InitStopNode(1, 3);
                    InitStopNode(2, 3);
                    InitStopNode(3, 3);
                    InitStopNode(0, 4);
                    break;
                case 9:
                    myStatic.siwpeC = 18;
                    myStatic.MinimumConut = 5;
                    InitNode(0, 0, 3);
                    InitNode(4, 0, 1);
                    InitNode(0, 3, 2);
                    InitNode(4, 4, 3);
                    break;
                case 10://피피티7번
                    myStatic.siwpeC = 18;
                    myStatic.MinimumConut = 8;
                    InitNode(0, 0, 3);
                    InitNode(4, 0, 3);
                    InitNode(1, 2, 2);
                    InitNode(3, 2, 2);
                    InitNode(4, 4, 2);
                    InitStopNode(0, 1);
                    InitStopNode(0, 2);
                    InitStopNode(2, 0);
                    InitStopNode(2, 1);
                    InitStopNode(2, 3);
                    InitStopNode(3, 3);

                    break;

                case 11://피피티 8번
                    myStatic.siwpeC = 15;
                    myStatic.MinimumConut = 8;
                    InitNode(3, 0, 3);
                    InitNode(3, 1, 1);
                    InitNode(3, 3, 2);
                    InitNode(1, 3, 3);

                    InitStopNode(0, 0);
                    InitStopNode(0, 1);
                    InitStopNode(0, 2);
                    InitStopNode(0, 3);
                    InitStopNode(0, 4);
                    InitStopNode(1, 0);
                    InitStopNode(1, 2);
                    InitStopNode(1, 4);
                    InitStopNode(2, 0);
                    InitStopNode(2, 4);
                    InitStopNode(3, 4);
                    InitStopNode(4, 0);
                    InitStopNode(4, 1);
                    InitStopNode(4, 2);
                    InitStopNode(4, 3);
                    InitStopNode(4, 4);

                    break;

                case 12://페이지 12
                    myStatic.siwpeC = 24;
                    myStatic.MinimumConut = 13;
                    InitStopNode(0, 0);
                    InitStopNode(0, 4);
                    InitStopNode(4, 0);
                    InitStopNode(4, 4);
                    InitStopNode(2, 0);
                    InitStopNode(2, 1);
                    InitStopNode(3, 3);
                    InitStopNode(0, 2);

                    InitNode(1, 0, 4);
                    InitNode(0, 3, 2);
                    InitNode(2, 2, 3);
                    InitNode(3, 0, 1);
                    InitNode(4, 3, 4);


                    break;

                case 13://페이지 16번
                    myStatic.siwpeC = 18;
                    myStatic.MinimumConut = 10;
                    InitStopNode(0, 0);
                    InitStopNode(0, 1);
                    InitStopNode(0, 3);
                    InitStopNode(0, 4);
                    InitStopNode(1, 0);
                    InitStopNode(3, 0);
                    InitStopNode(4, 0);
                    InitStopNode(4, 1);
                    InitStopNode(2, 2);
                    InitStopNode(1, 4);
                    InitStopNode(3, 4);
                    InitStopNode(4, 4);
                    InitStopNode(4, 3);

                    InitNode(2, 0, 4);
                    InitNode(0, 2, 3);
                    InitNode(1, 2, 1);
                    InitNode(4, 2, 4);
                    InitNode(2, 4, 2);



                    break;

                case 14:
                    myStatic.siwpeC = 23;
                    myStatic.MinimumConut = 10;
                    InitNode(4, 0, 3);
                    InitNode(2, 1, 2);
                    InitNode(2, 3, 1);
                    InitNode(3, 3, 3);
                    InitStopNode(2, 0);
                    InitStopNode(3, 0);
                    InitStopNode(0, 1);
                    InitStopNode(2, 2);
                    InitStopNode(3, 2);
                    InitStopNode(4, 2);
                    InitStopNode(0, 4);
                    InitStopNode(1, 4);
                    InitStopNode(2, 4);
                    break;


                case 15:
                    myStatic.siwpeC = 9;
                    myStatic.MinimumConut = 6;
                    InitNode(0, 1, 2);
                    InitNode(4, 0, 2);
                    InitNode(4, 4, 1);
                    InitStopNode(3, 0);
                    InitStopNode(1, 1);
                    InitStopNode(0, 2);
                    InitStopNode(1, 2);
                    InitStopNode(3, 2);
                    InitStopNode(4, 2);
                    InitStopNode(1, 3);
                    break;
                case 16:
                    myStatic.siwpeC = 18;
                    myStatic.MinimumConut = 6;

                    InitNode(1, 1, 4);
                    InitNode(2, 1, 3);
                    InitNode(1, 2, 1);
                    InitNode(2, 2, 4);
                    InitNode(3, 2, 2);

                    InitStopNode(4, 1);
                    InitStopNode(1, 4);


                    break;
                case 17:
                    myStatic.siwpeC = 16;
                    myStatic.MinimumConut = 9;

                    InitNode(0, 4, 2);
                    InitNode(2, 4, 2);
                    InitNode(4, 4, 1);

                    InitStopNode(1, 1);
                    InitStopNode(1, 2);
                    InitStopNode(1, 3);
                    InitStopNode(1, 4);
                    InitStopNode(2, 1);
                    InitStopNode(3, 1);
                    InitStopNode(3, 3);


                    break;
                case 18:
                    myStatic.siwpeC = 22;
                    myStatic.MinimumConut = 13;

                    InitNode(2, 0, 4);
                    InitNode(2, 1, 3);
                    InitNode(2, 2, 2);
                    InitNode(2, 3, 1);
                    InitNode(2, 4, 4);
                    InitStopNode(0, 1);

                    break;
                case 19:
                    myStatic.siwpeC = 10;
                    myStatic.MinimumConut = 5;

                    InitNode(2, 1, 2);
                    InitNode(1, 2, 1);
                    InitNode(3, 2, 3);
                    InitNode(2, 3, 3);

                    InitStopNode(1, 1);
                    InitStopNode(3, 1);
                    InitStopNode(1, 3);
                    InitStopNode(3, 3);

                    break;

                case 20:
                    myStatic.siwpeC = 12;
                    myStatic.MinimumConut = 4;

                    InitNode(0, 3, 1);
                    InitNode(4, 1, 1);
                    InitStopNode(0, 2);
                    InitStopNode(2, 2);
                    InitStopNode(3, 1);
                    InitStopNode(4, 3);
                    break;
                case 21:
                    myStatic.siwpeC = 25;
                    myStatic.MinimumConut = 9;
                    InitNode(0, 1, 4);
                    InitNode(0, 2, 3);
                    InitNode(0, 4, 1);
                    InitNode(3, 4, 4);
                    InitNode(4, 1, 2);
                    InitStopNode(0, 3);
                    InitStopNode(1, 1);
                    InitStopNode(2, 4);
                    InitStopNode(3, 2);
                    InitStopNode(4, 0);
                    break;
                case 22:
                    myStatic.siwpeC = 8;
                    myStatic.MinimumConut = 5;

                    InitNode(0, 0, 3);
                    InitNode(0, 4, 1);
                    InitNode(4, 0, 4);
                    InitNode(4, 4, 2);
                    InitNode(2, 2, 4);
                    InitStopNode(1, 1);
                    InitStopNode(1, 2);
                    InitStopNode(1, 3);
                    InitStopNode(2, 3);
                    InitStopNode(3, 1);
                    InitStopNode(3, 2);
                    InitStopNode(3, 3);
                    break;
                case 23:
                    myStatic.siwpeC = 8;
                    myStatic.MinimumConut = 6;
                    InitNode(1, 1, 3);
                    InitNode(1, 2, 4);
                    InitNode(2, 1, 4);
                    InitNode(2, 2, 2);
                    InitNode(3, 1, 1);
                    break;
                case 24:
                    myStatic.siwpeC = 20;
                    myStatic.MinimumConut = 13;
                    InitNode(2, 0, 3);
                    InitNode(4, 0, 1);
                    InitNode(0, 2, 4);
                    InitNode(3, 2, 2);
                    InitNode(3, 3, 4);
                    InitStopNode(0, 0);
                    InitStopNode(0, 1);
                    InitStopNode(1, 2);
                    InitStopNode(2, 2);
                    InitStopNode(3, 0);
                    InitStopNode(3, 4);
                    InitStopNode(4, 3);
                    break;

                case 25:
                    myStatic.siwpeC = 13;
                    myStatic.MinimumConut = 8;

                    InitStopNode(0, 0);
                    InitStopNode(2, 1);
                    InitStopNode(2, 2);
                    InitStopNode(2, 3);
                    InitStopNode(2, 4);
                    InitStopNode(0, 3);
                    InitNode(0, 1, 3);
                    InitNode(0, 4, 3);
                    InitNode(1, 1, 1);
                    InitNode(3, 4, 2);
                    break;

                case 26:
                    myStatic.siwpeC = 20;
                    myStatic.MinimumConut = 10;
                    InitNode(0, 0, 4);
                    InitNode(1, 1, 2);
                    InitNode(0, 4, 3);
                    InitNode(4, 2, 4);
                    InitNode(3, 3, 1);
                    InitStopNode(1, 0);
                    InitStopNode(3, 0);
                    InitStopNode(4, 0);
                    InitStopNode(1, 2);
                    InitStopNode(3, 2);
                    InitStopNode(2, 3);
                    InitStopNode(4, 3);
                    break;
                case 27:
                    myStatic.siwpeC = 15;
                    myStatic.MinimumConut = 5;
                    InitNode(2, 1, 1);
                    InitNode(3, 1, 3);
                    InitNode(4, 2, 3);
                    InitNode(4, 4, 2);
                    InitStopNode(1, 0);
                    InitStopNode(2, 0);
                    InitStopNode(3, 0);
                    InitStopNode(4, 0);
                    InitStopNode(4, 1);
                    InitStopNode(1, 2);
                    InitStopNode(2, 3);
                    InitStopNode(2, 4);
                    InitStopNode(4, 3);
                    break;
                case 28:
                    myStatic.siwpeC = 16;
                    myStatic.MinimumConut = 11;
                    InitNode(4, 0, 1);
                    InitNode(4, 1, 2);
                    InitNode(4, 2, 3);
                    InitNode(4, 4, 3);
                    InitStopNode(0, 0);
                    InitStopNode(2, 1);
                    InitStopNode(0, 3);
                    InitStopNode(2, 3);
                    InitStopNode(3, 3);
                    InitStopNode(4, 3);
                    break;

                case 29:
                    myStatic.siwpeC = 10;//중복 느낌
                    myStatic.MinimumConut = 5;
                    InitNode(2, 1, 2);
                    InitNode(1, 2, 3);
                    InitNode(2, 3, 3);
                    InitNode(3, 2, 1);
                    InitStopNode(1, 1);
                    InitStopNode(1, 3);
                    InitStopNode(2, 2);
                    InitStopNode(3, 1);
                    InitStopNode(3, 3);
                    break;
                case 30:
                    myStatic.siwpeC = 15;
                    myStatic.MinimumConut = 6;
                    InitNode(2, 0, 2);
                    InitNode(4, 0, 1);
                    InitNode(1, 2, 4);
                    InitNode(4, 3, 4);
                    InitNode(3, 4, 3);
                    InitStopNode(3, 0);
                    InitStopNode(1, 1);
                    InitStopNode(2, 3);
                    InitStopNode(3, 3);
                    break;
                case 31:
                    myStatic.siwpeC = 12;
                    myStatic.MinimumConut = 5;
                    InitNode(0, 0, 1);
                    InitNode(3, 0, 3);
                    InitNode(0, 1, 3);
                    InitNode(2, 2, 2);
                    InitStopNode(1, 0);
                    InitStopNode(2, 0);
                    InitStopNode(4, 0);
                    InitStopNode(1, 2);
                    InitStopNode(1, 3);
                    InitStopNode(2, 3);
                    InitStopNode(3, 2);
                    InitStopNode(3, 3);
                    break;



                ////////////이 위까지 작업 됨
                case 32://본적 있는거같다고 함
                    myStatic.siwpeC = 12;
                    myStatic.MinimumConut = 9;
                    InitNode(0, 0, 2);
                    InitNode(4, 0, 2);
                    InitNode(0, 3, 2);
                    InitNode(4, 4, 2);
                    InitStopNode(1, 0);
                    InitStopNode(2, 0);
                    InitStopNode(3, 0);
                    InitStopNode(0, 2);
                    InitStopNode(1, 2);
                    InitStopNode(3, 2);
                    InitStopNode(4, 2);
                    InitStopNode(1, 3);
                    InitStopNode(3, 4);
                    break;
                case 33://전에 본듯함
                    myStatic.siwpeC = 20;
                    myStatic.MinimumConut = 14;
                    InitNode(0, 0, 3);
                    InitNode(4, 0, 1);
                    InitNode(0, 3, 2);
                    InitNode(4, 4, 3);
                    InitStopNode(1, 0);
                    InitStopNode(2, 1);
                    InitStopNode(4, 1);
                    InitStopNode(0, 2);
                    InitStopNode(4, 2);
                    InitStopNode(1, 3);
                    InitStopNode(3, 4);
                    break;
                case 34:
                    myStatic.siwpeC = 20;
                    myStatic.MinimumConut = 9;
                    InitNode(0, 0, 3);
                    InitNode(4, 0, 1);
                    InitNode(0, 4, 4);
                    InitNode(4, 4, 2);
                    InitNode(2, 2, 4);
                    InitStopNode(1, 1);
                    InitStopNode(1, 2);
                    InitStopNode(1, 3);
                    InitStopNode(3, 1);
                    InitStopNode(3, 2);
                    InitStopNode(3, 3);
                    InitStopNode(2, 3);
                    break;
                case 35:
                    myStatic.siwpeC = 16;
                    myStatic.MinimumConut = 4;
                    InitNode(1, 0, 2);
                    InitNode(3, 0, 2);
                    InitNode(0, 4, 1);
                    InitStopNode(0, 0);
                    InitStopNode(0, 2);
                    InitStopNode(2, 0);
                    InitStopNode(2, 1);
                    InitStopNode(2, 3);
                    InitStopNode(3, 2);


                    break;
                case 36:
                    myStatic.siwpeC = 16;
                    myStatic.MinimumConut = 5;
                    InitNode(0, 0, 3);
                    InitNode(1, 2, 2);
                    InitNode(3, 2, 2);
                    InitNode(0, 4, 3);
                    InitNode(4, 4, 2);
                    InitStopNode(0, 1);
                    InitStopNode(0, 2);
                    InitStopNode(2, 0);
                    InitStopNode(2, 1);
                    InitStopNode(2, 3);
                    InitStopNode(3, 3);
                    break;
                case 37:
                    myStatic.siwpeC = 20;
                    myStatic.MinimumConut = 13;
                    InitNode(0, 0, 4);
                    InitNode(2, 1, 1);
                    InitNode(2, 2, 2);
                    InitNode(2, 3, 3);
                    InitNode(4, 4, 4);
                    InitStopNode(2, 0);
                    InitStopNode(0, 2);
                    InitStopNode(1, 2);
                    InitStopNode(3, 2);
                    InitStopNode(3, 3);
                    InitStopNode(4, 2);
                    InitStopNode(4, 3);
                    break;
                case 38:
                    myStatic.siwpeC = 18;
                    myStatic.MinimumConut = 7;
                    InitNode(0, 0, 4);
                    InitNode(2, 0, 2);
                    InitNode(4, 0, 4);
                    InitNode(1, 2, 1);
                    InitNode(3, 2, 3);
                    InitStopNode(2, 2);
                    break;

                case 39:
                    myStatic.siwpeC = 17;
                    myStatic.MinimumConut = 5;
                    InitNode(0, 0, 4);
                    InitNode(1, 0, 4);
                    InitNode(2, 0, 4);
                    InitNode(3, 0, 4);
                    InitNode(4, 0, 3);
                    InitNode(2, 2, 3);
                    InitNode(3, 2, 3);
                    InitNode(4, 2, 3);
                    InitNode(0, 4, 1);
                    InitNode(2, 4, 2);
                    InitNode(3, 4, 2);
                    InitNode(4, 4, 2);
                    break;
                case 40:
                    myStatic.siwpeC = 12;
                    myStatic.MinimumConut = 10;
                    InitNode(4, 0, 1);
                    InitNode(0, 2, 2);
                    InitNode(4, 4, 2);
                    InitStopNode(0, 0);
                    InitStopNode(2, 1);
                    InitStopNode(1, 2);
                    InitStopNode(0, 3);
                    InitStopNode(1, 3);
                    InitStopNode(3, 3);
                    InitStopNode(4, 3);
                    break;
                case 41:
                    myStatic.siwpeC = 10;
                    myStatic.MinimumConut = 5;
                    InitNode(0, 0, 3);
                    InitNode(0, 3, 3);
                    InitNode(0, 4, 1);
                    InitNode(4, 2, 2);
                    InitStopNode(1, 1);
                    InitStopNode(4, 1);
                    InitStopNode(1, 2);
                    InitStopNode(3, 2);
                    InitStopNode(1, 3);
                    InitStopNode(3, 3);
                    InitStopNode(1, 4);
                    break;
                case 42:
                    myStatic.siwpeC = 13;
                    myStatic.MinimumConut = 8;
                    InitNode(0, 0, 1);
                    InitNode(4, 0, 2);
                    InitNode(0, 3, 2);
                    InitStopNode(1, 0);
                    InitStopNode(2, 1);
                    InitStopNode(4, 1);
                    InitStopNode(0, 2);
                    InitStopNode(2, 2);
                    InitStopNode(2, 3);
                    break;
                case 43:
                    myStatic.siwpeC = 13;
                    myStatic.MinimumConut = 7;
                    InitNode(3, 1, 1);
                    InitNode(3, 3, 2);
                    InitNode(2, 4, 2);
                    InitStopNode(0, 0);
                    InitStopNode(0, 3);
                    InitStopNode(1, 2);
                    InitStopNode(3, 2);
                    InitStopNode(3, 4);
                    InitStopNode(4, 1);
                    InitStopNode(4, 2);
                    InitStopNode(4, 3);
                    InitStopNode(4, 4);
                    break;
                case 44:
                    myStatic.siwpeC = 13;
                    myStatic.MinimumConut = 8;
                    InitNode(4, 0, 3);
                    InitNode(0, 3, 1);
                    InitNode(4, 3, 3);
                    InitNode(2, 4, 2);
                    InitStopNode(0, 2);
                    InitStopNode(2, 3);
                    InitStopNode(4, 2);

                    break;
                case 45:
                    myStatic.siwpeC = 12;
                    myStatic.MinimumConut = 6;
                    InitNode(4, 0, 1);
                    InitNode(0, 4, 2);
                    InitNode(1, 4, 4);
                    InitNode(3, 4, 4);
                    InitNode(4, 4, 3);
                    InitStopNode(2, 2);
                    InitStopNode(2, 4);
                    break;

                case 46:
                    myStatic.siwpeC = 20;
                    myStatic.MinimumConut = 10;
                    InitNode(0, 0, 3);
                    InitNode(4, 0, 3);
                    InitNode(0, 4, 2);
                    InitNode(4, 4, 1);
                    InitStopNode(0, 2);
                    // InitStopNode(2, 0);
                    InitStopNode(4, 2);
                    InitStopNode(2, 4);
                    break;

                case 47:
                    myStatic.siwpeC = 16;
                    myStatic.MinimumConut = 10;

                    InitStopNode(0, 3);
                    InitStopNode(1, 1);
                    InitStopNode(1, 3);
                    InitNode(1, 4, 4);
                    InitStopNode(2, 0);
                    InitStopNode(3, 0);
                    InitNode(3, 1, 3);
                    InitNode(3, 2, 4);
                    InitStopNode(3, 3);
                    InitStopNode(3, 4);
                    InitStopNode(4, 0);
                    InitNode(4, 1, 2);
                    InitNode(4, 3, 1);
                    InitStopNode(4, 4);
                    break;
                case 48:
                    myStatic.siwpeC = 20;
                    myStatic.MinimumConut = 11;

                    InitStopNode(0, 0);
                    InitStopNode(0, 2);
                    InitStopNode(0, 3);
                    InitStopNode(0, 4);
                    InitNode(1, 0, 4);
                    InitNode(1, 4, 4);
                    InitStopNode(1, 3);
                    InitNode(2, 1, 3);
                    InitStopNode(2, 3);
                    InitNode(3, 1, 2);
                    InitNode(4, 0, 1);
                    InitStopNode(4, 1);
                    break;
                case 49:
                    myStatic.siwpeC = 28;
                    myStatic.MinimumConut = 18;

                    InitNode(0, 2, 3);
                    InitStopNode(0, 3);
                    InitNode(1, 1, 4);
                    InitStopNode(3, 2);
                    InitNode(3, 3, 1);
                    InitStopNode(4, 0);
                    InitNode(4, 1, 2);
                    InitNode(4, 2, 4);
                    break;
                case 50:
                    myStatic.siwpeC = 20;
                    myStatic.MinimumConut = 9;

                    InitNode(0, 1, 1);
                    InitNode(0, 3, 3);
                    InitNode(0, 4, 4);
                    InitStopNode(1, 1);
                    InitNode(1, 3, 2);
                    InitStopNode(1, 4);
                    InitStopNode(2, 1);
                    InitStopNode(2, 4);
                    InitStopNode(3, 1);
                    InitStopNode(3, 3);
                    InitNode(4, 0, 4);
                    InitStopNode(4, 1);
                    InitStopNode(4, 2);
                    InitStopNode(4, 3);
                    InitStopNode(3, 4);
                    InitStopNode(4, 4);
                    break;
                case 51:
                    myStatic.siwpeC = 17;
                    myStatic.MinimumConut = 8;

                    InitNode(0, 1, 4);
                    InitNode(1, 2, 2);
                    InitNode(1, 3, 3);
                    InitStopNode(2, 1);
                    InitStopNode(2, 2);
                    InitNode(3, 0, 1);
                    InitNode(3, 2, 4);
                    InitStopNode(4, 2);
                    break;
                case 52:
                    myStatic.siwpeC = 18;
                    myStatic.MinimumConut = 12;

                    InitStopNode(0, 0);
                    InitStopNode(0, 3);
                    InitNode(0, 4, 3);
                    InitNode(1, 0, 1);
                    InitStopNode(1, 1);
                    InitNode(1, 2, 2);
                    InitNode(1, 3, 4);
                    InitStopNode(2, 2);
                    InitStopNode(2, 3);
                    InitNode(3, 0, 4);
                    InitStopNode(3, 1);
                    InitStopNode(3, 2);
                    InitStopNode(4, 4);
                    break;
                case 53:
                    myStatic.siwpeC = 17;
                    myStatic.MinimumConut = 8;

                    InitStopNode(1, 1);
                    InitStopNode(1, 3);
                    InitNode(1, 4, 3);
                    InitStopNode(2, 0);
                    InitNode(2, 1, 4);
                    InitNode(2, 2, 2);
                    InitStopNode(2, 3);
                    InitStopNode(3, 0);
                    InitNode(3, 1, 4);
                    InitNode(3, 4, 1);
                    InitStopNode(4, 3);
                    break;
                case 54:
                    myStatic.siwpeC = 23;
                    myStatic.MinimumConut = 14;

                    InitNode(0, 1, 4);
                    InitNode(0, 3, 3);
                    InitNode(0, 4, 1);
                    InitStopNode(1, 1);
                    InitStopNode(1, 2);
                    InitStopNode(1, 3);
                    InitStopNode(2, 1);
                    InitNode(3, 1, 4);
                    InitStopNode(3, 3);
                    InitStopNode(4, 1);
                    InitNode(4, 3, 2);
                    break;
                case 55:
                    myStatic.siwpeC = 28;
                    myStatic.MinimumConut = 20;

                    InitStopNode(0, 0);
                    InitStopNode(1, 1);
                    InitStopNode(1, 2);
                    InitNode(2, 2, 1);
                    InitNode(0, 1, 3);
                    InitStopNode(2, 4);
                    InitNode(3, 0, 4);
                    InitStopNode(3, 1);
                    InitNode(3, 2, 2);
                    InitStopNode(4, 0);
                    InitNode(4, 1, 4);
                    break;
                case 56:
                    myStatic.siwpeC = 17;
                    myStatic.MinimumConut = 11;

                    InitStopNode(0, 0);
                    InitNode(0, 4, 3);
                    InitNode(1, 0, 1);
                    InitStopNode(1, 1);
                    InitStopNode(1, 3);
                    InitStopNode(1, 4);
                    InitNode(2, 2, 2);
                    InitStopNode(2, 3);
                    InitNode(3, 0, 4);
                    InitStopNode(3, 1);
                    InitStopNode(3, 3);
                    InitStopNode(4, 4);
                    InitStopNode(2, 4);
                    InitNode(3, 2, 4);
                    InitStopNode(3, 4);
                    InitStopNode(4, 3);
                    break;
                case 57:
                    myStatic.siwpeC = 21;
                    myStatic.MinimumConut = 13;

                    InitStopNode(0, 0);
                    InitStopNode(0, 4);
                    InitNode(1, 4, 2);
                    InitNode(2, 0, 4);
                    InitNode(2, 2, 1);
                    InitStopNode(2, 3);
                    InitNode(2, 4, 3);
                    InitStopNode(3, 0);
                    InitStopNode(3, 1);
                    InitNode(4, 0, 4);
                    InitStopNode(4, 3);
                    break;
                case 58:
                    myStatic.siwpeC = 20;
                    myStatic.MinimumConut = 15;

                    InitNode(0, 1, 4);
                    InitNode(0, 2, 3);
                    InitStopNode(1, 0);
                    InitStopNode(1, 1);
                    InitStopNode(1, 2);
                    InitStopNode(2, 0);
                    InitStopNode(2, 2);
                    InitNode(3, 1, 4);
                    InitNode(3, 3, 1);
                    InitStopNode(3, 4);
                    InitStopNode(4, 1);
                    InitNode(4, 2, 2);
                    break;
                case 59:
                    myStatic.siwpeC = 14;
                    myStatic.MinimumConut = 7;

                    InitStopNode(0, 0);
                    InitStopNode(0, 4);
                    InitStopNode(1, 0);
                    InitNode(1, 3, 4);
                    InitStopNode(1, 4);
                    InitNode(2, 2, 2);
                    InitNode(3, 0, 3);
                    InitNode(3, 3, 1);
                    InitNode(4, 2, 4);
                    InitStopNode(4, 3);
                    InitStopNode(4, 4);
                    break;
                case 60:
                    myStatic.siwpeC = 18;
                    myStatic.MinimumConut = 12;

                    InitStopNode(0, 3);
                    InitNode(0, 4, 3);
                    InitStopNode(1, 2);
                    InitStopNode(1, 3);
                    InitNode(2, 1, 2);
                    InitNode(2, 4, 4);
                    InitNode(3, 3, 1);
                    InitNode(4, 1, 4);
                    InitStopNode(4, 2);
                    InitStopNode(4, 4);
                    break;
                case 61:
                    myStatic.siwpeC = 18;
                    myStatic.MinimumConut = 10;

                    InitStopNode(0, 0);
                    InitStopNode(0, 1);
                    InitNode(1, 0, 2);
                    InitNode(1, 1, 4);
                    InitNode(2, 0, 3);
                    InitNode(2, 1, 4);
                    InitStopNode(2, 4);
                    InitStopNode(3, 0);
                    InitNode(3, 1, 1);
                    InitStopNode(4, 0);
                    InitStopNode(4, 2);
                    InitStopNode(4, 3);
                    break;
                case 62:
                    myStatic.siwpeC = 20;
                    myStatic.MinimumConut = 10;

                    InitNode(0, 2, 4);
                    InitNode(0, 3, 3);
                    InitStopNode(1, 0);
                    InitStopNode(1, 1);
                    InitStopNode(1, 2);
                    InitNode(1, 3, 2);
                    InitNode(1, 4, 4);
                    InitStopNode(2, 0);
                    InitNode(2, 3, 1);
                    InitStopNode(3, 3);
                    InitStopNode(4, 1);
                    break;
                case 63:
                    myStatic.siwpeC = 15;
                    myStatic.MinimumConut = 6;

                    InitNode(0, 1, 2);
                    InitStopNode(0, 2);
                    InitStopNode(1, 0);
                    InitStopNode(1, 4);
                    InitStopNode(2, 0);
                    InitNode(2, 2, 1);
                    InitStopNode(3, 0);
                    InitNode(3, 1, 4);
                    InitNode(3, 4, 4);
                    InitStopNode(4, 1);
                    InitNode(4, 2, 3);
                    break;
                case 64:
                    myStatic.siwpeC = 22;
                    myStatic.MinimumConut = 14;

                    InitNode(0, 1, 3);
                    InitNode(0, 3, 4);
                    InitNode(1, 0, 1);
                    InitStopNode(1, 1);
                    InitStopNode(1, 2);
                    InitStopNode(1, 4);
                    InitStopNode(2, 4);
                    InitStopNode(3, 2);
                    InitStopNode(3, 3);
                    InitNode(4, 1, 4);
                    InitNode(3, 4, 2);
                    break;
                case 65:
                    myStatic.siwpeC = 16;
                    myStatic.MinimumConut = 10;

                    InitStopNode(0, 4);
                    InitNode(1, 1, 4);
                    InitStopNode(1, 4);
                    InitStopNode(2, 1);
                    InitNode(2, 3, 2);
                    InitStopNode(2, 4);
                    InitNode(3, 1, 1);
                    InitNode(3, 3, 4);
                    InitStopNode(4, 1);
                    InitNode(4, 3, 3);
                    InitStopNode(4, 4);
                    break;
                case 66:
                    myStatic.siwpeC = 22;
                    myStatic.MinimumConut = 15;

                    InitStopNode(0, 0);
                    InitNode(0, 4, 4);
                    InitStopNode(1, 1);
                    InitStopNode(1, 3);
                    InitStopNode(1, 4);
                    InitNode(2, 3, 1);
                    InitNode(2, 4, 3);
                    InitStopNode(3, 0);
                    InitNode(3, 2, 4);
                    InitStopNode(3, 3);
                    InitStopNode(4, 0);
                    InitStopNode(4, 1);
                    InitNode(4, 2, 2);
                    break;
                case 67:
                    myStatic.siwpeC = 20;
                    myStatic.MinimumConut = 12;

                    InitStopNode(0, 0);
                    InitNode(0, 1, 3);
                    InitStopNode(0, 3);
                    InitStopNode(0, 4);
                    InitStopNode(2, 0);
                    InitNode(2, 1, 1);
                    InitNode(2, 2, 4);
                    InitNode(2, 4, 2);
                    InitStopNode(3, 0);
                    InitNode(3, 3, 4);
                    InitStopNode(4, 2);
                    InitStopNode(4, 4);
                    break;
                case 68:
                    myStatic.siwpeC = 24;
                    myStatic.MinimumConut = 18;

                    InitStopNode(0, 0);
                    InitStopNode(0, 1);
                    InitNode(0, 2, 1);
                    InitNode(0, 3, 4);
                    InitStopNode(0, 4);
                    InitNode(1, 0, 2);
                    InitStopNode(2, 0);
                    InitNode(2, 1, 3);
                    InitStopNode(2, 2);
                    InitNode(3, 1, 4);
                    InitStopNode(3, 2);
                    InitStopNode(3, 4);
                    InitStopNode(4, 0);
                    InitStopNode(4, 1);
                    InitStopNode(4, 2);
                    break;
                case 69:
                    myStatic.siwpeC = 20;
                    myStatic.MinimumConut = 14;

                    InitNode(0, 1, 3);
                    InitNode(0, 2, 1);
                    InitStopNode(0, 3);
                    InitNode(1, 0, 2);
                    InitStopNode(1, 1);
                    InitStopNode(1, 4);
                    InitStopNode(2, 3);
                    InitStopNode(2, 4);
                    InitStopNode(3, 0);
                    InitStopNode(3, 1);
                    InitStopNode(3, 3);
                    InitStopNode(3, 4);
                    InitNode(4, 2, 4);
                    InitNode(4, 3, 4);
                    InitStopNode(4, 4);
                    break;
                case 70:
                    myStatic.siwpeC = 17;
                    myStatic.MinimumConut = 10;

                    InitStopNode(0, 0);
                    InitStopNode(0, 3);
                    InitStopNode(1, 0);
                    InitNode(1, 3, 3);
                    InitStopNode(1, 4);
                    InitNode(2, 1, 4);
                    InitNode(2, 2, 2);
                    InitNode(2, 3, 4);
                    InitStopNode(3, 4);
                    InitStopNode(4, 0);
                    InitStopNode(4, 1);
                    InitNode(4, 3, 1);
                    InitStopNode(4, 4);
                    break;
                case 71:
                    myStatic.siwpeC = 17;
                    myStatic.MinimumConut = 8;

                    InitStopNode(0, 0);
                    InitStopNode(0, 1);
                    InitNode(0, 2, 2);
                    InitNode(0, 4, 4);
                    InitStopNode(1, 0);
                    InitStopNode(4, 0);
                    InitStopNode(1, 1);
                    InitNode(1, 3, 4);
                    InitStopNode(2, 0);
                    InitStopNode(2, 1);
                    InitNode(2, 2, 3);
                    InitStopNode(2, 4);
                    InitStopNode(3, 0);
                    InitStopNode(3, 1);
                    InitStopNode(3, 2);
                    InitStopNode(3, 4);
                    InitStopNode(4, 1);
                    InitStopNode(4, 2);
                    InitNode(4, 4, 1);
                    break;
                case 72:
                    myStatic.siwpeC = 21;
                    myStatic.MinimumConut = 14;

                    InitStopNode(0, 0);
                    InitNode(0, 1, 2);
                    InitNode(0, 2, 4);
                    InitNode(0, 3, 1);
                    InitStopNode(1, 0);
                    InitStopNode(1, 2);
                    InitStopNode(2, 0);
                    InitStopNode(2, 1);
                    InitStopNode(2, 2);
                    InitNode(2, 3, 4);
                    InitStopNode(3, 0);
                    InitNode(3, 3, 3);
                    InitStopNode(3, 4);
                    InitStopNode(4, 0);
                    InitStopNode(4, 2);
                    break;
                case 73:
                    myStatic.siwpeC = 26;
                    myStatic.MinimumConut = 20;

                    InitNode(0, 2, 3);
                    InitNode(0, 3, 4);
                    InitStopNode(1, 0);
                    InitNode(1, 4, 2);
                    InitNode(2, 1, 4);
                    InitStopNode(2, 2);
                    InitStopNode(3, 0);
                    InitNode(3, 1, 1);
                    InitStopNode(3, 4);
                    InitStopNode(4, 0);
                    break;
                case 74:
                    myStatic.siwpeC = 20;
                    myStatic.MinimumConut = 12;

                    InitStopNode(0, 0);
                    InitStopNode(0, 4);
                    InitNode(1, 0, 4);
                    InitNode(1, 2, 2);
                    InitStopNode(1, 4);
                    InitNode(2, 3, 1);
                    InitNode(3, 4, 3);
                    InitNode(4, 0, 4);
                    InitStopNode(4, 3);
                    InitStopNode(4, 4);
                    break;
                case 75:
                    myStatic.siwpeC = 20;
                    myStatic.MinimumConut = 13;

                    InitStopNode(0, 0);
                    InitNode(0, 2, 3);
                    InitStopNode(0, 3);
                    InitStopNode(0, 4);
                    InitStopNode(1, 0);
                    InitNode(1, 1, 2);
                    InitNode(1, 2, 4);
                    InitNode(3, 0, 4);
                    InitStopNode(3, 2);
                    InitNode(3, 3, 1);
                    InitStopNode(3, 4);
                    InitStopNode(4, 0);
                    InitStopNode(4, 1);
                    InitStopNode(4, 2);
                    InitStopNode(4, 3);
                    InitStopNode(4, 4);
                    break;
                case 76:
                    myStatic.siwpeC = 23;
                    myStatic.MinimumConut = 15;

                    InitStopNode(0, 1);
                    InitStopNode(0, 0);
                    InitNode(0, 4, 3);
                    InitStopNode(1, 0);
                    InitNode(2, 2, 4);
                    InitStopNode(2, 3);
                    InitNode(2, 4, 4);
                    InitNode(3, 0, 1);
                    InitStopNode(3, 1);
                    InitStopNode(3, 4);
                    InitNode(4, 0, 2);
                    InitStopNode(4, 2);
                    break;
                case 77:
                    myStatic.siwpeC = 19;
                    myStatic.MinimumConut = 11;

                    InitStopNode(0, 0);
                    InitNode(0, 2, 2);
                    InitNode(0, 4, 4);
                    InitStopNode(1, 1);
                    InitNode(2, 0, 1);
                    InitNode(2, 3, 3);
                    InitStopNode(3, 0);
                    InitStopNode(3, 1);
                    InitStopNode(3, 4);
                    InitStopNode(4, 0);
                    InitNode(4, 3, 4);
                    InitStopNode(4, 4);
                    break;
                case 78:
                    myStatic.siwpeC = 26;
                    myStatic.MinimumConut = 19;

                    InitStopNode(0, 0);
                    InitStopNode(0, 2);
                    InitNode(0, 4, 4);
                    InitNode(1, 2, 1);
                    InitNode(1, 4, 2);
                    InitStopNode(2, 0);
                    InitStopNode(2, 1);
                    InitNode(3, 0, 4);
                    InitStopNode(3, 3);
                    InitNode(3, 4, 3);
                    InitStopNode(4, 4);
                    break;
                case 79:
                    myStatic.siwpeC = 19;
                    myStatic.MinimumConut = 12;

                    InitStopNode(0, 1);
                    InitNode(0, 2, 1);
                    InitNode(1, 0, 4);
                    InitStopNode(2, 0);
                    InitStopNode(2, 1);
                    InitNode(2, 3, 4);
                    InitNode(2, 4, 3);
                    InitStopNode(3, 0);
                    InitNode(3, 2, 2);
                    InitStopNode(3, 4);
                    InitStopNode(4, 0);
                    InitStopNode(4, 1);
                    InitStopNode(4, 2);
                    break;
                case 80:
                    myStatic.siwpeC = 20;
                    myStatic.MinimumConut = 14;

                    InitStopNode(0, 0);
                    InitStopNode(0, 1);
                    InitStopNode(0, 2);
                    InitNode(0, 3, 4);
                    InitStopNode(0, 4);
                    InitNode(1, 1, 4);
                    InitNode(1, 2, 1);
                    InitNode(1, 3, 3);
                    InitStopNode(2, 1);
                    InitNode(2, 2, 2);
                    InitStopNode(2, 3);
                    InitStopNode(2, 4);
                    InitStopNode(3, 1);
                    InitStopNode(4, 0);
                    InitStopNode(4, 2);
                    InitStopNode(4, 3);
                    InitStopNode(4, 4);
                    break;
                case 81:
                    myStatic.siwpeC = 21;
                    myStatic.MinimumConut = 15;

                    InitNode(0, 0, 4);
                    InitStopNode(0, 1);
                    InitStopNode(0, 4);
                    InitNode(1, 0, 3);
                    InitStopNode(1, 2);
                    InitNode(1, 4, 4);
                    InitStopNode(2, 0);
                    InitStopNode(2, 2);
                    InitNode(2, 4, 1);
                    InitNode(3, 1, 2);
                    InitStopNode(4, 3);
                    InitStopNode(4, 4);
                    break;
                case 82:
                    myStatic.siwpeC = 15;
                    myStatic.MinimumConut = 10;

                    InitStopNode(0, 0);
                    InitNode(1, 1, 4);
                    InitNode(1, 2, 4);
                    InitStopNode(2, 0);
                    InitNode(2, 1, 2);
                    InitStopNode(2, 2);
                    InitStopNode(2, 4);
                    InitStopNode(3, 0);
                    InitNode(3, 2, 1);
                    InitNode(3, 4, 3);
                    InitStopNode(4, 0);
                    InitStopNode(4, 2);
                    break;
                case 83:
                    myStatic.siwpeC = 14;
                    myStatic.MinimumConut = 8;

                    InitStopNode(0, 1);
                    InitStopNode(0, 0);
                    InitNode(0, 2, 3);
                    InitNode(0, 4, 1);
                    InitStopNode(1, 0);
                    InitStopNode(1, 1);
                    InitStopNode(1, 3);
                    InitStopNode(1, 4);
                    InitStopNode(2, 0);
                    InitStopNode(2, 1);
                    InitStopNode(2, 3);
                    InitNode(3, 4, 2);
                    InitStopNode(4, 1);
                    InitStopNode(4, 2);
                    InitNode(4, 4, 3);
                    break;
                case 84:
                    myStatic.siwpeC = 13;
                    myStatic.MinimumConut = 6;

                    InitNode(1, 2, 4);
                    InitStopNode(1, 3);
                    InitStopNode(1, 4);
                    InitNode(2, 1, 4);
                    InitNode(2, 4, 1);
                    InitStopNode(3, 2);
                    InitStopNode(3, 3);
                    InitNode(3, 4, 2);
                    InitStopNode(4, 0);
                    InitNode(4, 2, 3);
                    InitStopNode(4, 4);
                    break;
                case 85:
                    myStatic.siwpeC = 18;
                    myStatic.MinimumConut = 12;

                    InitStopNode(0, 1);
                    InitStopNode(0, 4);
                    InitNode(1, 3, 3);
                    InitNode(2, 0, 4);
                    InitStopNode(2, 4);
                    InitStopNode(3, 0);
                    InitNode(3, 1, 4);
                    InitStopNode(3, 2);
                    InitNode(4, 0, 1);
                    InitNode(4, 3, 2);
                    break;
                case 86:
                    myStatic.siwpeC = 17;
                    myStatic.MinimumConut = 10;

                    InitNode(0, 2, 3);
                    InitNode(1, 1, 4);
                    InitStopNode(1, 3);
                    InitStopNode(1, 4);
                    InitStopNode(2, 3);
                    InitStopNode(3, 0);
                    InitNode(3, 2, 4);
                    InitNode(3, 3, 1);
                    InitStopNode(3, 4);
                    InitNode(4, 1, 2);
                    InitStopNode(4, 4);
                    break;
                case 87:
                    myStatic.siwpeC = 22;
                    myStatic.MinimumConut = 15;

                    InitStopNode(0, 0);
                    InitNode(0, 1, 4);
                    InitStopNode(0, 2);
                    InitStopNode(0, 3);
                    InitNode(1, 0, 2);
                    InitNode(1, 2, 4);
                    InitStopNode(1, 3);
                    InitStopNode(2, 1);
                    InitNode(2, 4, 1);
                    InitNode(3, 1, 3);
                    InitStopNode(3, 2);
                    InitStopNode(3, 3);
                    InitStopNode(4, 0);
                    InitStopNode(4, 2);
                    break;
                case 88:
                    myStatic.siwpeC = 14;
                    myStatic.MinimumConut = 7;

                    InitNode(0, 0, 4);
                    InitNode(1, 0, 3);
                    InitStopNode(1, 3);
                    InitStopNode(2, 0);
                    InitStopNode(2, 2);
                    InitNode(2, 3, 2);
                    InitNode(3, 0, 1);
                    InitNode(3, 1, 4);
                    InitStopNode(3, 4);
                    InitStopNode(4, 2);
                    InitStopNode(4, 3);
                    InitStopNode(4, 4);
                    break;
                case 89:
                    myStatic.siwpeC = 23;
                    myStatic.MinimumConut = 17;

                    InitNode(0, 1, 4);
                    InitStopNode(0, 2);
                    InitStopNode(0, 3);
                    InitNode(0, 4, 4);
                    InitNode(1, 3, 3);
                    InitNode(2, 3, 1);
                    InitStopNode(3, 0);
                    InitStopNode(3, 4);
                    InitNode(4, 2, 2);
                    InitStopNode(4, 4);
                    break;
                case 90:
                    myStatic.siwpeC = 16;
                    myStatic.MinimumConut = 9;

                    InitNode(0, 0, 4);
                    InitNode(0, 2, 2);
                    InitNode(0, 3, 3);
                    InitStopNode(1, 0);
                    InitNode(1, 2, 4);
                    InitStopNode(1, 3);
                    InitNode(2, 0, 1);
                    InitStopNode(3, 4);
                    InitStopNode(4, 0);
                    InitStopNode(4, 2);
                    break;
                case 91:
                    myStatic.siwpeC = 18;
                    myStatic.MinimumConut = 12;

                    InitStopNode(0, 1);
                    InitStopNode(0, 0);
                    InitStopNode(0, 4);
                    InitStopNode(1, 0);
                    InitStopNode(1, 1);
                    InitNode(1, 2, 2);
                    InitNode(1, 3, 4);
                    InitStopNode(1, 4);
                    InitNode(2, 2, 1);
                    InitStopNode(2, 4);
                    InitStopNode(3, 0);
                    InitNode(3, 1, 3);
                    InitStopNode(3, 3);
                    InitStopNode(3, 4);
                    InitStopNode(4, 0);
                    InitNode(4, 1, 4);
                    break;
                case 92:
                    myStatic.siwpeC = 33;
                    myStatic.MinimumConut = 26;

                    InitNode(0, 0, 4);
                    InitNode(1, 1, 1);
                    InitStopNode(1, 3);
                    InitNode(1, 4, 4);
                    InitStopNode(2, 0);
                    InitStopNode(2, 2);
                    InitStopNode(3, 0);
                    InitStopNode(3, 1);
                    InitStopNode(3, 4);
                    InitStopNode(4, 0);
                    InitStopNode(4, 1);
                    InitStopNode(4, 2);
                    InitNode(4, 3, 2);
                    InitNode(4, 4, 3);
                    break;
                case 93:
                    myStatic.siwpeC = 18;
                    myStatic.MinimumConut = 11;

                    InitStopNode(0, 0);
                    InitStopNode(0, 1);
                    InitNode(0, 4, 3);
                    InitNode(1, 0, 2);
                    InitNode(1, 4, 4);
                    InitStopNode(2, 0);
                    InitNode(4, 2, 4);
                    InitStopNode(2, 3);
                    InitNode(3, 0, 1);
                    InitStopNode(4, 1);
                    InitStopNode(4, 3);
                    InitStopNode(4, 4);
                    break;
                case 94:
                    myStatic.siwpeC = 19;
                    myStatic.MinimumConut = 13;

                    InitNode(0, 1, 3);
                    InitStopNode(0, 2);
                    InitStopNode(0, 3);
                    InitStopNode(0, 4);
                    InitNode(1, 1, 2);
                    InitStopNode(1, 2);
                    InitNode(3, 0, 4);
                    InitStopNode(3, 1);
                    InitNode(3, 2, 4);
                    InitNode(3, 4, 1);
                    InitStopNode(4, 0);
                    InitStopNode(4, 1);
                    InitStopNode(4, 4);
                    break;
                case 95:
                    myStatic.siwpeC = 15;
                    myStatic.MinimumConut = 7;

                    InitNode(0, 0, 4);
                    InitStopNode(1, 0);
                    InitStopNode(1, 3);
                    InitStopNode(2, 0);
                    InitStopNode(2, 3);
                    InitNode(2, 4, 3);
                    InitNode(3, 1, 1);
                    InitNode(4, 0, 2);
                    InitNode(4, 2, 4);
                    InitStopNode(4, 3);
                    break;
                case 96:
                    myStatic.siwpeC = 21;
                    myStatic.MinimumConut = 15;

                    InitStopNode(0, 1);
                    InitStopNode(0, 2);
                    InitNode(1, 0, 4);
                    InitNode(1, 1, 2);
                    InitStopNode(2, 0);
                    InitStopNode(2, 1);
                    InitStopNode(2, 3);
                    InitNode(3, 0, 4);
                    InitNode(3, 2, 3);
                    InitStopNode(3, 3);
                    InitStopNode(4, 0);
                    InitNode(4, 4, 1);
                    break;
                case 97:
                    myStatic.siwpeC = 20;
                    myStatic.MinimumConut = 14;

                    InitStopNode(0, 1);
                    InitNode(0, 4, 4);
                    InitStopNode(1, 0);
                    InitNode(1, 4, 1);
                    InitStopNode(2, 1);
                    InitNode(2, 3, 2);
                    InitNode(3, 1, 3);
                    InitStopNode(4, 2);
                    InitNode(4, 3, 4);
                    InitStopNode(4, 4);
                    break;

                case 98:
                    myStatic.siwpeC = 13;
                    myStatic.MinimumConut = 5;

                    InitStopNode(0, 1);
                    InitStopNode(0, 2);
                    InitStopNode(0, 4);
                    InitNode(1, 2, 2);
                    InitNode(1, 3, 3);
                    InitNode(2, 2, 4);
                    InitNode(2, 3, 1);
                    InitNode(3, 0, 4);
                    InitStopNode(3, 1);
                    InitStopNode(3, 2);
                    InitStopNode(3, 4);
                    InitStopNode(4, 0);
                    InitStopNode(4, 1);
                    InitStopNode(4, 2);
                    InitStopNode(4, 4);
                    break;

                case 99:
                    myStatic.siwpeC = 22;
                    myStatic.MinimumConut = 14;

                    InitNode(0, 0, 4);
                    InitStopNode(0, 2);
                    InitNode(0, 4, 3);
                    InitStopNode(1, 0);
                    InitNode(1, 4, 1);
                    InitNode(2, 1, 4);
                    InitStopNode(2, 4);
                    InitStopNode(3, 0);
                    InitStopNode(3, 2);
                    InitStopNode(3, 3);
                    InitStopNode(3, 4);
                    InitNode(4, 0, 2);
                    InitStopNode(4, 2);
                    InitStopNode(4, 3);
                    InitStopNode(4, 4);
                    break;

                case 100:
                    myStatic.siwpeC = 19;
                    myStatic.MinimumConut = 11;

                    InitStopNode(0, 0);
                    InitNode(0, 1, 2);
                    InitStopNode(0, 3);
                    InitNode(1, 0, 3);
                    InitNode(1, 3, 4);
                    InitStopNode(2, 1);
                    InitNode(3, 0, 4);
                    InitStopNode(3, 1);
                    InitStopNode(3, 2);
                    InitStopNode(3, 4);
                    InitStopNode(4, 0);
                    InitNode(4, 1, 1);
                    InitStopNode(4, 4);
                    break;

                case 101:
                    myStatic.siwpeC = 15;
                    myStatic.MinimumConut = 8;

                    InitStopNode(0, 1);
                    InitNode(0, 3, 4);
                    InitStopNode(1, 0);
                    InitNode(1, 2, 1);
                    InitStopNode(1, 3);
                    InitStopNode(1, 4);
                    InitStopNode(2, 0);
                    InitStopNode(2, 4);
                    InitStopNode(3, 2);
                    InitNode(3, 3, 4);
                    InitNode(4, 2, 3);
                    InitNode(4, 3, 2);
                    InitStopNode(4, 0);
                    InitStopNode(4, 4);
                    break;

                default:
                    myStatic.stageC = 1;
                    SceneManager.LoadScene("TitleScene");
                    break;

                case 102:
                    myStatic.siwpeC = 21;
                    myStatic.MinimumConut = 14;

                    InitNode(0, 0, 2);
                    InitStopNode(0, 4);
                    InitStopNode(1, 0);
                    InitStopNode(1, 2);
                    InitNode(1, 4, 4);
                    InitStopNode(2, 2);
                    InitStopNode(2, 4);
                    InitStopNode(3, 1);
                    InitStopNode(3, 2);
                    InitNode(3, 3, 3);
                    InitNode(3, 4, 1);
                    InitNode(4, 1, 4);
                    InitStopNode(4, 2);
                    break;

                case 103:
                    myStatic.siwpeC = 19;
                    myStatic.MinimumConut = 13;

                    InitStopNode(0, 0);
                    InitStopNode(0, 2);
                    InitNode(1, 1, 2);
                    InitNode(1, 2, 4);
                    InitStopNode(1, 4);
                    InitStopNode(2, 1);
                    InitNode(2, 4, 4);
                    InitNode(3, 0, 1);
                    InitNode(4, 0, 3);
                    InitStopNode(4, 4);
                    break;

                case 104:
                    myStatic.siwpeC = 16;
                    myStatic.MinimumConut = 9;

                    InitStopNode(0, 0);
                    InitStopNode(0, 1);
                    InitStopNode(1, 3);
                    InitStopNode(1, 4);
                    InitNode(2, 0, 4);
                    InitNode(0, 2, 1);
                    InitStopNode(3, 0);
                    InitNode(3, 2, 2);
                    InitNode(3, 3, 4);
                    InitNode(3, 4, 3);
                    InitStopNode(4, 4);
                    break;
            }
        }
        #endregion

        turn = TURN_STATE.STAY;
		//myStatic.stageC += 1;
		StartCoroutine(UIChange());
	}
	// Use this for initialization
	void Start()
	{
		isRestart = false;
		StartSetting();
		//turn = TURN_STATE.STAY;
		//myStatic.stageC += 1;
		//StartCoroutine(UIChange());
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			test();
		}
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
        //myStatic.stageC -= 1;

		SceneManager.LoadScene("CountDownScene");
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
		IsMove = true;
		if (isClear)
		{
			swipe.text = "Clear";
			isClear = true;
		}
		else if (myStatic.siwpeC <= 0)
		{
			//swipe.text = "No Count!";
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
			if (myStatic.stageC == 0)
			{
				swipe.text = myStatic.siwpeC.ToString();
				StageLevelCount.text = "Tutorial".ToString();//"-LEVEL" + (myStatic.stageC - 1) + "-".ToString();
			}
			else
			{
				swipe.text = myStatic.siwpeC.ToString();
				StageLevelCount.text = "LEVEL " + (myStatic.stageC).ToString();
			}
		}
	}



	void ChangeScene()
	{
		SceneManager.LoadScene(1);
	}

	void test()
	{
		List<List<NodeArray>> temp = new List<List<NodeArray>>();
		//temp.Add(new List<NodeArray>());
		temp = nodeArr;
		int i, j;
		Debug.Log("NodeArr시작");
		for (i = 0; i < 5; i++)
		{
			for (j = 0; j < 5; j++)
			{
				Debug.Log(i + "," + j + ":" + nodeArr[i][j].nodeNumber);
			}
		}
		Debug.Log("NodeArr끝");
		Debug.Log("temp시작");
		for (i = 0; i < 5; i++)
		{
			for (j = 0; j < 5; j++)
			{
				Debug.Log(i + "," + j + ":" + temp[i][j].nodeNumber);
			}
		}

		Debug.Log("temp 끝");

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
				GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Swipe");
				SwipeNodes();
				//turn = TURN_STATE.MOVE;

				//myStatic.SwipeCount += 1;//------------------------------------------------------------------------
				//Debug.Log(myStatic.SwipeCount);
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
					myStatic.SwipeCount += 1;//------------------------------------------------------------------------
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
				if (IsMove)
					StartCoroutine(UIChange());
				break;

		}
	}

	private void SwipeNodes()
	{
		if (nodeCheckArr != null)
			nodeCheckArr.Clear();
		nodeCheckArr = null;


		nodeCheckArr = new List<List<NodeArray>>();
		// nodeCheckArr = new List<List<NodeArray>>();
		for (int i = 0; i < nodeArr.Count; i++)
		{
			nodeCheckArr.Add(new List<NodeArray>());
			for (int j = 0; j < nodeArr[i].Count; j++)
			{


				nodeCheckArr[i].Add(new NodeArray(nodeArr[i][j]));
			}
		}

		#region 스와이프
		if (m_Swipe.SwipeLeft)
		{
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 5; j++)
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
				for (int j = 0; j < 5; j++)
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
		#endregion

		int _count = 0;
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				if (nodeCheckArr[i][j].nodeNumber == nodeArr[i][j].nodeNumber)
				{
					_count++;
				}
			}
		}
		if (_count == 25)
		{
			turn = TURN_STATE.STAY;
			IsMove = false;
			Debug.Log("가만히 있어야 해");
			myStatic.siwpeC += 1;
		}
		else
		{
			IsMove = true;
			turn = TURN_STATE.MOVE;
			Debug.Log("움직여야 해");

		}

		//if (nodeCheckArr == nodeArr)
		//{

		//}
		//else
		//{
		//    IsMove = true;
		//    turn = TURN_STATE.MOVE;
		//    Debug.Log("움직여야 해");

		//}

	}


	private void CompareNode(int y, int x, int dy, int dx)
	{
		if (x + dx < 0 || x + dx > NodeSize - 1)
			return;
		if (y + dy < 0 || y + dy > NodeSize - 1)
			return;// 닿는지 않닿았는지


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
				if (nodeArr[y + dy][x + dx].nodeNumber == nodeArr[y][x].nodeNumber)
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

				#region 원래꺼
				//if (nodeArr[y + dy][x + dx].nodeNumber == 1 && nodeArr[y][x].nodeNumber == 1 && nodeArr[y + dy][x + dx].nodeNumber == nodeArr[y][x].nodeNumber)//숫자블록 2개가 합쳤는데 둘다 1일때---------------------------
				//{
				//    //Debug.Log("1끼리 만났기 때문에 서로 비워줌");
				//    nodeArr[y + dy][x + dx].isNodechange = true;
				//    //nodeArr[y + dy][x + dx].nodeNumber -= 1;
				//    //현재 노드 정보를 다음 노드로 이동
				//    nodeObjList[y][x].GetComponent<NodeState>().TargetPosition = nodeArr[y + dy][x + dx].position;
				//    nodeObjList[y][x].GetComponent<NodeState>().ThisNode = nodeObjList[y + dy][x + dx];
				//    nodeObjList[y + dy][x + dx].GetComponent<NodeState>().TransNode = nodeObjList[y][x];
				//    nodeObjList[y][x] = null;
				//    //현재노드 삭제
				//    nodeArr[y][x].nodeNumber = 0;
				//    nodeArr[y][x].Node_Kind = NODE_KIND.EMPTY;

				//    nodeObjList[y + dy][x + dx] = null;
				//    nodeArr[y + dy][x + dx].nodeNumber = 0;
				//    nodeArr[y + dy][x + dx].Node_Kind = NODE_KIND.EMPTY;
				//}//------------------------------------------------------------------------------------------------------------------------------------------------------------------------
				//else if (nodeArr[y + dy][x + dx].nodeNumber == nodeArr[y][x].nodeNumber)
				//{
				//    //Debug.Log("넘버");
				//    //Debug.Log("같은 숫자끼리 합쳐짐");
				//    GetComponent<AudioSource>().Play();
				//    //합체된 노드 접근방지
				//    nodeArr[y + dy][x + dx].isNodechange = true;
				//    nodeArr[y + dy][x + dx].nodeNumber -= 1;
				//    //현재 노드 정보를 다음 노드로 이동
				//    nodeObjList[y][x].GetComponent<NodeState>().TargetPosition = nodeArr[y + dy][x + dx].position;
				//    nodeObjList[y][x].gameObject.transform.localScale = new Vector3(1.078f, 1.078f, 1.078f);
				//    nodeObjList[y][x].GetComponent<NodeState>().ThisNode = nodeObjList[y + dy][x + dx];
				//    nodeObjList[y + dy][x + dx].GetComponent<NodeState>().TransNode = nodeObjList[y][x];
				//    nodeObjList[y][x] = null;
				//    //현재노드 삭제
				//    nodeArr[y][x].nodeNumber = 0;
				//    nodeArr[y][x].Node_Kind = NODE_KIND.EMPTY;
				//}
				#endregion
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
				//NodeArray temp = new NodeArray();
				////if (nodeArr[i][j].Node_Kind == NODE_KIND.EMPTY || nodeArr[i][j].Node_Kind == NODE_KIND.STOP)//현재 클리어조건이 비어있거나 스톱블록일때
				//if (nodeArr[i][j].Node_Kind == NODE_KIND.EMPTY || nodeArr[i][j].Node_Kind == NODE_KIND.STOP)
				//{

				//	count++;

				//}
				if (nodeArr[i][j].nodeNumber == 0)
					count++;
			}
		}
		if (count == 25)
		{
			//Debug.Log("클리어");
			isClear = true;
			myStatic.isGameClear = true;

		}
	}
	public void FadeinFadeOut_Restart()
	{
		myStatic.isGameClear = true;
		isRestart = true;
		//myStatic.stageC -= 1;
		GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Touch");
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
		myStatic.stageC += 1;
		GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Touch");
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
			Handheld.Vibrate();
			Debug.Log("Doit");
		}
		//myStatic.siwpeC = 0;
		GameObject.Find("FXManager").GetComponent<FXManager>().SoundManager_F("Touch");
		//Debug.Log(myStatic.stageC);
		//myStatic.stageC -= 1; // 방금 이거 뺌	
		//timeSpan += Time.deltaTime;
		//  if(timeSpan>checkTime)

		//SceneManager.LoadScene("CountDownScene");//다시 게임 씬 불러옴
		Result.SetActive(false);
		StartSetting();

	}
}
