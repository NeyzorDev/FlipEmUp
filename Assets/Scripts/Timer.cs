using System;
using UnityEngine;

public class Timer
{
	private float time;
	private float timer;
	private bool isPlaying;

	public delegate void TimeoutHandler();
	private TimeoutHandler Timeout;

	public Timer(float t, TimeoutHandler to)
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