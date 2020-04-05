using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeState : MonoBehaviour {




    [SerializeField]
    private Vector2 nodePosition;
    [SerializeField]
    private Vector2 targetPosition;
    [SerializeField]
    private GameObject transNode;
    [SerializeField]
    private bool isMoveEnd;
    [SerializeField]
    private int nodeNumber;
    public NODE_KIND node_kind;
    [SerializeField]
    private GameObject thisNode;//자기 자신



    #region GETSET
    public Vector2 NodePosition
    {
        get
        {
            return nodePosition;
        }

        set
        {
            nodePosition = value;
        }
    }

    public Vector2 TargetPosition
    {
        get
        {
            return targetPosition;
        }

        set
        {
            targetPosition = value;
        }
    }

    public GameObject TransNode
    {
        get
        {
            return transNode;
        }

        set
        {
            transNode = value;
        }
    }

    public bool IsMoveEnd
    {
        get
        {
            return isMoveEnd;
        }

        set
        {
            isMoveEnd = value;
        }
    }

    public int NodeNumber
    {
        get
        {
            return nodeNumber;
        }

        set
        {
            nodeNumber = value;
        }
    }
    public GameObject ThisNode
    {
        get
        {
            return thisNode;
        }
        set
        {
            thisNode = value;
        }
    }

    #endregion

    // Use this for initialization
    void Start () {

        transNode = null;
        isMoveEnd = false;
        

    }
	
	// Update is called once per frame
	void Update()
    {

	}
}
