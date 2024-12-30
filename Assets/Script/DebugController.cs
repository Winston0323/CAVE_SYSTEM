using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    // Start is called before the first frame update
    public LineRenderer LWD1;
    public LineRenderer LWD2;
    public LineRenderer LWD3;
    public LineRenderer LWD4;

    public LineRenderer RWD1;
    public LineRenderer RWD2;
    public LineRenderer RWD3;
    public LineRenderer RWD4;

    public LineRenderer BWD1;
    public LineRenderer BWD2;
    public LineRenderer BWD3;
    public LineRenderer BWD4;

    public LineRenderer LRWD1;
    public LineRenderer LRWD2;
    public LineRenderer LRWD3;
    public LineRenderer LRWD4;

    public LineRenderer RRWD1;
    public LineRenderer RRWD2;
    public LineRenderer RRWD3;
    public LineRenderer RRWD4;

    public LineRenderer BRWD1;
    public LineRenderer BRWD2;
    public LineRenderer BRWD3;
    public LineRenderer BRWD4;

    public GameObject leftEye;
    public GameObject rightEye;

    public Vector3 leftWall1;
    public Vector3 leftWall2;
    public Vector3 leftWall3;
    public Vector3 leftWall4;
    public Vector3 rightWall1;
    public Vector3 rightWall2;
    public Vector3 rightWall3;
    public Vector3 rightWall4;
    public Vector3 bottomWall1;
    public Vector3 bottomWall2;
    public Vector3 bottomWall3;
    public Vector3 bottomWall4;
    void Start()
    {
        LWD1.startWidth = 0.003f;
        LWD2.startWidth = 0.003f;
        LWD3.startWidth = 0.003f;
        LWD4.startWidth = 0.003f;
        LWD1.endWidth = 0.003f;
        LWD2.endWidth = 0.003f;
        LWD3.endWidth = 0.003f;
        LWD4.endWidth = 0.003f;

        RWD1.startWidth = 0.003f;
        RWD2.startWidth = 0.003f;
        RWD3.startWidth = 0.003f;
        RWD4.startWidth = 0.003f;
        RWD1.endWidth = 0.003f;
        RWD2.endWidth = 0.003f;
        RWD3.endWidth = 0.003f;
        RWD4.endWidth = 0.003f;

        BWD1.startWidth = 0.003f;
        BWD2.startWidth = 0.003f;
        BWD3.startWidth = 0.003f;
        BWD4.startWidth = 0.003f;
        BWD1.endWidth = 0.003f;
        BWD2.endWidth = 0.003f;
        BWD3.endWidth = 0.003f;
        BWD4.endWidth = 0.003f;

        LRWD1.startWidth = 0.003f;
        LRWD2.startWidth = 0.003f;
        LRWD3.startWidth = 0.003f;
        LRWD4.startWidth = 0.003f;
        LRWD1.endWidth = 0.003f;
        LRWD2.endWidth = 0.003f;
        LRWD3.endWidth = 0.003f;
        LRWD4.endWidth = 0.003f;

        RRWD1.startWidth = 0.003f;
        RRWD2.startWidth = 0.003f;
        RRWD3.startWidth = 0.003f;
        RRWD4.startWidth = 0.003f;
        RRWD1.endWidth = 0.003f;
        RRWD2.endWidth = 0.003f;
        RRWD3.endWidth = 0.003f;
        RRWD4.endWidth = 0.003f;

        BRWD1.startWidth = 0.003f;
        BRWD2.startWidth = 0.003f;
        BRWD3.startWidth = 0.003f;
        BRWD4.startWidth = 0.003f;
        BRWD1.endWidth = 0.003f;
        BRWD2.endWidth = 0.003f;
        BRWD3.endWidth = 0.003f;
        BRWD4.endWidth = 0.003f;

        leftWall1 = new Vector3(1.2f, 0f, 1.2f);
        leftWall2 = new Vector3(1.2f, 2.4f, 1.2f);
        leftWall3 = new Vector3(-1.2f, 0f, 1.2f);
        leftWall4 = new Vector3(-1.2f, 2.4f, 1.2f);

        rightWall1 = new Vector3(1.2f, 2.4f, 1.2f);
        rightWall2 = new Vector3(1.2f, 2.4f, -1.2f);
        rightWall3 = new Vector3(1.2f, 0f, 1.2f);
        rightWall4 = new Vector3(1.2f, 0f, -1.2f);

        bottomWall1 = new Vector3(-1.2f, 0f, 1.2f);
        bottomWall2 = new Vector3(-1.2f, 0f, 1.2f);
        bottomWall3 = new Vector3(-1.2f, 0f, -1.2f);
        bottomWall4 = new Vector3(1.2f, 0f, -1.2f);
        
    }

    // Update is called once per frame
    void Update()
    {
        LWD1.SetPosition(0, leftEye.transform.position);
        LWD2.SetPosition(0, leftEye.transform.position);
        LWD3.SetPosition(0, leftEye.transform.position);
        LWD4.SetPosition(0, leftEye.transform.position);
        LWD1.SetPosition(1, leftWall1);
        LWD2.SetPosition(1, leftWall2);
        LWD3.SetPosition(1, leftWall3);
        LWD4.SetPosition(1, leftWall4);
        
        RWD1.SetPosition(0, leftEye.transform.position);
        RWD2.SetPosition(0, leftEye.transform.position);
        RWD3.SetPosition(0, leftEye.transform.position);
        RWD4.SetPosition(0, leftEye.transform.position);
        RWD1.SetPosition(1, rightWall1);
        RWD2.SetPosition(1, rightWall2);
        RWD3.SetPosition(1, rightWall3);
        RWD4.SetPosition(1, rightWall4);

        BWD1.SetPosition(0, leftEye.transform.position);
        BWD2.SetPosition(0, leftEye.transform.position);
        BWD3.SetPosition(0, leftEye.transform.position);
        BWD4.SetPosition(0, leftEye.transform.position);
        BWD1.SetPosition(1, bottomWall1);
        BWD2.SetPosition(1, bottomWall2);
        BWD3.SetPosition(1, bottomWall3);
        BWD4.SetPosition(1, bottomWall4);

        LRWD1.SetPosition(0, rightEye.transform.position);
        LRWD2.SetPosition(0, rightEye.transform.position);
        LRWD3.SetPosition(0, rightEye.transform.position);
        LRWD4.SetPosition(0, rightEye.transform.position);
        LRWD1.SetPosition(1, leftWall1);
        LRWD2.SetPosition(1, leftWall2);
        LRWD3.SetPosition(1, leftWall3);
        LRWD4.SetPosition(1, leftWall4);

        RRWD1.SetPosition(0, rightEye.transform.position);
        RRWD2.SetPosition(0, rightEye.transform.position);
        RRWD3.SetPosition(0, rightEye.transform.position);
        RRWD4.SetPosition(0, rightEye.transform.position);
        RRWD1.SetPosition(1, rightWall1);
        RRWD2.SetPosition(1, rightWall2);
        RRWD3.SetPosition(1, rightWall3);
        RRWD4.SetPosition(1, rightWall4);

        BRWD1.SetPosition(0, rightEye.transform.position);
        BRWD2.SetPosition(0, rightEye.transform.position);
        BRWD3.SetPosition(0, rightEye.transform.position);
        BRWD4.SetPosition(0, rightEye.transform.position);
        BRWD1.SetPosition(1, bottomWall1);
        BRWD2.SetPosition(1, bottomWall2);
        BRWD3.SetPosition(1, bottomWall3);
        BRWD4.SetPosition(1, bottomWall4);

    }
}
