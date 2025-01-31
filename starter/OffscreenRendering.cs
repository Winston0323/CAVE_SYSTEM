using System.Collections; using System.Collections.Generic;
using System.IO; using UnityEngine;

/// <summary>
/// Attach this script to an empty gameObject. Create a secondary camera 
/// gameObject for offscreen rendering (not your main camera) and connect it 
/// with this script. Offscreen camera should have a texture object attached to it.  
/// OffscreenCamera texture object is used for rendering (please see camera properties). 
/// </summary> 
public class OffscreenRendering : MonoBehaviour {  	
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
	public Camera OffscreenCamera; 	
	#endregion  	
	/// <summary> 	
	/// Keep track of saved frames. 	
	/// counter is added as postifx to file names. 	
	/// </summary> 	private int FrameCounter = 0;  	
	
	// Use this for initialization 	
	void Start () { 		
		StartCoroutine("CaptureAndSaveFrames"); 	
	}      
	
	/// <summary>     
	/// Captures x frames per second.      
	/// </summary>     
	/// <returns>Enumerator object</returns>     
	IEnumerator CaptureAndSaveFrames() {  		 
	while (true) { 			
		yield return new WaitForEndOfFrame();  			
		// Remember currently active render texture. 			
		RenderTexture currentRT = RenderTexture.active;  			
		// Set target texture as active render texture. 			
		RenderTexture.active = OffscreenCamera.targetTexture;  			
		// Render to texture 			
		OffscreenCamera.Render();  			
		// Read offscreen texture 			
		Texture2D offscreenTexture = new Texture2D(OffscreenCamera.targetTexture.width, OffscreenCamera.targetTexture.height, TextureFormat.RGB24, false); 			
		offscreenTexture.ReadPixels(new Rect(0, 0, OffscreenCamera.targetTexture.width, OffscreenCamera.targetTexture.height), 0, 0, false); 			
		offscreenTexture.Apply();  			
		// Reset previous render texture. 			
		RenderTexture.active = currentRT;  			
		++FrameCounter; 			
		
		// Encode texture into PNG 			
		byte[] bytes = offscreenTexture.EncodeToPNG(); 				
		File.WriteAllBytes(Application.dataPath + "/../capturedframe"+FrameCounter.ToString()+".png", bytes);  	
		
		// Delete textures. 			
		UnityEngine.Object.Destroy(offscreenTexture);              
		
		yield return new WaitForSeconds(1.0f / ScreenshotsPerSecond);         
		} 	
	}  	
	
	/// <summary>     
	/// Stop image capture.     
	/// </summary>     
	public void StopCapturing (){         
		StopCoroutine("CaptureAndSaveFrames");         
		FrameCounter = 0;     
	} 	
	
	/// <summary> 	
	/// Resume image capture. 	
	/// </summary> 	
	public void ResumeCapturing () { 		
		StartCoroutine("CaptureAndSaveFrames"); 	
	} 
}