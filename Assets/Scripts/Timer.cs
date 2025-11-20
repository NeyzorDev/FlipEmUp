using System;
using UnityEngine;

public class Timer
{
	private float time;
	private float timer;
	private bool isPlaying;

	private Action Timeout;


    public Timer(float t)
    {
        time = t;
        isPlaying = false;
    }

    public Timer(float t, Action to) : this(t)
    {
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

	public bool IsPlaying()
	{
		return isPlaying;
	}
}