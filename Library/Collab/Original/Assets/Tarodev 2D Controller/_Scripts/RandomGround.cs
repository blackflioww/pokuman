using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




namespace TarodevController {
public class RandomGround : MonoBehaviour
{
    public GameObject anaPlayer;
    public List<GameObject> powerUplar;
    public List<GameObject> zeminler;
    public GameObject powerImage,speedImage;
    GameObject geriGelecekItem;
    bool haveJumpSpeed= false;
    bool fillDownJump = false;
    bool haveSpeedPower = false;
    bool fillDownSpeed = false;
    string aktifCheckpoint = "baslangiczemini";


       
        private void OnTriggerEnter2D(Collider2D other) {
            if(other.gameObject.CompareTag("soSahareketliplatform") || other.gameObject.CompareTag("saSohareketliplatform")){
                transform.parent.parent = other.transform;
            }
            if(other.gameObject.CompareTag("diken")){
                restartFunc();
            }
            if(other.gameObject.CompareTag("jumppowerup")){
                haveJumpSpeed = true;
                powerImage.SetActive(true);
                other.gameObject.SetActive(false);
            }else if(other.gameObject.CompareTag("speedpowerup")){
                haveSpeedPower = true;
                speedImage.SetActive(true);
                other.gameObject.SetActive(false);
            }
            if(other.gameObject.CompareTag("checkpoint1") && aktifCheckpoint != "checkpoint2"){
                aktifCheckpoint = "checkpoint1";
            } else if(other.gameObject.CompareTag("checkpoint2")){
                aktifCheckpoint = "checkpoint2";
            }
        }
        /*private void OnTriggerStay2D(Collider2D other) {
            //Debug.Log("trigger");
            foreach (var item in zeminler)
            {

                if(other.gameObject.name == item.gameObject.name){
                    zeminler.Remove(item);
                    geriGelecekItem = item;
                } 
            }
        }*/
        private void OnTriggerExit2D(Collider2D other) {
            if(other.gameObject.CompareTag("soSahareketliplatform") || other.gameObject.CompareTag("saSohareketliplatform")){
                transform.parent.parent = null;
            }
            /*if(other.gameObject.CompareTag("baslangiczemini")){
                return;
            }
            foreach (var item in zeminler)
            {

                if(other.gameObject.name == item.gameObject.name){
                    return;
                } 
            }
            zeminler.Add(geriGelecekItem);*/
        }
        
        private void Update() {
            
            keyController();

            if(fillDownJump){
                powerImage.GetComponent<Image>().fillAmount -= 0.125f*Time.deltaTime;
                if(powerImage.GetComponent<Image>().fillAmount<0.01){
                    fillDownJump = false;
                    TarodevController.PlayerController._jumpHeight = 30f;
                    powerImage.GetComponent<Image>().fillAmount = 1f;
                    powerImage.SetActive(false);
                }
            }
            if(fillDownSpeed){
                speedImage.GetComponent<Image>().fillAmount -= 0.125f*Time.deltaTime;
                if(speedImage.GetComponent<Image>().fillAmount<0.01){
                    fillDownSpeed = false;
                    TarodevController.PlayerController._acceleration = 90f;
                    TarodevController.PlayerController._moveClamp = 13;
                    TarodevController.PlayerController._deAcceleration = 60f;
                    speedImage.GetComponent<Image>().fillAmount = 1f;
                    speedImage.SetActive(false);
                }
            }
        }
    


        public void keyController(){
            if(Input.GetKeyDown(KeyCode.Q)){
                foreach (var item in zeminler)
                {
                    Vector3 guncelKonum = item.gameObject.transform.position;
                    Vector3 guncelBoyut = item.gameObject.transform.localScale;
                    float randomZeminX = Random.Range(11f,52f);
                    float randomZeminy = Random.Range(guncelKonum.y+4f, guncelKonum.y-4f);
                // float randomZeminboyutx = Random.Range(1f, 3f);float randomZeminboyuty = Random.Range(0.5f, 0.5f);
                    item.gameObject.transform.position = new Vector3(randomZeminX, randomZeminy, guncelKonum.z);
                    //item.gameObject.transform.localScale = new Vector3(randomZeminboyutx, randomZeminboyuty, guncelBoyut.z);

                }
            }
            if(Input.GetKeyDown(KeyCode.J)){
                //Jump Skill Used
                if(haveJumpSpeed){
                    TarodevController.PlayerController._jumpHeight = 40f;
                    fillDownJump = true;
                    haveJumpSpeed = false;
                }
            }
            if(Input.GetKeyDown(KeyCode.H)){
                if(haveSpeedPower){
                    TarodevController.PlayerController._acceleration = 400f;
                    TarodevController.PlayerController._moveClamp = 26;
                    TarodevController.PlayerController._deAcceleration = 1000f;
                    fillDownSpeed = true;
                    haveSpeedPower = false;

                    //GameObject.Find("CMVir").GetComponent<CinemachineVirtualCamera>().orthographicSize = 9;
                }
            }
            if(Input.GetKeyDown(KeyCode.R)){
                restartFunc();
            }
        }

        public void restartFunc(){
            switch(aktifCheckpoint){
                case "baslangiczemini":
                    anaPlayer.transform.position = GameObject.Find("checkpoint1").transform.position;
                    break;
                case "checkpoint1":
                    anaPlayer.transform.position = GameObject.Find("checkpoint2").transform.position;
                    break;
                case "checkpoint2":
                    anaPlayer.transform.position = GameObject.Find("checkpoint3").transform.position;
                    break;
                case "checkpoint3":
                    break; 
            }
            foreach (var item in powerUplar)
            {
                item.SetActive(true);
            }
            fillDownJump = false;
            TarodevController.PlayerController._jumpHeight = 30f;
            powerImage.GetComponent<Image>().fillAmount = 1f;
            powerImage.SetActive(false);
            haveJumpSpeed = false;

            TarodevController.PlayerController._acceleration = 90f;
            TarodevController.PlayerController._moveClamp = 13;
            TarodevController.PlayerController._deAcceleration = 60f;
            fillDownSpeed = false;
            haveSpeedPower = false;
            speedImage.GetComponent<Image>().fillAmount = 1f;
            speedImage.SetActive(false);

        }
    }
}