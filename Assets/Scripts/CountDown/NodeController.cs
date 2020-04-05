using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NodeController : MonoBehaviour
{
	GameObject Particle;
	const float MoveSpeed = 50.0f;
	private NodeState mNodeState;
	private SpriteRenderer mSpRenderer;
	private int NodeSize = 5;


	public static bool FinishCh;

	void Start()
	{
		Particle = Resources.Load<GameObject>("Prefabs/Flash2");

		FinishCh = false;
		mNodeState = GetComponent<NodeState>();
		mSpRenderer = GetComponent<SpriteRenderer>();
		if (mNodeState.NodeNumber > 0)
			SpriteChange(mNodeState.NodeNumber);
		//Debug.Log(mNodeState.NodeNumber);//필드에 생성된 블록들의 NodeNumber확인
	}

	// Update is called once per frame
	void Update()
	{
		//if (Input.GetKeyDown(KeyCode.Space))//필드의 값을 확인하기 위한 조건
		//{
		//    Debug.Log(mNodeState.NodeNumber);
		//    Debug.Log(mNodeState.node_kind);
		//}
		//mSpRenderer.sprite = Resources.Load<Sprite>("AnotherNode/StopNode");
		GetState();
		//if (myStatic.isGameClear)
		//{
		//    Debug.Log("asdf");

		//    Destroy(gameObject);
		//    Destroy(this);
		//}
	}

	public void SpriteChange(int number)
	{
		//string path = "Sprites/CountDown/temp/" + number.ToString();
		string path = "InGameImage/block/" + number.ToString();
		mSpRenderer.sprite = Resources.Load<Sprite>(path);
	}
	public void Test()
	{
		transform.position = Vector3.MoveTowards(transform.position, mNodeState.TargetPosition, MoveSpeed * Time.deltaTime);

		if (mNodeState.TransNode != null)
		{
			GameObject tempNode = mNodeState.TransNode;

			tempNode.transform.position = Vector3.MoveTowards(tempNode.transform.position, tempNode.GetComponent<NodeState>().TargetPosition, MoveSpeed * Time.deltaTime);

			if (tempNode.transform.position == (Vector3)tempNode.GetComponent<NodeState>().TargetPosition
				&& transform.position == (Vector3)(mNodeState.TargetPosition))
			{

			}
			mNodeState.IsMoveEnd = true;
		}
	}
	public void MoveNode()
	{
		//노드 이동
		transform.position = Vector3.MoveTowards(transform.position, mNodeState.TargetPosition, MoveSpeed * Time.deltaTime);

		//만약 합체되는 노드가 있다면
		if (mNodeState.TransNode != null)
		{
			//합체되는 노드 이동
			GameObject tempNode = mNodeState.TransNode;
			//Debug.Log(tempNode.transform.position);
			//Debug.Log("몇번?");
			tempNode.transform.position = Vector3.MoveTowards(tempNode.transform.position,
				tempNode.GetComponent<NodeState>().TargetPosition, MoveSpeed * Time.deltaTime);
			//합체되는 노드, 현재노드 이동확인
			if (tempNode.transform.position == (Vector3)tempNode.GetComponent<NodeState>().TargetPosition
				&& transform.position == (Vector3)(mNodeState.TargetPosition))
			{
				mNodeState.IsMoveEnd = true;


				#region 아직은 생각하지 말자

				if (tempNode.gameObject.tag == "Plus")
				{
					//Debug.Log("주체 - 플러스");
					//Debug.Log("플러스에서 바뀐거:" + mNodeState.NodeNumber);
					gameObject.tag = "Untagged";//태그를 바꿔서 블럭의 기능을 없앰
					SpriteChange(++mNodeState.NodeNumber);
					//Debug.Log("플러스에서 바뀐거:" + mNodeState.NodeNumber);

					StartCoroutine("DDIYoung");
				}
				else if (gameObject.gameObject.tag == "Plus")
				{
					//Debug.Log("주체 - 숫자");
					//Debug.Log("플러스에서 바뀐거:" + mNodeState.NodeNumber);
					gameObject.tag = "Untagged";//태그를 바꿔서 블럭의 기능을 없앰
					mNodeState.NodeNumber += tempNode.GetComponent<NodeState>().NodeNumber + 1;
					SpriteChange(mNodeState.NodeNumber);
					//Debug.Log("플러스에서 바뀐거:" + mNodeState.NodeNumber);
					StartCoroutine("DDIYoung");
				}
				else if (tempNode.gameObject.tag == "Minus")
				{
					//Debug.Log("주체 - 마이너스");
					//Debug.Log("마이너스에서 바뀐거:" + mNodeState.NodeNumber);
					gameObject.tag = "Untagged";//태그를 바꿔서 블럭의 기능을 없앰
					SpriteChange(--mNodeState.NodeNumber);
					//Debug.Log("마이너스에서 바뀐거:" + mNodeState.NodeNumber);
					StartCoroutine("DDIYoung");
				}
				else if (gameObject.gameObject.tag == "Minus")
				{
					//Debug.Log("주체 - 숫자");
					//Debug.Log("마이너스에서 바뀐거:" + mNodeState.NodeNumber);
					gameObject.tag = "Untagged";//태그를 바꿔서 블럭의 기능을 없앰
					mNodeState.NodeNumber += tempNode.GetComponent<NodeState>().NodeNumber - 1;
					SpriteChange(mNodeState.NodeNumber);
					//Debug.Log("마이너스에서 바뀐거:" + mNodeState.NodeNumber);
					StartCoroutine("DDIYoung");
				}
				else if (tempNode.gameObject.tag == "Multiple")
				{
					//Debug.Log("주체 - 곱하기");
					//Debug.Log("플러스에서 바뀐거:" + mNodeState.NodeNumber);
					gameObject.tag = "Untagged";//태그를 바꿔서 블럭의 기능을 없앰
					SpriteChange(mNodeState.NodeNumber * 2);
					//Debug.Log("플러스에서 바뀐거:" + mNodeState.NodeNumber);
					StartCoroutine("DDIYoung");
				}
				else if (gameObject.gameObject.tag == "Multiple")
				{
					//Debug.Log("주체 - 숫자");
					//Debug.Log("플러스에서 바뀐거:" + mNodeState.NodeNumber);
					gameObject.tag = "Untagged";//태그를 바꿔서 블럭의 기능을 없앰
					mNodeState.NodeNumber += tempNode.GetComponent<NodeState>().NodeNumber * 2;
					SpriteChange(mNodeState.NodeNumber);
					//Debug.Log("플러스에서 바뀐거:" + mNodeState.NodeNumber);
					StartCoroutine("DDIYoung");
				}

				#endregion


				//    if (mNodeState.NodeNumber > 1)
				//    {
				//        Debug.Log("??");
				//        SpriteChange(--mNodeState.NodeNumber);
				//        StartCoroutine("DDIYoung");

				//    }
				//    else
				//    {
				//        mNodeState.NodeNumber = 0;
				//        FinishCh = true;
				//        StartCoroutine("DDIYoung");
				//        Debug.Log("클리어");
				//        Destroy(mNodeState.gameObject);
				//        //Destroy(gameObject);
				//        //myStatic.stageC += 1;
				//        //Debug.Log("드디어 클리어");

				//        //SceneManager.LoadScene(1);
				//    }

				//    Destroy(mNodeState.TransNode);
				//    mNodeState.TransNode = null;
				//    Debug.Log("노드 컨트롤러 들어옴");

				//}

				if (mNodeState.NodeNumber > 1)
				{
					SpriteChange(--mNodeState.NodeNumber);
					mSpRenderer.transform.localScale = new Vector3(1.2f, 1.2f);
					StartCoroutine("DDIYoung");
				}
				else
				{
					mNodeState.NodeNumber = 0;
					Debug.Log("is0");
					//Debug.Log("NodeNumber is not 1 : " + mNodeState.NodeNumber);
					//var newBlock = Instantiate(Particle, new Vector3(mNodeState.transform.position.x, mNodeState.transform.position.y, -1.5f), Quaternion.identity);
					StartCoroutine("DDIYoung");
					//FinishCh = true;
				}
				Destroy(mNodeState.TransNode);
				mNodeState.TransNode = null;
			}
		}
		else
		{
			//노드 이동 확인
			//Debug.Log("이동만 한듯");
			if (transform.position == (Vector3)(mNodeState.TargetPosition))
			{
				mNodeState.IsMoveEnd = true;
				if (mNodeState.NodeNumber <= 0 && mNodeState.tag == "Untagged")
				{
					Debug.Log("1따리");
					// Destroy(mNodeState.gameObject);
				}
			}
		}
	}

	private IEnumerator DDIYoung()
	{
		mSpRenderer.transform.localScale = new Vector3(1.2f, 1.2f);

		yield return new WaitForSeconds(0.08f);

		mSpRenderer.transform.localScale = new Vector3(1.0f, 1.0f);
	}

	public void StateUpdate()
	{

	}
	public void GetState()
	{
		//string path = "Sprites/CountDown/temp/" + number.ToString();
		if (mNodeState.NodeNumber >= 1)
		{
			mNodeState.node_kind = NODE_KIND.NUMBER;
		}
		else if (mNodeState.NodeNumber <= 0)
		{
			mNodeState.node_kind = NODE_KIND.EMPTY;//문제의 코드
		}

		if (mNodeState.node_kind == NODE_KIND.EMPTY)
		{
			//Destroy(mNodeState.TransNode);
			Destroy(mNodeState.ThisNode);
			Destroy(this);
			Destroy(gameObject);
		}
	}
}