using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.CloudSave;
using System.Threading.Tasks;
using System;
using Unity.VisualScripting;


public class CloudSaveScript : MonoBehaviour
{
    public static CloudSaveScript Instance{get;private set;}
    private void Awake() =>Instance=this;
    public async void Start()
    {
        await UnityServices.InitializeAsync();
    }
    public async Task<string> SaveData(string dados){
        try
        {
           var data = new Dictionary<string,object>{{"firstdata",dados}};
            await CloudSaveService.Instance.Data.Player.SaveAsync(data); 
        }
        catch (System.Exception e)
        {
            
            return e.Message;
        }
        return "Objeto do tipo 'Mensagem' enviado com sucesso";
    }
     public async Task<string> LoadData(){
        Dictionary<string,string> serverData= await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string>{"firstdata"});
        return serverData["firstdata"];
     }

     public async Task<string> DeleteKey(){
        try
        {
          await CloudSaveService.Instance.Data.ForceDeleteAsync("firstdata");  
        }
        catch (System.Exception e)
        {
            
            return e.Message;
        }
        
        return "Objeto excluido com sucesso!";
     }
}
