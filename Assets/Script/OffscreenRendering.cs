using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Attach this script to an empty gameObject. Create a secondary camera 
/// gameObject for offscreen rendering (not your main camera) and connect it 
/// with this script. Offscreen camera should have a texture object attached to it.  
/// OffscreenCamera texture object is used for rendering (please see camera properties). 
/// </summary> 
public class OffscreenRendering : MonoBehaviour
{
	#region public members 	
	/// <summary> 	
	/// The desired number of screenshots per second. 	
	/// </summary> 	
	[Tooltip("Number of screenshots per second.")]
	public int ScreenshotsPerSecond = 19;
	/// <summary> 	
	/// Camera used to render screen to texture. Offscreen camera 	
	/// with desired target texture size should be attached here, 	
	/// not the main camera. 	
	/// </summary> 	
	[Tooltip("The camera that is used for off-screen rendering.")]
	public Camera OffscreenCameraLeft;
	public Camera OffscreenCameraRight;

	//public Camera OffscreenCamera;
	#endregion
	/// <summary> 	
	/// Keep track of saved frames. 	
	/// counter is added as postifx to file names. 	
	/// </summary> 	
	private int FrameCounter = 0;
	//left plane textures
	public RenderTexture leftPlaneLeftTexture;
	public RenderTexture leftPlaneRightTexture;

	public RenderTexture rightPlaneLeftTexture;
	public RenderTexture rightPlaneRightTexture;

	public RenderTexture bottomPlaneLeftTexture;
	public RenderTexture bottomPlaneRightTexture;

	public Vector3 leftWallMax;
	public Vector3 leftWallMin;
	public Vector3 rightWallMax;
	public Vector3 rightWallMin;
	public Vector3 butWallMax;
	public Vector3 butWallMin;
	// Use this for initialization 	
	void Start()
	{
		OffscreenCameraLeft = GameObject.Find("leftCam").GetComponent<Camera>();
		OffscreenCameraRight = GameObject.Find("rightCam").GetComponent<Camera>();

		StartCoroutine("CaptureAndSaveFrames");
	}

