using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeConnection : MonoBehaviour
{
	public float distanceFromChainEnd = 0.6f;

	public void ConnectRopeEnd(Rigidbody2D endRB)
	{
		HingeJoint2D joint = gameObject.AddComponent<HingeJoint2D>();
		joint.autoConfigureConnectedAnchor = false;
		joint.connectedBody = endRB;
		joint.anchor = Vector2.zero;
		
		JointAngleLimits2D tmp = joint.limits;
		tmp.min = 60;
		tmp.max = -60;
		joint.limits = tmp;
		joint.useLimits = true;

		joint.connectedAnchor = new Vector2(0f, -distanceFromChainEnd);
	}

	public void ConnectRopeStart(Rigidbody2D startRB)
	{
		HingeJoint2D joint = gameObject.AddComponent<HingeJoint2D>();
		joint.autoConfigureConnectedAnchor = false;
		joint.connectedBody = startRB;
		joint.anchor = Vector2.zero;
		joint.connectedAnchor = new Vector2(0f, distanceFromChainEnd);
	}
}
