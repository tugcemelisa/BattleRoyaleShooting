using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fox_Menu : MonoBehaviour
{
    // Load Game butonuna referans
    [SerializeField] Button loadButton;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        // Kayıtlı bilgi var mı kontrolü. Eğer varsa Load butonunu aktifleştiriyoruz
        if (PlayerPrefs.HasKey("posX"))
        {
            loadButton.interactable = true;
        }
    }
    // Yeni oyun fonksiyonu
    public void StartNewGame()
    {
        // Kayıtlı bilgi var mı kontrolü. Eğer varsa bütün kayıtlı bilgileri siliyoruz ve yeni bir oyun başlatıyoruz
        if (PlayerPrefs.HasKey("posX"))
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("Game");
        }
        // Yoksa direkt oyunu başlatıyoruz
        else
        {
            SceneManager.LoadScene("Game");
        }
    }
    // Oyunu bütün kayıtlı bilgilerle başlatan bir fonksiyon 
    public void LoadGame()
    {
        // Eğer kayıtlı bilgi varsa oyunu başlatma
        if (PlayerPrefs.HasKey("posX"))
        {
            SceneManager.LoadScene("Game");
        }
    }
}
