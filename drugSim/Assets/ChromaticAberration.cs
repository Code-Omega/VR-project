using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class ChromaticAberration : MonoBehaviour {
	
	#region Variables
	public Shader curShader;
	public float ChromaticAbberationRate = 1.0f;
	float ChromaticAbberation;
	Vector3 lastPos;
	private Material curMaterial;
	#endregion
	
	#region Properties
	Material material
	{
		get
		{
			if(curMaterial == null)
			{
				curMaterial = new Material(curShader);
				curMaterial.hideFlags = HideFlags.HideAndDontSave;	
			}
			return curMaterial;
		}
	}
	#endregion
	// Use this for initialization
	void Start () 
	{
		if(!SystemInfo.supportsImageEffects)
		{
			enabled = false;
			return;
		}
		ChromaticAbberation = 1.0f;
	}
	
	void OnRenderImage (RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if(curShader != null)
		{
			material.SetFloat("_AberrationOffset", ChromaticAbberation);
			//material.SetBool("_OffsetDirectionPositive", OVRPlayerController.forwardMovement);
			//material.set("_OffsetDirectionNegative", OVRPlayerController.backwardMovement);
			Graphics.Blit(sourceTexture, destTexture, material);
		}
		else
		{
			Graphics.Blit(sourceTexture, destTexture);	
		}
		
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		GameObject controller = GameObject.Find ("OVRPlayerController");
		Vector3 newPos = controller.transform.position;
		Vector3 horizontalVelocity = new Vector3(newPos.x-lastPos.x, 0, newPos.y-lastPos.y);
		float horizontalSpeed = horizontalVelocity.magnitude/Time.deltaTime;

		ChromaticAbberation = 1.0f * ChromaticAbberationRate * horizontalSpeed;
		if (ChromaticAbberation > 30)
						ChromaticAbberation = 30;

		if (OVRPlayerController.backwardMovement)
						ChromaticAbberation *= -1.0f;

		Debug.Log(controller.transform.position);
		Debug.Log("speed: "+horizontalSpeed);
		Debug.Log("ab:                                     "+ChromaticAbberation);

		lastPos = newPos;
	}
	
	void OnDisable ()
	{
		if(curMaterial)
		{
			DestroyImmediate(curMaterial);	
		}
		
	}
	
	
}