using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class APIService
{
    private string url = "http://3.134.105.55:8999";
    private string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyIjp7InJvbGUiOiJVU0VSIiwiYWN0aXZlIjp0cnVlLCJfaWQiOiI1ZGYxYjhlZmI5NzU3MjAwMWYyZGM0NTEiLCJuYW1lIjoiRXplcXVpZWwgQ2FtZXp6YW5hIiwiZW1haWwiOiJlemVxdWllbGNhbWV6emFuYUBnbWFpbC5jb20iLCJjcmVhdGVkX2F0IjoiMjAxOS0xMi0xMlQwMzo1MDowNy41MDlaIiwidXBkYXRlZEF0IjoiMjAxOS0xMi0xMlQwMzo1MDowNy41MDlaIiwiX192IjowfSwiaWF0IjoxNTc2NTU0NzY3LCJleHAiOjE1NzY1NTgzNjd9.B329KykHzzPcmJhfe1BCXp_hEK5-G66rNfqXH0iVBM0";

    private static APIService instance = null;
    public static APIService Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            return new APIService();
        }
    }
    public APIService()
    {

    }

    public IEnumerator GetRandomVirus<T>(Action<T> callback, Action error = null)
    {
        return GetRequest("/v1/public/virus/random", callback, error);
    }

    //public IEnumerator GetChunk(string chunkId, Action<GetChunksDataModel> callback, Action error = null)
    //{
    //    return GetRequest("/chunks" + chunkId, callback, error);
    //}

    //public IEnumerator GetChunkByQuery(string query, Action<GetChunksDataModel> callback, Action error = null)
    //{
    //    return GetRequest("/chunks" + query, callback, error);
    //}

    //public IEnumerator UploadImage(byte[] image, Action<UploadImageDataModel> callback, Action error = null)
    //{
    //    WWWForm form = new WWWForm();
    //    form.AddBinaryData("image", image);
    //    return PostRequest("/images", form, callback, error);
    //}

    IEnumerator PostRequest<T>(string path, WWWForm form, Action<T> callback, Action error)
    {
        UnityWebRequest uwr = UnityWebRequest.Post(this.url + path, form);
        uwr.SetRequestHeader("Authorization", token);
        yield return uwr.SendWebRequest();

        if (uwr.responseCode == 0 || uwr.responseCode >= 400)
        {
            Debug.LogError("API Error (Code: " + uwr.responseCode + "): " + uwr.error);
            error?.Invoke();
        }
        else
        {
            Debug.Log("API Success: (Code: " + uwr.responseCode + "): " + uwr.downloadHandler.text);
            callback(JsonUtility.FromJson<T>(uwr.downloadHandler.text));
        }
    }

    private IEnumerator PostRequest<T>(string path, string json, Action<T> callback, Action error)
    {
        var uwr = new UnityWebRequest(this.url + path, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        uwr.SetRequestHeader("Authorization", token);

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.responseCode == 0 || uwr.responseCode >= 400)
        {
            Debug.LogError("API Error (Code: " + uwr.responseCode + "): " + uwr.error);
            error?.Invoke();
        }
        else
        {
            Debug.Log("API Success: (Code: " + uwr.responseCode + "): " + uwr.downloadHandler.text);
            callback(JsonUtility.FromJson<T>(uwr.downloadHandler.text));
        }
    }

    private IEnumerator GetRequest<T>(string path, Action<T> callback, Action error)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(this.url + path);
        uwr.SetRequestHeader("Content-Type", "application/json");
        //uwr.SetRequestHeader("Authorization", token);
        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.responseCode == 0 || uwr.responseCode >= 400)
        {
            Debug.LogError("API Error (Code: " + uwr.responseCode + "): " + uwr.error);
            if (error != null) { error(); }
        }
        else
        {
            Debug.Log("API Success: (Code: " + uwr.responseCode + "): " + uwr.downloadHandler.text);
            if (callback != null) { callback(JsonUtility.FromJson<T>(uwr.downloadHandler.text)); };
        }
    }
}