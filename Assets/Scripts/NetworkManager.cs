 using System;
 using System.Collections;
 using Newtonsoft.Json.Linq;
 using UnityEngine;
 using UnityEngine.Networking;

 public static class NetworkManager
 {
     private static string _url = "";
     public static IEnumerator MakeRequest(string methodName)
     {
         using (UnityWebRequest webRequest = UnityWebRequest.Get(_url))
         {
             yield return webRequest.SendWebRequest();
             if (webRequest.result != UnityWebRequest.Result.Success)
             {
                 Debug.LogError("Error: " + webRequest.error);
             }
             else
             {
                 Debug.Log("Response: " + webRequest.downloadHandler.text);
             }
         }
     }
     public static  IEnumerator Register(string userName,Action<string> onSuccess,Action onFail)
     {
         var webRequest = new UnityWebRequest(_url, "POST");
         byte[] bytes = new System.Text.UTF8Encoding().GetBytes(userName);
         webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bytes);
         webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
         webRequest.SetRequestHeader("Content-Type", "application/json");
         //Send the request then wait here until it returns
         yield return webRequest.SendWebRequest();
         if (webRequest.isNetworkError)
         {
             Debug.Log("Error While Sending: " + webRequest.error);
             yield return "Error";
         }
         else
         {
             Debug.Log("Received: " + webRequest.downloadHandler.text); 
             onSuccess.Invoke(webRequest.downloadHandler.text);
             yield return webRequest.downloadHandler.text;
         }
     } 
 }
