// Copyright (c) 2014-2015, THUNDERBEAST GAMES LLC
// Licensed under the MIT license, see LICENSE for details


using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;

namespace JSONExporter
{

public class JEKeyframe
{
	public Vector3 pos = new Vector3(0, 0, 0);
	public Vector3 scale = new Vector3(1, 1, 1);
	public Quaternion rot = Quaternion.identity;
	public float time = 0.0f;

}

public class JEAnimationClip
{
	public string name;
	public Dictionary<string, List<JEKeyframe>> keyframes = new Dictionary<string, List<JEKeyframe>>();
}


public class JEAnimation : JEComponent
{

    override public void Preprocess()
    {
    	// m_LocalScale.x - z
    	// m_LocalPosition.x - z
    	// m_LocalRotation.x - w

    	// for now copy the keys, we could evaluate and maybe should
    	// as Unity keys have in/out tangents

        unityAnim = unityComponent as JSONAnimationHelper;

        // no way to get clips like this, going to need a custom component
        // for now, support one animation
        //for (int i = 0; i < unityAnim.GetClipCount(); i++)
        //{
        //	var clip = unityAnim.clips[i];
        //}

        for (int i = 0; i < unityAnim.AnimationClips.Length; i++)
		{
			AddClip(unityAnim.AnimationClips[i]);
		}

    }

    void AddClip(AnimationClip clip)
    {

    	JEAnimationClip aclip = new JEAnimationClip();
    	aclip.name = clip.name;
    	clips.Add(aclip);

        AnimationClipCurveData[] curveData = AnimationUtility.GetAllCurves(clip, true);

        Dictionary<string, List<AnimationClipCurveData>> animdata = new  Dictionary<string, List<AnimationClipCurveData>> ();

        for (int i = 0; i < curveData.Length; i++)
        {
        	AnimationClipCurveData cd = curveData[i];

        	List<AnimationClipCurveData> nodedata;

        	if (!animdata.TryGetValue(cd.path, out nodedata))
        		nodedata = animdata[cd.path] = new List<AnimationClipCurveData>();

			nodedata.Add(cd);
        }

        foreach(KeyValuePair<string, List<AnimationClipCurveData>> entry in animdata)
        {
        	var boneName = entry.Key;

        	var keyframes = aclip.keyframes[boneName] = new List<JEKeyframe>();

        	float maxTime = 0;

        	foreach (AnimationClipCurveData cd in entry.Value)
        	{
        		var curve = cd.curve;

        		 if (curve.keys.Length == 0)
        		 	continue;

        		 if (curve.keys[curve.keys.Length-1].time > maxTime)
        		 	maxTime = curve.keys[curve.keys.Length-1].time;

        	}

			Vector3 pos = new Vector3(0, 0, 0);
			Vector3 scale = new Vector3(1, 1, 1);
			Quaternion rot = Quaternion.identity;

			Vector3 lastpos = new Vector3(0, 0, 0);
			Vector3 lastscale = new Vector3(1, 1, 1);
			Quaternion lastrot = Quaternion.identity;

        	for (float time = 0.0f; time <= maxTime;)
        	{
	        	foreach (AnimationClipCurveData cd in entry.Value)
	        	{
	        		var curve = cd.curve;

	        		float value = curve.Evaluate(time);

	        		if (cd.propertyName == "m_LocalScale.x")
	        		{
	        			scale.x = value;
	        		}
	        		else if (cd.propertyName == "m_LocalScale.y")
	        		{
	        			scale.y = value;
	        		}
	        		else if (cd.propertyName == "m_LocalScale.z")
	        		{
	        			scale.z = value;
	        		}
	        		else if (cd.propertyName == "m_LocalPosition.x")
	        		{
	        			pos.x = value;
	        		}
	        		else if (cd.propertyName == "m_LocalPosition.y")
	        		{
	        			pos.y = value;
	        		}
	        		else if (cd.propertyName == "m_LocalPosition.z")
	        		{
	        			pos.z = value;
	        		}
	        		else if (cd.propertyName == "m_LocalRotation.x")
	        		{
	        			rot.x = value;
	        		}
	        		else if (cd.propertyName == "m_LocalRotation.y")
	        		{
	        			rot.y = value;
	        		}
	        		else if (cd.propertyName == "m_LocalRotation.z")
	        		{
	        			rot.z = value;
	        		}
	        		else if (cd.propertyName == "m_LocalRotation.w")
	        		{
	        			rot.w = value;
	        		}
	        		else
	        			Debug.Log("Unknown Animtation: " + cd.propertyName);

		        }

		        bool needFrame = false;
		        float t;
		        t = Math.Abs(lastpos.x - pos.x);
		        if (t < .01f)
		        	needFrame = true;
		        t = Math.Abs(lastpos.y - pos.y);
		        if (t < .01f)
		        	needFrame = true;
		        t = Math.Abs(lastpos.z - pos.z);
		        if (t < .01f)
		        	needFrame = true;
		        t = Math.Abs(lastscale.x - scale.x);
		        if (t < .01f)
		        	needFrame = true;
		        t = Math.Abs(lastscale.y - scale.y);
		        if (t < .01f)
		        	needFrame = true;
		        t = Math.Abs(lastscale.z - scale.z);
		        if (t < .01f)
		        	needFrame = true;
		        t = Math.Abs(lastrot.x - rot.x);
		        if (t < .01f)
		        	needFrame = true;
		        t = Math.Abs(lastrot.y - rot.y);
		        if (t < .01f)
		        	needFrame = true;
		        t = Math.Abs(lastrot.z - rot.z);
		        if (t < .01f)
		        	needFrame = true;
		        t = Math.Abs(lastrot.w - rot.w);
		        if (t < .01f)
		        	needFrame = true;

		        if (needFrame)
		        {
		        	lastpos = pos;
		        	lastrot = rot;
		        	lastscale = scale;

		        	JEKeyframe keyframe = new JEKeyframe();
		        	keyframe.pos = pos;
		        	keyframe.rot = rot;
		        	keyframe.scale = scale;
		        	keyframe.time = time;

		        	keyframes.Add(keyframe);

		        }

        		time += .025f;
        	}

        }

    }

    new public static void Reset()
    {

    }

    public override JSONComponent ToJSON()
    {
    	var json = new JSONAnimation();
    	json.type = "Animation";

    	json.clips = new JSONAnimationClip[clips.Count];

    	for (int i = 0; i < clips.Count; i++)
    	{
    		JEAnimationClip clip = clips[i];

    		var jclip = json.clips[i] = new JSONAnimationClip();

    		jclip.name = clip.name;

    		JSONAnimationNode[] jnodes = jclip.nodes = new JSONAnimationNode[clip.keyframes.Count];

    		int count = 0;
			foreach (KeyValuePair<string, List<JEKeyframe>> entry in clip.keyframes)
			{
				JSONAnimationNode node = new JSONAnimationNode();
				node.name = entry.Key;
				node.keyframes = new JSONKeyframe[entry.Value.Count];

				int kcount = 0;
				foreach (var key in entry.Value)
				{
					var jkeyframe = new JSONKeyframe();

					jkeyframe.pos = key.pos;
					jkeyframe.scale = key.scale;
					jkeyframe.rot = key.rot;

					jkeyframe.time = key.time;

					node.keyframes[kcount++] = jkeyframe;
				}

				jnodes[count++] = node;

			}

    	}

        return json;
    }

    JSONAnimationHelper unityAnim;
    List<JEAnimationClip> clips = new List<JEAnimationClip>();

}

}
