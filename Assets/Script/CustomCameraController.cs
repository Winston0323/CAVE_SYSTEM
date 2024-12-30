using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCameraController : MonoBehaviour
{
    // Start is called before the first frame update
    //camera rig and custom cam rig
    public GameObject leftEyeAnchor;
    public GameObject rightEyeAnchor;

    public GameObject rightHandAnchor;
    
    public GameObject leftHandEyeAnchor;
    public GameObject rightHandEyeAnchor;
    public GameObject HandCam;

    public GameObject currLeftEyeAnchor;
    public GameObject currRightEyeAnchor;

    public Vector3 lastLeftEye;
    public Vector3 lastRightEye;
    public bool freeze;
    public Vector3 eyePos;
    public bool mount;
    public bool debug;

    public GameObject leftParent;
    public GameObject rightParent;

    public GameObject leftDebug;
    public GameObject rightDebug;

    //walls
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject butWall;

    public Vector3 wallMax;
    public Vector3 wallMin;

    public Vector3 pa;
    public Vector3 pb;
    public Vector3 pc;

    public float l;
    public float r;
    public float b;
    public float t;



    public float iod;

    public static CustomCameraController instance;

    void Start()
    {
        freeze = false;
        Unmount();
        disableDebug();
        if (instance == null)
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        iod = Vector3.Distance(leftHandEyeAnchor.transform.position, rightEyeAnchor.transform.position);
        if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) > 0.9)
        {
            Mount();
            if (OVRInput.GetDown(OVRInput.RawButton.A)) {
                if (debug)
                {
                    debug = false;
                    disableDebug();
                }
                else {
                    debug = true;
                    enableDebug();
                }
            }
        }
        else if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) < 0.3)
        {
            Unmount();
            debug = false;
            disableDebug();
        }
        if (OVRInput.GetDown(OVRInput.RawButton.B))
        {
            if (freeze == false)
            {
                froze();
            }
            else {
                unfroze();
            }
 
        }
        leftParent.transform.localPosition = leftEyeAnchor.transform.localPosition;
        rightParent.transform.localPosition = rightEyeAnchor.transform.localPosition;
        HandCam.transform.position = rightHandAnchor.transform.position;
    }
    public Matrix4x4 getPPrme(string wall , string eye) {

        if (eye == "lefteye")
        {
            if (freeze)
            {
                eyePos = lastLeftEye;
            }
            else {
                eyePos = currLeftEyeAnchor.transform.position;
            }
            
        }
        else if (eye == "righteye")
        {
            if (freeze)
            {
                eyePos = lastRightEye;
            }
            else
            {
                eyePos = currRightEyeAnchor.transform.position;
            }
        }
        else {
            Debug.Log("eye string is wrong");
            eyePos = new Vector3(0, 0, 0);
        }


        if (wall == "left")
        {
            pa = new Vector3(-1.2f, 0f, 1.2f);
            pb = new Vector3(1.2f, 0f, 1.2f);
            pc = new Vector3(-1.2f, 2.4f, 1.2f);
        }
        else if (wall == "right")
        {
            pa = new Vector3(1.2f, 0f, 1.2f);
            pb = new Vector3(1.2f, 0f, -1.2f);
            pc = new Vector3(1.2f, 2.4f, 1.2f);
        }
        else if (wall == "buttom")
        {
            pa = new Vector3(-1.2f, 0f, -1.2f);
            pb = new Vector3(1.2f, 0f, -1.2f);
            pc = new Vector3(-1.2f, 0f, 1.2f);
        }
        else {

            Debug.Log("wall string is wrong");
            pa = new Vector3(0f, 0f, 0f);
            pb = new Vector3(0f, 0f, 0f);
            pc = new Vector3(0f, 0f, 0f);
        }

        //calculate each vector
        Vector3 va = pa - eyePos;
        Vector3 vb = pb - eyePos;
        Vector3 vc = pc - eyePos;

        Vector3 vr = (pb - pa) / (pb - pa).magnitude;
        Vector3 vu = (pc - pa) / (pc - pa).magnitude;
        Vector3 vn = Vector3.Cross(vr, vu) / Vector3.Cross(vr, vu).magnitude;
        float d = -Vector3.Dot(vn, va);

         l = Vector3.Dot(vr, va) * 0.3f / d; 
         r = Vector3.Dot(vr, vb) * 0.3f / d; 
         b = Vector3.Dot(vu, va) * 0.3f / d; 
         t = Vector3.Dot(vu, vc) * 0.3f / d;

        Matrix4x4 P = Matrix4x4.Frustum( l, r, b, t, 0.3f, 1000f);
        Matrix4x4 Mtrans = new Matrix4x4(new Vector4(-vr.x, -vu.x, -vn.x, 0),
                                        new Vector4(-vr.y, -vu.y, -vn.y, 0),
                                        new Vector4(vr.z, vu.z, vn.z, 0),
                                        new Vector4(0f, 0f, 0f, 1f));
        Matrix4x4 T = new Matrix4x4(new Vector4(1f, 0f, 0f, 0f),
                                    new Vector4(0f, 1f, 0f, 0f),
                                    new Vector4(0f, 0f, 1f, 0f),
                                    new Vector4(-eyePos.x, -eyePos.y, -eyePos.z, 1));
        return P * Mtrans * T;
    }

    void Mount()
    {
        currLeftEyeAnchor = leftHandEyeAnchor;
        currRightEyeAnchor = rightHandEyeAnchor;
        if (mount == false) {
            lastLeftEye = currLeftEyeAnchor.transform.position;
            lastRightEye = currRightEyeAnchor.transform.position;
        }
        mount = true;
    }
    void Unmount()
    {
        currLeftEyeAnchor = leftEyeAnchor;
        currRightEyeAnchor = rightEyeAnchor;
        if (mount == true)
        {
            lastLeftEye = currLeftEyeAnchor.transform.position;
            lastRightEye = currRightEyeAnchor.transform.position;
        }
        mount = false;
    }
    void froze() {
       
        lastLeftEye = currLeftEyeAnchor.transform.position;
        lastRightEye = currRightEyeAnchor.transform.position;
        freeze = true;
    }
    void unfroze() {
        freeze = false;
    }
    void disableDebug() {
        leftDebug.SetActive(false);
        rightDebug.SetActive(false);
    }
    void enableDebug()
    {
        leftDebug.SetActive(true);
        rightDebug.SetActive(true);
    }
}
