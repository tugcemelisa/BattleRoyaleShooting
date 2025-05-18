using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Fox_Coins : MonoBehaviour
{
    Fox_Logic foxLogic;
    //obje adı
    public string objectName;
    //obje alınmış mı?
    public bool isTaken;

    private void Start()
    {
        foxLogic = FindObjectOfType<Fox_Logic>();
        // Eğer bu isimde bir kaydımız varsa
        if (PlayerPrefs.HasKey(objectName))
        {
            // Bu kaydın değerinin 1 olup olmadığına bakmak, sonucu isTaken değişkeninde depolamak 
            // Eğer böyle bir kayıt varsa, 1'i 1'le karşılaştıracağız yani her zaman True geri dönüşü alacağız
            isTaken = PlayerPrefs.GetInt(objectName) == 1;
            // isTaken değişkeninin değerine göre objenin durumunu aktive/deaktive etmek 
            gameObject.SetActive(!isTaken);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //Eğer paraya dokunan objenin "Player" etiketi varsa
        if (other.CompareTag("Player"))
        {
            //Değişkeni ayarlamak 
            isTaken = true;
            //Objenin adını kullanarak bir kayıt oluşturmak, içinde "1" bilgisini tutmak
            PlayerPrefs.SetInt(objectName, 1);
            // Objeyi deaktive etmek
            gameObject.SetActive(false);

            //Kayıtlı bilgilerden para miktarına bakmak ve geçici bir değişkende depolamak
            //Eğer böyle bir bilgi yoksa değeri sıfıra eşitlemek
            var value = PlayerPrefs.GetInt("Coins", 0);
            //"Coins" alanında saklanan para miktarını güncellemek
            //Bunu yapabilmek için geçici değişkendeki değere bir eklememiz lazım
            PlayerPrefs.SetInt("Coins", value + 1);
            //UI güncelleme fonksiyonunu çağıralım (hatayı görmezden gelebilirsiniz; daha fonksiyonu yazmadık)
            foxLogic.GetCoin();
        }
    }
}
