using Godot;
using System;

public class FootSteps : AudioStreamPlayer
{

    private float[] parts = {
        0.000f,
        0.511f,
        1.019f,
        1.527f,
        1.936f,
        2.444f
    };

    private Random gen = new Random();

    private float queuedStop = 0;

    public void playRandom()
    {
        if (!Playing || queuedStop == 0)
        {
            int index = gen.Next(0, parts.Length);
            Play(parts[index]);
            if (index == parts.Length - 1)
            {
                queuedStop = Stream.GetLength();
            }
            else
            {
                queuedStop = parts[index + 1];
            }
        }
    }

    public void checkStop()
    {
        var time = GetPlaybackPosition() + AudioServer.GetTimeSinceLastMix();
        if (Playing && time >= queuedStop) {
            Stop();
            queuedStop = 0;
        }
    }
}
