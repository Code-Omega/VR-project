using UnityEngine;
using System.Collections;

public class color_change : MonoBehaviour {
	Vector3 lastPos;
	// Use this for initialization
	void Start () {
		
	}
	
	Color TransformHSV(
		float H,          // hue shift (in degrees)
		float S,          // saturation multiplier (scalar)
		float V           // value multiplier (scalar)
		)
	{
		float VSU = V*S*Mathf.Cos((H*Mathf.PI/180));
		float VSW = V*S*Mathf.Sin((H*Mathf.PI/180));
		
		Color ret = Color.black;
		ret.r = (float)((.299*V+.701*VSU+.168*VSW)
		                + (.587*V-.587*VSU+.330*VSW)
		                + (.114*V-.114*VSU-.497*VSW));
		ret.g = (float)((.299*V-.299*VSU-.328*VSW)
		                + (.587*V+.413*VSU+.035*VSW)
		                + (.114*V-.114*VSU+.292*VSW));
		ret.b = (float)((.299*V-.3*VSU+1.25*VSW)
		                + (.587*V-.588*VSU-1.05*VSW)
		                + (.114*V+.886*VSU-.203*VSW));
		return ret;
	}
	
	// Update is called once per frame
	void Update (){
		GameObject controller = GameObject.Find ("OVRPlayerController");
		Vector3 newPos = controller.transform.position;
		Vector3 horizontalVelocity = new Vector3(newPos.x-lastPos.x, 0, newPos.y-lastPos.y);
		float horizontalSpeed = horizontalVelocity.magnitude;
		//float verticalSpeed = controller.velocity.y;
		//float overallSpeed = controller.velocity.magnitude;
		Debug.Log(controller.transform.position);
		Debug.Log(horizontalSpeed);
		Debug.Log(transform.renderer.material.color);
		transform.renderer.material.color = new Color(horizontalSpeed*100%255,10,10);
		lastPos = newPos;
	}
	
	
}
