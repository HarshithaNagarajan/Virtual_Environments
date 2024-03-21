using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;
using Ubiq.Geometry;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class MyNetworkedButton : MonoBehaviour
{
    NetworkContext context;

    // Start is called before the first frame update
    void Start()
    {
        context = NetworkScene.Register(this);
        
        var button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClicked);
    }

    // Update is called once per frame
    void ButtonClicked()
    {
    
        var message = new Message();
        message.buttonClicked = true;
        context.SendJson(message);

        
    }
    private struct Message
    {
        public bool buttonClicked;
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var m = message.FromJson<Message>();

    }
}