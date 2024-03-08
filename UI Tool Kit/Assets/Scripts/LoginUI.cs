using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private UIDocument uIDocument;
    private Button loginButton ,registerButton;
    
private void Awake() {
    var root =uIDocument.rootVisualElement;
    loginButton = root.Q<Button>("loginButton");
    loginButton.clicked +=()=> Debug.Log("Botao login pressionado");

    registerButton = root.Q<Button>("registerButton");
    registerButton.clicked +=()=> Debug.Log("Botao Cadastrar pressionado");
}
}
