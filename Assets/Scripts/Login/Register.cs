using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using System.Text.RegularExpressions;

public class Register : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Button registerButton;
    public Button loginButton;
    public GameObject notificationPrefab;

    string serverURL = "regional-sociology.gl.at.ply.gg:24198";

    void Start()
    {
        registerButton.onClick.AddListener(RegisterUser);
        loginButton.onClick.AddListener(Login);
    }

    void RegisterUser()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        // Validar si el correo electrónico es válido
        if (!IsValidEmail(username))
        {
            ShowNotification("Por favor, ingrese un correo electrónico válido.");
            return;
        }

        // Validar la contraseña
        if (!IsPasswordSecure(password))
        {
            ShowNotification("La contraseña debe tener al menos 8 caracteres y contener al menos una letra mayúscula.");
            return;
        }

        StartCoroutine(RegisterUserCoroutine(username, password));
    }

    void Login()
    {
        StartCoroutine(LoginCoroutine(usernameInput.text, passwordInput.text));
    }

    IEnumerator RegisterUserCoroutine(string username, string password)
    {
        string jsonData = $"{{\"correo\":\"{username}\",\"password\":\"{password}\"}}";

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
            ShowNotification("Usuario creado correctamente");
        }
    }

    IEnumerator LoginCoroutine(string username, string password)
    {
        UnityWebRequest www = UnityWebRequest.Get($"{serverURL}/Users?correo={username}&password={password}");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            ShowNotification("Usuario no encontrado");
            Debug.LogError(www.error);
        }
        else
        {
            if (www.downloadHandler.text.Contains("Usuario no encontrado"))
            {
                Debug.Log("Credenciales incorrectas");
            }
            else
            {
                Debug.Log($"Inicio de sesión exitoso como '{username}'");
                ShowNotification("Inicio de sesión correcto");
                UserDataManager.instance.userid = username;
                StartCoroutine(LoadSceneAfterDelay(2f));
            }
        }
    }

    void ShowNotification(string message)
    {
        GameObject notification = Instantiate(notificationPrefab, Vector3.zero, Quaternion.identity);
        notification.transform.SetParent(GameObject.Find("UIManager").transform.GetChild(0).transform, false);

        RectTransform notificationRectTransform = notification.GetComponent<RectTransform>();

        notificationRectTransform.anchorMin = new Vector2(0.5f, 0f);
        notificationRectTransform.anchorMax = new Vector2(0.5f, 0f);
        notificationRectTransform.pivot = new Vector2(0.5f, 0f);
        notificationRectTransform.anchoredPosition = new Vector2(0f, 600f);

        notification.GetComponentInChildren<TextMeshProUGUI>().text = message;

        StartCoroutine(DestroyNotificationAfterDelay(notification, 2f));
    }

    IEnumerator DestroyNotificationAfterDelay(GameObject notification, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(notification);
    }

    IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.instance.MainMenu(); 
    }

    // Función para validar un correo electrónico
    bool IsValidEmail(string email)
    {
        // Expresión regular para validar un correo electrónico
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, pattern);
    }

    // Función para validar una contraseña segura
    bool IsPasswordSecure(string password)
    {
        // Verificar que la contraseña tenga al menos 8 caracteres y contenga al menos una letra mayúscula
        return password.Length >= 8 && Regex.IsMatch(password, @"[A-Z]");
    }
}
