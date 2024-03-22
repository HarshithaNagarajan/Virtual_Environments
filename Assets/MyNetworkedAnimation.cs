using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;
using Ubiq.Geometry;
using UnityEngine.XR.Interaction.Toolkit;

public class MyNetworkedAnimation : MonoBehaviour
{
    NetworkContext context;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        context = NetworkScene.Register(this);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var message = new Message();
        // message.animationState = animator.GetCurrentAnimatorStateInfo(0); // Get current animation state
        context.SendJson(message);
    }

    private struct Message
    {
        public AnimatorStateInfo animationState;
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var m = message.FromJson<Message>();

        // Set received animation state
        animator.Play(m.animationState.fullPathHash, 0, m.animationState.normalizedTime);
    }
}
