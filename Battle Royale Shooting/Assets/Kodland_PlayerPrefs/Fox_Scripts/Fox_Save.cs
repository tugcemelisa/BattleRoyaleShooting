using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fox_Save : MonoBehaviour
{
    [SerializeField] TMP_Text saveWarning;

    //Karakterin pozisyonunu kaydetmek
    public void SavePosition(Vector3 playerPos)
    {
        // PlayerPrefs'in farklı alanlarına karakterin pozisyonunun eksenlerini kaydetmek
        PlayerPrefs.SetFloat("posX", playerPos.x);
        PlayerPrefs.SetFloat("posY", playerPos.y);
        PlayerPrefs.SetFloat("posZ", playerPos.z);
        // Verileri kaydetmek
        PlayerPrefs.Save();
        saveWarning.text = "The save was successful!";
        Invoke("DeleteText", 2f);
    }

    public void DeleteText()
    {
        saveWarning.text = "";
    }


    private void OnTriggerEnter(Collider other)
    {
        // Eğer portaldan Player etiketli bir objenin geçişi tetiklenirse
        if (other.CompareTag("Player"))
        {
            // Objenin pozisyonuna bakıp bu bilgiyi SavePosition fonksiyonuna iletelim
            Vector3 pos = other.transform.position;
            SavePosition(pos);
        }
    }
}
