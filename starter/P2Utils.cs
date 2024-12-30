using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Utils : MonoBehaviour
{
  public enum RenderingMode { Stereo, Mono, LeftOnly, RightOnly };
  public Camera leftEye;
  public Camera rightEye;
  public GameObject leftParent;
  public GameObject rightParent;
  public static P2Utils instance;

  RenderingMode renderingMode;
  Vector3 leftPosStart;
  Vector3 rightPosStart;
  float iod;

    // Start is called before the first frame update
    void Start()
    {
        renderingMode = RenderingMode.Stereo;
        leftPosStart = leftParent.transform.position;
        rightPosStart = rightParent.transform.position;
        iod = 0.065f;
        if (instance == null)
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (renderingMode == RenderingMode.Mono)
        {
            var lv = leftEye.GetStereoViewMatrix(Camera.StereoscopicEye.Left);
            var lp = leftEye.GetStereoProjectionMatrix(Camera.StereoscopicEye.Left);
            rightEye.SetStereoViewMatrix(Camera.StereoscopicEye.Right, lv);
            rightEye.SetStereoProjectionMatrix(Camera.StereoscopicEye.Right, lp);
        }

        if (iod != 0.065f)
        {
            var leftPos = UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.LeftEye);
            var rightPos = UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.RightEye);
            var direction = Vector3.Normalize(leftPos - rightPos);
            
            leftParent.transform.localPosition = leftPosStart + direction * (iod - 0.065f) * 17.5f;
            rightParent.transform.localPosition = rightPosStart - direction * (iod - 0.065f) * 17.5f;
        }
    }

    public void changeRenderingMode(P2Utils.RenderingMode mode)
    {
        renderingMode = mode;
        switch (renderingMode)
        {
        default:
        case RenderingMode.Stereo:
            Shader.SetGlobalInt("_RenderingMode", 0);
            rightEye.ResetStereoViewMatrices();
            rightEye.ResetStereoProjectionMatrices();
            break;
        case RenderingMode.Mono:
            Shader.SetGlobalInt("_RenderingMode", 1);
            break;
        case RenderingMode.LeftOnly:
            Shader.SetGlobalInt("_RenderingMode", 2);
            rightEye.ResetStereoViewMatrices();
            rightEye.ResetStereoProjectionMatrices();
            break;
        case RenderingMode.RightOnly:
            Shader.SetGlobalInt("_RenderingMode", 3);
            rightEye.ResetStereoViewMatrices();
            rightEye.ResetStereoProjectionMatrices();
            break;
        }
    }

    public void disableTracking(bool enabled)
    {
        UnityEngine.XR.XRDevice.DisableAutoXRCameraTracking(leftEye, enabled);
        UnityEngine.XR.XRDevice.DisableAutoXRCameraTracking(rightEye, enabled);
    }

    public void setIODDistance(float distance)
    {
        iod = distance;
        if (iod == 0.065f)
            resetEyeParents();
    }

    void resetEyeParents()
    {
        leftParent.transform.localPosition = leftPosStart;
        rightParent.transform.localPosition = rightPosStart;
    }
}
