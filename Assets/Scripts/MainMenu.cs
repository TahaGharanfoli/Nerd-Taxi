using System;
using System.Collections;
using System.Collections.Generic;
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
         StartCoroutine(NetworkManager.Register(_userNameInputField.text, (result) =>
         {
            _userNameText.text = _userNameInputField.text; 
            JObject tempData=JObject.Parse(result);
            string userId = tempData["userId"].ToString();
            SaveUserId(userId);
            SaveUserName(_userNameText.text);
            _changeNamePanel.SetActive(false);
            print("Register User Id  :  "+userId);
         }, () => { }));
      }
   }
   private void SaveUserId(string userId)
   {
      PlayerPrefs.SetString("UserId",userId);
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
      // init data
   }

   private void CreateLeaderBoardRow()
   {
      var tempRow = Instantiate(_leaderBoardRowPrefab, _leaderBoardRowParent);
   }
}
