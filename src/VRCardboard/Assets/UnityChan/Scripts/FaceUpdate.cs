﻿using UnityEngine;
using System.Collections;

public class FaceUpdate : MonoBehaviour
{
	public AnimationClip[] animations;
	public bool gui;
	Animator anim;

	public float delayWeight;

	void Start ()
	{
		anim = GetComponent<Animator> ();
	}

	void OnGUI ()
	{
		if (gui) {
			foreach (var animation in animations) {
				if (GUILayout.Button (animation.name)) {
					anim.CrossFade (animation.name, 0);
				}
			}
		}
	}

	float current = 0;


	void Update ()
	{

		if (Input.GetMouseButton (0)) {
			current = 1;
		} else {
			current = Mathf.Lerp (current, 0, delayWeight);
		}
		anim.SetLayerWeight (1, current);
	}
}
