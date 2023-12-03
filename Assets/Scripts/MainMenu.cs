using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
   [SerializeField] private GameObject _changeNamePanel;
   [SerializeField] private Transform _target;
   [SerializeField] private float _speed;
   [SerializeField] private InputField _userNameInputField;
   [SerializeField] private Text _userNameText;
   private void Start()
   {
      ControlUserName();
   }
   private void ControlUserName()
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
            SaveUserId(result);
            _changeNamePanel.SetActive(false);
            print("Register User Id  :  "+result);
         }, () => { }));
      }
   }
   private void SaveUserId(string userId)
   {
      PlayerPrefs.SetString("UserId",userId);
   }
}
