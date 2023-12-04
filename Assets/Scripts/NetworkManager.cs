 using System;
 using System.Collections;
 using Newtonsoft.Json.Linq;
 using UnityEngine;
 using UnityEngine.Networking;

 public static class NetworkManager
 {
     private static string _url = "http://188.121.117.218:1010/api/nerdtaxi";
      // private static string _url = "http://game.irantata.ir:1010/api/nerdtaxi";

      public static IEnumerator Test(string url)
      {
          JObject json = new JObject();
          json.Add("username", "Ali");
          var webRequest = new UnityWebRequest(url, "POST");
          byte[] bytes = new System.Text.UTF8Encoding().GetBytes(json.ToString());
          webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bytes);
          webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
          webRequest.SetRequestHeader("Content-Type", "application/json");
          //Send the request then wait here until it returns
          yield return webRequest.SendWebRequest();
          if (webRequest.isNetworkError)
          {
              // onFail?.Invoke();
              Debug.Log("Error While Sending: " + webRequest.error);
              yield return "Error";
          }
          else
          {
              Debug.Log("Received: " + webRequest.downloadHandler.text);
              // onSuccess?.Invoke(webRequest.downloadHandler.text);
              yield return webRequest.downloadHandler.text;
          }
      }
      public static IEnumerator Register(string userName, Action<string> onSuccess, Action onFail)
     {
         JObject json = new JObject();
         json.Add("username", userName);
         Debug.Log("######   :  " + json["username"]);
         var webRequest = new UnityWebRequest(_url + "/createUser", "POST");
         byte[] bytes = new System.Text.UTF8Encoding().GetBytes(json.ToString());
         webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bytes);
         webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
         webRequest.SetRequestHeader("Content-Type", "application/json");
         //Send the request then wait here until it returns
         yield return webRequest.SendWebRequest();
         if (webRequest.isNetworkError)
         {
             onFail?.Invoke();
             Debug.Log("Error While Sending: " + webRequest.error);
             yield return "Error";
         }
         else
         {
             Debug.Log("Received: " + webRequest.downloadHandler.text);
             onSuccess?.Invoke(webRequest.downloadHandler.text);
             yield return webRequest.downloadHandler.text;
         }
     }

      public static IEnumerator UpdateUserName(string userId, string userName, Action onSuccess, Action onFail)
      {
          UnityWebRequest webRequest = UnityWebRequest.Put(_url + "/updateUsername", "");

          // Set the request headers (optional)
          webRequest.SetRequestHeader("Content-Type", "application/json");

          // Create JSON data to send in the request body
         
          JObject jsonData = new JObject();
          jsonData.Add("userId", userId);
          jsonData.Add("username", userName);
          // Convert the string data to bytes
          byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData.ToString());

          // Set the request body
          webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
          webRequest.downloadHandler = new DownloadHandlerBuffer();

          // Send the request and wait for a response
          yield return webRequest.SendWebRequest();

          // Check for errors
          if (webRequest.result != UnityWebRequest.Result.Success)
          {
              Debug.LogError("Error: " + webRequest.error);
          }
          else
          {
              // Print the response
              onSuccess?.Invoke();
              Debug.Log("Response: " + webRequest.downloadHandler.text);
          }
      }

     public static IEnumerator UpdateScore(string userId, int userScore, Action onSuccess, Action onFail)
     {
         UnityWebRequest webRequest = UnityWebRequest.Put(_url + "/updateUserScore", "");

         // Set the request headers (optional)
         webRequest.SetRequestHeader("Content-Type", "application/json");

         // Create JSON data to send in the request body
         
         JObject jsonData = new JObject();
         jsonData.Add("userId", userId);
         jsonData.Add("score", userScore);
         // Convert the string data to bytes
         byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData.ToString());

         // Set the request body
         webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
         webRequest.downloadHandler = new DownloadHandlerBuffer();

         // Send the request and wait for a response
         yield return webRequest.SendWebRequest();

         // Check for errors
         if (webRequest.result != UnityWebRequest.Result.Success)
         {
             Debug.LogError("Error: " + webRequest.error);
         }
         else
         {
             // Print the response
             Debug.Log("Response: " + webRequest.downloadHandler.text);
         }
     }
     
     public static  IEnumerator GetLeaderBoard(Action<string> onSuccess,Action onFail)
     {
         using (UnityWebRequest webRequest = UnityWebRequest.Get(_url+"/get/leaderBoard"))
         {
             yield return webRequest.SendWebRequest();
             if (webRequest.result != UnityWebRequest.Result.Success)
             {
                 onFail?.Invoke();
                 Debug.LogError("Error: " + webRequest.error);
             }
             else
             {
                 onSuccess?.Invoke(webRequest.downloadHandler.text);
                 Debug.Log("Response: " + webRequest.downloadHandler.text);
             }
         }
     } 
     public static  IEnumerator GetUserNameById(string userName,Action<string> onSuccess,Action onFail)
     {
         using (UnityWebRequest webRequest = UnityWebRequest.Get(_url+""))
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
 }
