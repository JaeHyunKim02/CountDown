//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;

//public class PlusNode : MonoBehaviour
//{

//    const float MoveSpeed = 50.0f;
//    private NodeState mNodeState;
//    private SpriteRenderer mSpRenderer;

//    public static bool FinishCh;

//    private void Awake()
//    {
//        FinishCh = false;
//    }
//    // Use this for initialization
//    void Start()
//    {
//        FinishCh = false;
//        mNodeState = GetComponent<NodeState>();
//        mSpRenderer = GetComponent<SpriteRenderer>();
//        SpriteChange(mNodeState.NodeNumber);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        //mSpRenderer.sprite = Resources.Load<Sprite>("AnotherNode/StopNode");
//    }

//    public void SpriteChange(int number)
//    {

//        string path = "Sprites/CountDown/temp/temp2" + number.ToString();
//        mSpRenderer.sprite = Resources.Load<Sprite>(path);
//    }

//    public void MoveNode()
//    {

//        //노드 이동
//        transform.position = Vector3.MoveTowards(transform.position, mNodeState.TargetPosition, MoveSpeed * Time.deltaTime);

//        //만약 합체되는 노드가 있다면
//        if (mNodeState.TransNode != null)
//        {
//            //합체되는 노드 이동
//            GameObject tempNode = mNodeState.TransNode;
//            tempNode.transform.position = Vector3.MoveTowards(tempNode.transform.position,
//                tempNode.GetComponent<NodeState>().TargetPosition, MoveSpeed * Time.deltaTime);
//            //합체되는 노드, 현재노드 이동확인
//            if (tempNode.transform.position == (Vector3)tempNode.GetComponent<NodeState>().TargetPosition
//                && transform.position == (Vector3)(mNodeState.TargetPosition))
//            {
//                mNodeState.IsMoveEnd = true;
//                if (mNodeState.NodeNumber != 1)
//                {
//                    SpriteChange(--mNodeState.NodeNumber);
//                    mSpRenderer.transform.localScale = new Vector3(1.2f, 1.2f);
//                    StartCoroutine("DDIYoung");
//                }
//                else
//                {
//                    FinishCh = true;
//                    //myStatic.stageC += 1;
//                    //Debug.Log("드디어 클리어");

//                    //SceneManager.LoadScene(1);
//                }
//                Destroy(mNodeState.TransNode);
//                mNodeState.TransNode = null;
//            }
//        }
//        else
//        {
//            //노드 이동 확인
//            if (transform.position == (Vector3)(mNodeState.TargetPosition))
//            {
//                mNodeState.IsMoveEnd = true;
//            }
//        }
//    }



//    private IEnumerator DDIYoung()
//    {
//        yield return new WaitForSeconds(0.03f);
//        mSpRenderer.transform.localScale = new Vector3(1.0f, 1.0f);
//    }
//}
