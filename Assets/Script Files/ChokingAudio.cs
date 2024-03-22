// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Ubiq.Messaging;
// public class ChokingAudio : MonoBehaviour

// {
//     AudioSource audioSource;
//     Animator AvatarChoking;
//     NetworkContext context;
//     private bool isAudioPlaying = false;
//     // public TriggerScript instance; 
//     // Start is called before the first frame update
//     void Start()
//     {
//         context = NetworkScene.Register(this);
//         AvatarChoking = GetComponent<Animator>();
//         // epipenAnimator = GameObject.FindGameObjectWithTag("Epipen").GetComponent<Animator>();
//         audioSource = GetComponent<AudioSource>();
         

//     }

//     // Update is called once per frame
//     void Update()
//     {

//         if (!TriggerScript.instance.hasTriggered){
//             if (AvatarChoking.GetCurrentAnimatorStateInfo(0).IsTag("LayingDown")){
//                 // Debug.Log("Playing Coughing Sound");
//                 isAudioPlaying = true;
//                 audioSource.Play();
//             } 
//         } else {
//             if (isAudioPlaying){
//                 audioSource.Stop();
//                 isAudioPlaying = false;
//             }
//         }  
//         // if (!TriggerScript.instance.hasTriggered){
//         //     Debug.Log("Epipen not triggered");
//         //     // audioSource.Play();
//         //     isAudioPlaying = true;
//         // } else {
//         //     Debug.Log("Epipen was triggered");
//         //     // audioSource.Stop();
//         //     isAudioPlaying = false;
//         // }
        
        
//     }

//         void SendNetworkMessage()
//     {
//         var message = new Message();
//         message.isAudioPlaying = isAudioPlaying;
//         context.SendJson(message);
//     }

//     private struct Message
//     {
//         public bool isAudioPlaying;
//     }

//     public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
//     {
//         var m = message.FromJson<Message>();
//         // Update audio state based on the received message
//         if (m.isAudioPlaying)
//         {
//             audioSource.Play();
//         }
//         else
//         {
//             audioSource.Stop();
//         }
//     }
// }
