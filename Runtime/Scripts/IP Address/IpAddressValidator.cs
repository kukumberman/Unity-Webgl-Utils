using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using Newtonsoft.Json.Linq;

public sealed class IpAddressValidator : MonoBehaviour
{
    public UnityEvent OnValidatedSuccessfully;
    public UnityEvent OnValidationFailed;

    public bool RunOnStart;
    public bool AllowOnProtocolError;
    public bool AllowOnConnectionError;

    public List<string> BlackListedCountries;
    public List<string> DebugBlackListedCountries;

    private void Awake()
    {
        if (Debug.isDebugBuild)
        {
            BlackListedCountries.AddRange(DebugBlackListedCountries);
        }
    }

    private void Start()
    {
        if (RunOnStart)
        {
            Run();
        }
    }

    public void Run()
    {
        StartCoroutine(DoRequest());
    }

    private IEnumerator DoRequest()
    {
        // Dima: will not work in the browser due to its restrictions "http://ip-api.com/json"
        var url = Encoding.UTF8.GetString(
            Convert.FromBase64String("aHR0cDovL2lwLWFwaS5jb20vanNvbg==")
        );

        // "https://api.country.is/"
        url = Encoding.UTF8.GetString(Convert.FromBase64String("aHR0cHM6Ly9hcGkuY291bnRyeS5pcy8="));

        using (var request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                HandleRequestOk(request);
            }
            else
            {
                HandleRequestError(request);
            }
        }
    }

    private void HandleRequestOk(UnityWebRequest request)
    {
        var json = request.downloadHandler.text;

        if (Debug.isDebugBuild)
        {
            Debug.Log(json);
        }

        var allowed = Validate(json);

        if (allowed)
        {
            Debug.LogWarning("OnValidatedSuccessfully");
            OnValidatedSuccessfully.Invoke();
        }
        else
        {
            Debug.LogWarning("OnValidationFailed");
            OnValidationFailed.Invoke();
        }
    }

    private void HandleRequestError(UnityWebRequest request)
    {
        Debug.Log(request.result);
        Debug.Log(request.error);
        Debug.Log(request.downloadHandler.text);

        if (request.result == UnityWebRequest.Result.ProtocolError && AllowOnProtocolError)
        {
            OnValidatedSuccessfully.Invoke();
            return;
        }

        if (request.result == UnityWebRequest.Result.ConnectionError && AllowOnConnectionError)
        {
            OnValidatedSuccessfully.Invoke();
            return;
        }

        OnValidationFailed.Invoke();
    }

    private bool Validate(string json)
    {
        var jsonObject = JObject.Parse(json);

        if (!jsonObject.TryGetValue("country", out var countryToken))
        {
            return false;
        }

        var countryName = countryToken.ToObject<string>();

        if (BlackListedCountries.Contains(countryName))
        {
            return false;
        }

        return true;
    }
}
