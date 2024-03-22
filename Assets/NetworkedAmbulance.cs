using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;
using Ubiq.Geometry;
using UnityEngine.XR.Interaction.Toolkit;


public class NetworkedAmbulance : MonoBehaviour
{
    NetworkContext context;
    PositionRotation lastSentPose;

    void Start()
    {
        context = NetworkScene.Register(this);
    }

    void Update()
    {
        // Only the owner should send updates
        var currentPose = Transforms.ToLocal(transform, context.Scene.transform);
        if (!currentPose.Equals(lastSentPose))
        {
            var message = new Message();
            message.pose = currentPose;
            context.SendJson(message);
            lastSentPose = currentPose;
        }
            
    }

    private struct Message
    {
        public PositionRotation pose;
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var m = message.FromJson<Message>();
        var pose = Transforms.ToWorld(m.pose, context.Scene.transform);
        transform.position = pose.position;
        transform.rotation = pose.rotation;
    }
}