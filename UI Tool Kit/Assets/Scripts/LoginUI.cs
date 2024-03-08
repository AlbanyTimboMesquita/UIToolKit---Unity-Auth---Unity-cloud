using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private UIDocument uIDocument;
    private Button loginButton ,registerButton,sendButton,getButton,deleteButton;
    private TextField userNameField,passwordField,inputTextField;
    private Label errorLabel;
    
private void Awake() {
    var root = uIDocument.rootVisualElement;
    
    userNameField = root.Q<TextField>("emailTextField");
    passwordField = root.Q<TextField>("passwordTextField");
    inputTextField = root.Q<TextField>("inputTextField");

    loginButton = root.Q<Button>("loginButton");
    registerButton = root.Q<Button>("registerButton");
    sendButton = root.Q<Button>("sendButton");
    getButton = root.Q<Button>("getButton");
    deleteButton = root.Q<Button>("deleteButton");

    errorLabel=root.Q<Label>("errorLabel");

    registerButton.clicked +=CadastrarButtonClick;
    loginButton.clicked +=LoginButtonClick;
    sendButton.clicked += EnviarDados;
    getButton.clicked += RecuperarDados;
    deleteButton.clicked += DeletarDados;
}

    private async void DeletarDados()
    {
       var errorText= await CloudSaveScript.Instance.DeleteKey();
       errorLabel.text="Cloud:"+errorText;
    }

    private async void RecuperarDados()
    {
       try
        {
            var errorText=await CloudSaveScript.Instance.LoadData();
            errorLabel.text="Cloud:"+errorText;
        }
        finally
        {
            
        }
    }

    private async void EnviarDados()
    {
          try
        {
            var errorText=await CloudSaveScript.Instance.SaveData(inputTextField.value);
            errorLabel.text=errorText;
        }
        finally
        {
            
        }
        
    }

    private async void CadastrarButtonClick()
    {
        SetButtonEnabled(false);
        
        try
        {
            var errorText = await AuthenticationManager.Instance.CadastroAsync(userNameField.value,passwordField.value);
            errorLabel.text=errorText;  
        }
        finally
        {
            SetButtonEnabled(true);
            
        }
        
    }

    private async void LoginButtonClick()
    {
        SetButtonEnabled(false);
        try
        {
            var errorText = await AuthenticationManager.Instance.LoginAsync(userNameField.value,passwordField.value);
            errorLabel.text=errorText;
        }
        finally
        {
           SetButtonEnabled(true);
        }
        
    }

    private void SetButtonEnabled(bool status)
    {
        registerButton.SetEnabled(status);
        loginButton.SetEnabled(status);
    }
}
