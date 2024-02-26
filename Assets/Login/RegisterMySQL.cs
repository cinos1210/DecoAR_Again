using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using MySql.Data.MySqlClient;
using System;
using Renci.SshNet;

public class Register : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Button registerButton;
    public Button loginButton;

     string connectionString = "server=20.106.127.74;uid=root;pwd=1105;database=DecoAR"; //configuración de MySQL

    void Start()
    {
        registerButton.onClick.AddListener(WriteToDatabase);
        loginButton.onClick.AddListener(Login);
    }

    void WriteToDatabase()
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM Users WHERE correo = @correo";
                cmd.Parameters.AddWithValue("@correo", usernameInput.text);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                
                if (count > 0)
                {
                    Debug.Log($"Username '{usernameInput.text}' already exists");
                }
                else
                {
                    cmd.CommandText = "INSERT INTO users (correo, password) VALUES (@correo, @password)";
                    cmd.Parameters.AddWithValue("@password", passwordInput.text);
                    cmd.ExecuteNonQuery();
                    Debug.Log("Account Registered");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }

    void Login()
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM users WHERE correo = @correo AND password = @password";
                cmd.Parameters.AddWithValue("@correo", usernameInput.text);
                cmd.Parameters.AddWithValue("@password", passwordInput.text);
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count > 0)
                {
                    Debug.Log($"Logging in '{usernameInput.text}'");
                    LoadWelcomeScreen();
                }
                else
                {
                    Debug.Log("Incorrect credentials");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }

    void LoadWelcomeScreen()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
