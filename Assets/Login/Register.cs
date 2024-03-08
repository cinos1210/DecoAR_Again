using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;
using System.Collections;

public class Register : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Button registerButton;
    public Button loginButton;
    public GameObject notificationPrefab; // El prefab de notificación

    string serverURL = "regional-sociology.gl.at.ply.gg:24198"; // URL donde se ejecuta main.js

    void Start()
    {
        registerButton.onClick.AddListener(RegisterUser);
        loginButton.onClick.AddListener(Login);
    }

    void RegisterUser()
    {
        StartCoroutine(RegisterUserCoroutine(usernameInput.text, passwordInput.text));
    }

    void Login()
    {
        StartCoroutine(LoginCoroutine(usernameInput.text, passwordInput.text));
    }

    IEnumerator RegisterUserCoroutine(string username, string password)
    {
        string jsonData = $"{{\"correo\":\"{username}\",\"password\":\"{password}\",\"cat1\":0,\"cat2\":0,\"cat3\":0}}";

        byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest www = new UnityWebRequest($"{serverURL}/Users", "POST");
        www.uploadHandler = new UploadHandlerRaw(postData);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log("Usuario registrado correctamente");
        }
    }

    IEnumerator LoginCoroutine(string username, string password)
    {
        UnityWebRequest www = UnityWebRequest.Get($"{serverURL}/Users?correo={username}&password={password}");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
        }
        else
        {
            if (www.downloadHandler.text.Contains("Usuario no encontrado")) // Revisa el mensaje de respuesta para determinar si las credenciales son incorrectas
            {
                Debug.Log("Incorrect credentials");
            }
            else
            {
                Debug.Log($"Logging in '{username}'");
                // Mostrar notificación
                ShowNotification("Inicio de sesión correcto");
                // Cargar escena después de un breve retraso
                StartCoroutine(LoadSceneAfterDelay("SampleScene", 2f));
            }
        }
    }

void ShowNotification(string message)
{
    // Instantia el prefab de notificación y configura
    GameObject notification = Instantiate(notificationPrefab, Vector3.zero, Quaternion.identity);
    notification.transform.SetParent(GameObject.Find("Canvas").transform, false);
    
    RectTransform notificationRectTransform = notification.GetComponent<RectTransform>();
    
    // Establece la posición del objeto de notificación en la parte inferior de la pantalla
    notificationRectTransform.anchorMin = new Vector2(0.5f, 0f);
    notificationRectTransform.anchorMax = new Vector2(0.5f, 0f);
    notificationRectTransform.pivot = new Vector2(0.5f, 0f);
    notificationRectTransform.anchoredPosition = new Vector2(0f, 600f); // Ajusta la posición vertical según sea necesario
    
    // Asigna el mensaje proporcionado
    notification.GetComponentInChildren<TextMeshProUGUI>().text = message;
}


    IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
