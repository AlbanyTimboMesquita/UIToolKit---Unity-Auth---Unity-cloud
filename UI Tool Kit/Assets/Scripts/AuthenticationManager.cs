using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using System.Threading.Tasks;
using Unity.Services.Authentication;

public class AuthenticationManager : MonoBehaviour
{
    public static AuthenticationManager Instance{get;private set;}
    private void Awake() =>Instance=this;
    async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            SetupEvents();
        }
        catch (AuthenticationException e)
        {
            
            Debug.LogException(e);
        }
        catch (RequestFailedException e)
        {
            
            Debug.LogException(e);
        }
        Debug.Log($"Unity service state: {UnityServices.State}");

 
    }
    public async Task<string> CadastroAsync(string nome,string senha){
        
        try
        {
          await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(nome,senha);  
        }
        catch (AuthenticationException e)
        {
            if(e.ErrorCode==AuthenticationErrorCodes.AccountAlreadyLinked){
                return "Este email já está cadastrado.";

            }else{
            Debug.LogException(e);
            return e.Message;
            }
        }catch (RequestFailedException e)
        {
            Debug.LogException(e);
            return e.Message;
        }
        return "";
    }

    public async Task<string> LoginAsync(string nome,string senha){
        
        try
        {
          await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(nome,senha);  
        }
        catch (AuthenticationException e)
        {
            Debug.LogException(e);
            return e.Message;
        }
        catch (RequestFailedException e)
        {
            Debug.LogException(e);
            return e.Message;
        }
        return "";
    }
    private static void SetupEvents(){
        AuthenticationService.Instance.SignedIn +=()=>{
            Debug.Log($"Id jogador:{AuthenticationService.Instance.PlayerId}");
            Debug.Log($"Token:{AuthenticationService.Instance.AccessToken}");
            Debug.Log($"Id jogador:{AuthenticationService.Instance.PlayerName}");
        };

        AuthenticationService.Instance.SignedOut +=()=>{
            Debug.Log("Jogador deslogou");
        };
    }

}
