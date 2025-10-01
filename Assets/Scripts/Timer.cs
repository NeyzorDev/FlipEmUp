using System;
using UnityEngine;

public class Timer
{
	private float time;
	private float timer;
	private bool isPlaying;

	private Action Timeout;


	public Timer(float t, Action to)
	{
		time = t;
		isPlaying = false;
		Timeout += to;
	}

	public void Play()
	{
		timer = 0.0f;
		isPlaying = true;
    }

	public void Update()
	{
		if (isPlaying) 
		{ 
			timer += Time.deltaTime;

            if (timer > time)
			{
				isPlaying = false;
				Timeout.Invoke();
			}
        }
	}
}