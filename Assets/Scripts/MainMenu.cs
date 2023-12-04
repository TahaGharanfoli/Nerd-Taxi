using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
   [SerializeField] private GameObject _changeNamePanel;
   [SerializeField] private Transform _target;
   [SerializeField] private float _speed;
   [SerializeField] private TMP_InputField _userNameInputField;
   [SerializeField] private Text _userNameText;
   [SerializeField] private LeaderBoardRow _leaderBoardRowPrefab;
   [SerializeField] private Transform _leaderBoardRowParent;
   [SerializeField] private GameObject _leaderBoardPage;
   private List<GameObject> _leaderBoardRowList = new List<GameObject>();

   private void Start()
   {
      _userNameText.text = $"{GetUserName()}";
      ControlUserRegister();
   }
   private void ControlUserRegister()
   {
      if (!PlayerPrefs.HasKey("UserId"))
      {
         _changeNamePanel.SetActive(true);
      }
      else
      {
         _changeNamePanel.SetActive(false);
      }
   }
   private void Update()
   {
      _target.Rotate(0,10*_speed*Time.deltaTime,0);
   }
   public void OnClickPlay()
   {
      SceneManager.LoadScene("GameScene");
   }

   public void OnClickChangeUserName()
   {
      if (_userNameInputField.text.Length > 3)
      {
         if (PlayerPrefs.HasKey("UserId"))
         {
            StartCoroutine(NetworkManager.UpdateUserName(GetUserId(), _userNameInputField.text,() =>
            {
               _userNameText.text = _userNameInputField.text;
               SaveUserName(_userNameInputField.text);
               _changeNamePanel.SetActive(false);
            }, null));
         }
         else
         {

            StartCoroutine(NetworkManager.Register(_userNameInputField.text, (result) =>
            {
               _userNameText.text = _userNameInputField.text;
               JObject tempData = JObject.Parse(result);
               string userId = tempData["userId"].ToString();
               SaveUserId(userId);
               SaveUserName(_userNameText.text);
               _changeNamePanel.SetActive(false);
               print("Register User Id  :  " + userId);
            }, () => { }));
         }
      }
   }

   private void SaveUserId(string userId)
   {
      PlayerPrefs.SetString("UserId",userId);
   }

   private string GetUserId()
   {
      return PlayerPrefs.GetString("UserId");
   }

   private string GetUserName()
   {
      if (!PlayerPrefs.HasKey("UserName"))
      {
         return "guest";
      }

      return PlayerPrefs.GetString("UserName");
   }
   private void SaveUserName(string userName)
   {
      PlayerPrefs.SetString("UserName",userName);
   }
   public void OnClickOpenLeaderBoard()
   {
      _leaderBoardPage.SetActive(true);
      StartCoroutine(NetworkManager.GetLeaderBoard((result) =>
      {
         List<LeaderBoardRowData> leaderBoardRowList = JsonConvert.DeserializeObject<List<LeaderBoardRowData>>(result);
         CreateLeaderBoardRow(leaderBoardRowList);
         
      },null));
   }
   private void CreateLeaderBoardRow(List<LeaderBoardRowData> leaderBoardList)
   {
      if (_leaderBoardRowList.Count>0)
      {
         int count = leaderBoardList.Count;
         for (int i = 0; i < count; i++)
         {
            Destroy(_leaderBoardRowList[i].gameObject);
         }
         _leaderBoardRowList.Clear();
      }
      GameObject tempRow ;
      int index = 1;
      foreach (var item in leaderBoardList)
      {
         tempRow=Instantiate(_leaderBoardRowPrefab.gameObject, _leaderBoardRowParent);
         tempRow.GetComponent<LeaderBoardRow>().Init(item.username,index,item.score);
         _leaderBoardRowList.Add(tempRow);
         index++;
      }
   }

   public class LeaderBoardRowData
   {
      public string id;
      public string username;
      public int score;
   }
}
