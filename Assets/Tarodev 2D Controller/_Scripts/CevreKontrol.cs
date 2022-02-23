using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class CevreKontrol : MonoBehaviour
{
    public List<GameObject> hareketliNesneler;
    float[] guncelX = new float[40];
    Vector3 hareketYonu1 = Vector3.right;
    Vector3 hareketYonu2 = Vector3.left;
    public float platformHizi = 4f;
    public GameObject altimizdakiPlatform;
    public GameObject genelPanel, hakkindaPanel, nasilOynanirPanel, ayarlarPanel;
    

    // Update is called once per frame
    private void Start() {
        int i = 0;
        foreach (var item in hareketliNesneler)
        {
            guncelX[i] = item.gameObject.transform.position.x;
            i++;
        }
    }
    private void FixedUpdate() {
        int i = 0;
        foreach (var item in hareketliNesneler)
        {
            float itemGuncelX = item.gameObject.transform.position.x;
            if(item.CompareTag("soSahareketliplatform")){
                if(itemGuncelX<guncelX[i]){
                    hareketYonu1 = Vector3.right;
                } else if(itemGuncelX>guncelX[i]+15){
                    hareketYonu1 = Vector3.left;
                }
                item.transform.Translate(hareketYonu1 * Time.deltaTime*platformHizi,Space.World);
            } 
            if(item.CompareTag("saSohareketliplatform")){
                if(itemGuncelX>guncelX[i]){
                    hareketYonu2 = Vector3.left;
                } else if(itemGuncelX<guncelX[i]-15){
                    hareketYonu2 = Vector3.right;
                }
                item.transform.Translate(hareketYonu2 * Time.deltaTime*platformHizi,Space.World);
            }
            i++;
        }
        
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (genelPanel.activeSelf)
            {
                genelPanel.SetActive(false);
            } else
            {
                genelPanel.SetActive(true);
            }
            
        }
    }

    public void OynaButton()
    {
        altimizdakiPlatform.SetActive(false);
        genelPanel.SetActive(false);
    }

    public void nasilOynanirButton()
    {
        hakkindaPanel.SetActive(false);
        nasilOynanirPanel.SetActive(true);
        ayarlarPanel.SetActive(false);
    }

    public void hakkindaButton()
    {
        hakkindaPanel.SetActive(true);
        nasilOynanirPanel.SetActive(false);
        ayarlarPanel.SetActive(false);
    }

    public void ayarlarPanelButton()
    {
        ayarlarPanel.SetActive(true);
        hakkindaPanel.SetActive(false);
        nasilOynanirPanel.SetActive(false);
    }

    public void sesKis()
    {
        AudioListener.volume -= 0.1f;
    }

    public void sesAc()
    {
        AudioListener.volume += 0.1f;
    }
}