	/// <summary>     
	/// Captures x frames per second.      
	/// </summary>     
	/// <returns>Enumerator object</returns>     
	IEnumerator CaptureAndSaveFrames()
	{
		while (true)
		{
			yield return new WaitForEndOfFrame();
			
			//======================================================Left Wall=====================================================================
			
			// ========================== Left Eye Texture==============================
			// Remember currently active render texture. 			
			RenderTexture currentRT = RenderTexture.active;
			// Set target texture as active render texture. 			
			RenderTexture.active = leftPlaneLeftTexture;
			OffscreenCameraLeft.targetTexture = leftPlaneLeftTexture;
			
			//change the projection matrix	
			OffscreenCameraLeft.projectionMatrix = CustomCameraController.instance.getPPrme("left", "lefteye");
			// Render to texture 
			OffscreenCameraLeft.Render();

			// Read offscreen texture
			Debug.Log(leftPlaneLeftTexture.name);			
			Texture2D offscreenTexture = new Texture2D(leftPlaneLeftTexture.width, leftPlaneLeftTexture.height, TextureFormat.RGB24, false);
			offscreenTexture.ReadPixels(new Rect(0, 0, leftPlaneLeftTexture.width, leftPlaneLeftTexture.height), 0, 0, false);
			offscreenTexture.Apply();
			
			// Reset previous render texture. 			
			RenderTexture.active = currentRT;

			// ========================== Right Eye Texture==============================
			// Remember currently active render texture. 			
			currentRT = RenderTexture.active;
			// Set target texture as active render texture. 			
			RenderTexture.active = leftPlaneRightTexture;
			//set the target texture
			OffscreenCameraRight.targetTexture = leftPlaneRightTexture;
			OffscreenCameraRight.projectionMatrix = CustomCameraController.instance.getPPrme("left", "righteye");
			
			// Render to texture 			
			OffscreenCameraRight.Render();
			// Read offscreen texture 			
			offscreenTexture = new Texture2D(leftPlaneRightTexture.width, leftPlaneRightTexture.height, TextureFormat.RGB24, false);
			offscreenTexture.ReadPixels(new Rect(0, 0, leftPlaneRightTexture.width, leftPlaneRightTexture.height), 0, 0, false);
			offscreenTexture.Apply();
			// Reset previous render texture. 			
			RenderTexture.active = currentRT;

			//======================================================Right Wall=====================================================================

			// ========================== Left Eye Texture==============================
			// Remember currently active render texture. 			
			currentRT = RenderTexture.active;
			// Set target texture as active render texture. 			
			RenderTexture.active = rightPlaneLeftTexture;
			OffscreenCameraLeft.targetTexture = rightPlaneLeftTexture;

			//change the projection matrix	
			OffscreenCameraLeft.projectionMatrix = CustomCameraController.instance.getPPrme("right", "lefteye");
			// Render to texture 
			OffscreenCameraLeft.Render();

			// Read offscreen texture 			
			offscreenTexture = new Texture2D(rightPlaneLeftTexture.width, rightPlaneLeftTexture.height, TextureFormat.RGB24, false);
			offscreenTexture.ReadPixels(new Rect(0, 0, rightPlaneLeftTexture.width, rightPlaneLeftTexture.height), 0, 0, false);
			offscreenTexture.Apply();

			// Reset previous render texture. 			
			RenderTexture.active = currentRT;

			// ========================== Right Eye Texture==============================
			// Remember currently active render texture. 			
			currentRT = RenderTexture.active;
			// Set target texture as active render texture. 			
			RenderTexture.active = rightPlaneRightTexture;
			//set the target texture
			OffscreenCameraRight.targetTexture = rightPlaneRightTexture;
			OffscreenCameraRight.projectionMatrix = CustomCameraController.instance.getPPrme("right", "righteye");

			// Render to texture 			
			OffscreenCameraRight.Render();
			// Read offscreen texture 			
			offscreenTexture = new Texture2D(rightPlaneRightTexture.width, rightPlaneRightTexture.height, TextureFormat.RGB24, false);
			offscreenTexture.ReadPixels(new Rect(0, 0, rightPlaneRightTexture.width, rightPlaneRightTexture.height), 0, 0, false);
			offscreenTexture.Apply();
			// Reset previous render texture. 			
			RenderTexture.active = currentRT;

			//======================================================Buttom Wall=====================================================================

			// ========================== Left Eye Texture==============================
			// Remember currently active render texture. 			
			currentRT = RenderTexture.active;
			// Set target texture as active render texture. 			
			RenderTexture.active = bottomPlaneLeftTexture;
			OffscreenCameraLeft.targetTexture = bottomPlaneLeftTexture;

			//change the projection matrix	
			OffscreenCameraLeft.projectionMatrix = CustomCameraController.instance.getPPrme("buttom", "lefteye");
			// Render to texture 
			OffscreenCameraLeft.Render();

			// Read offscreen texture 			
			offscreenTexture = new Texture2D(bottomPlaneLeftTexture.width, bottomPlaneLeftTexture.height, TextureFormat.RGB24, false);
			offscreenTexture.ReadPixels(new Rect(0, 0, bottomPlaneLeftTexture.width, bottomPlaneLeftTexture.height), 0, 0, false);
			offscreenTexture.Apply();

			// Reset previous render texture. 			
			RenderTexture.active = currentRT;

			// ========================== Right Eye Texture==============================
			// Remember currently active render texture. 			
			currentRT = RenderTexture.active;
			// Set target texture as active render texture. 			
			RenderTexture.active = bottomPlaneRightTexture;
			//set the target texture
			OffscreenCameraRight.targetTexture = bottomPlaneRightTexture;
			OffscreenCameraRight.projectionMatrix = CustomCameraController.instance.getPPrme("buttom", "righteye");

			// Render to texture 			
			OffscreenCameraRight.Render();
			// Read offscreen texture 			
			offscreenTexture = new Texture2D(bottomPlaneRightTexture.width, bottomPlaneRightTexture.height, TextureFormat.RGB24, false);
			offscreenTexture.ReadPixels(new Rect(0, 0, bottomPlaneRightTexture.width, bottomPlaneRightTexture.height), 0, 0, false);
			offscreenTexture.Apply();
			// Reset previous render texture. 			
			RenderTexture.active = currentRT;

			++FrameCounter;

			// Encode texture into PNG 			
			//byte[] bytes = offscreenTexture.EncodeToPNG();
			//File.WriteAllBytes(Application.dataPath + "/../capturedframe" + FrameCounter.ToString() + ".png", bytes);

			// Delete textures. 			
			UnityEngine.Object.Destroy(offscreenTexture);

			yield return new WaitForSeconds(1.0f / ScreenshotsPerSecond);
		}
	}

	/// <summary>     
	/// Stop image capture.     
	/// </summary>     
	public void StopCapturing()
	{
		StopCoroutine("CaptureAndSaveFrames");
		FrameCounter = 0;
	}

	/// <summary> 	
	/// Resume image capture. 	
	/// </summary> 	
	public void ResumeCapturing()
	{
		StartCoroutine("CaptureAndSaveFrames");
	}
}