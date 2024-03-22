// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Ubiq.Messaging;
// using Ubiq.Geometry;
// using UnityEngine.XR.Interaction.Toolkit;
// using UnityEngine.UI;

// public class MyNetworkedButton : MonoBehaviour
// {
//     NetworkContext context;
//     private bool buttonClicked = false; 
//     private Button.ButtonClickedEvent m_OnClick;

//     // Start is called before the first frame update
//     void Start()
//     {
//         context = NetworkScene.Register(this);
        
//         var button = GetComponent<Button>();
//         button.onClick.AddListener(ButtonClicked);
//         m_OnClick = button.onClick;
//     }

//     // Update is called once per frame
//     void ButtonClicked()
//     {
//         var message = new Message();
//         // message.buttonClicked = true;
//         message.buttonClickedEvent = m_OnClick;
//         context.SendJson(message);

        
//     }
//     private struct Message
//     {
//         public Button.ButtonClickedEvent buttonClickedEvent;
//     }

//     public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
//     {
//         var m = message.FromJson<Message>();

//         if (m.buttonClickedEvent != null)
//         {
//             // Perform actions upon receiving the onClick event
//             Debug.Log("Button Clicked Event Received!");

//             // Invoke the received onClick event
//             m.buttonClickedEvent.Invoke();
//         }
//     }
// }