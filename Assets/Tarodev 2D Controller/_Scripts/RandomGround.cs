using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


namespace TarodevController {
public class RandomGround : MonoBehaviour
{
    private int countOfJump, countOfSpeed= 0;
    private bool sJumpClosed, sSpeedClosed, sInvisClosed;
    public GameObject anaPlayer;
    public List<GameObject> powerUplar;
    public GameObject powerImage,speedImage, invisImage;
    bool haveJumpSpeed, haveSpeedPower, fillDownJump, fillDownSpeed, haveInvisPower, haveSuperPower= false;
    public static bool activlyInvis, invisCheck = false;
    string aktifCheckpoint = "baslangiczemini";
    CinemachineVirtualCamera kamera;
    public AudioSource sesKaynagi;
    public AudioClip takePower, usePower;
    public Animator _animator;
    public ParticleSystem skilAlinca, hizKullaninca, jumpKullaninca, superPowerAlinca, h1,h2,j1,j2;

       
        private void OnTriggerEnter2D(Collider2D other) {
            if(other.gameObject.CompareTag("soSahareketliplatform") || other.gameObject.CompareTag("saSohareketliplatform")){
                transform.parent.parent = other.transform;
            }
            if(other.gameObject.CompareTag("diken") && !activlyInvis){
                restartFunc();
            }
            if(other.gameObject.CompareTag("jumppowerup"))
            {
                countOfJump++;
                haveJumpSpeed = true;
                powerImage.SetActive(true);
                other.gameObject.SetActive(false);
                sesEfekti(0);
                skilAlinca.Play();
            }else if(other.gameObject.CompareTag("speedpowerup"))
            {
                countOfSpeed++;
                haveSpeedPower = true;
                speedImage.SetActive(true);
                other.gameObject.SetActive(false);
                sesEfekti(0);
                skilAlinca.Play();
            }else if(other.gameObject.CompareTag("invispowerup"))
            {
                haveInvisPower = true;
                invisImage.SetActive(true);
                other.gameObject.SetActive(false);
                sesEfekti(0);
                skilAlinca.Play();
            }else if (other.gameObject.CompareTag("superpowerup"))
            {
                haveSuperPower = true;
                powerImage.SetActive(true);
                speedImage.SetActive(true);
                invisImage.SetActive(true);
                other.gameObject.SetActive(false);
                sesEfekti(0);
                superPowerAlinca.Play();
            }
            if(other.gameObject.CompareTag("checkpoint1") && aktifCheckpoint != "checkpoint2" && aktifCheckpoint != "checkpoint3" && aktifCheckpoint != "checkpoint4" && aktifCheckpoint != "checkpoint5"){
                aktifCheckpoint = "checkpoint1";
            } else if(other.gameObject.CompareTag("checkpoint2") && aktifCheckpoint != "checkpoint3" && aktifCheckpoint != "checkpoint4" && aktifCheckpoint != "checkpoint5"){
                aktifCheckpoint = "checkpoint2";
            } else if (other.gameObject.CompareTag("checkpoint3") && aktifCheckpoint != "checkpoint4" && aktifCheckpoint != "checkpoint5")
            {
                aktifCheckpoint = "checkpoint3";
            } else if (other.gameObject.CompareTag("checkpoint4") && aktifCheckpoint != "checkpoint5")
            {
                aktifCheckpoint = "checkpoint4";
            } else if (other.gameObject.CompareTag("checkpoint5"))
            {
                aktifCheckpoint = "checkpoint5";
            }

            if (other.gameObject.CompareTag("asko"))
            {
                _animator.SetBool("bittiMi", true);
                Invoke("restartGame",5);
                
            }

            if (other.gameObject.CompareTag("deadzone"))
            {
                restartFunc();
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("asko"))
            {
                TarodevController.PlayerController._moveClamp = 0;
                
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if(other.gameObject.CompareTag("soSahareketliplatform") || other.gameObject.CompareTag("saSohareketliplatform")){
                transform.parent.parent = null;
            }
        }

        private void Start()
        {
            kamera = GameObject.Find("CMVir").GetComponent<CinemachineVirtualCamera>();
            TarodevController.PlayerController._moveClamp = 13;
            
            
        }

        private void Update() {
            
            keyController();

            if(fillDownJump){
                powerImage.GetComponent<Image>().fillAmount -= 0.125f*Time.deltaTime;
                if(powerImage.GetComponent<Image>().fillAmount<0.01)
                {
                    kamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain -= 0.7f;
                    fillDownJump = false;
                    TarodevController.PlayerController._jumpHeight = 30f;
                    powerImage.GetComponent<Image>().fillAmount = 1f;
                    if (countOfJump != 2)
                        powerImage.SetActive(false);

                    countOfJump = 0;
                    kamera.m_Lens.OrthographicSize = 7.3f;
                    jumpKullaninca.enableEmission = false;
                    j1.enableEmission = false;
                    j2.enableEmission = false;
                }
            }
            if(fillDownSpeed){
                speedImage.GetComponent<Image>().fillAmount -= 0.125f*Time.deltaTime;
                if(speedImage.GetComponent<Image>().fillAmount<0.01){
                    kamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain -= 0.7f;
                    fillDownSpeed = false;
                    TarodevController.PlayerController._acceleration = 90f;
                    TarodevController.PlayerController._moveClamp = 13;
                    TarodevController.PlayerController._deAcceleration = 60f;
                    speedImage.GetComponent<Image>().fillAmount = 1f;
                    speedImage.SetActive(false);
                    if (countOfSpeed != 2)
                        speedImage.SetActive(false);

                    countOfSpeed = 0;
                    kamera.m_Lens.OrthographicSize = 7.3f;
                    hizKullaninca.enableEmission = false;
                    h1.enableEmission = false;
                    h2.enableEmission = false;
                    
                }
            }

            
        }
    


        public void keyController(){
            
            if(Input.GetKeyDown(KeyCode.J)){
                //Jump Skill Used
                if(haveJumpSpeed){
                    sesEfekti(1);
                    TarodevController.PlayerController._jumpHeight = 40f;
                    fillDownJump = true;
                    haveJumpSpeed = false;
                    kamera.m_Lens.OrthographicSize = 9;
                    kamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain += 0.7f;
                    jumpKullaninca.enableEmission = true;
                    j1.enableEmission = true;
                    j2.enableEmission = true;
                }

                if (haveSuperPower && sJumpClosed && kamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain>0)
                {
                    kamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain -= 0.7f;
                    TarodevController.PlayerController._jumpHeight = 30f;
                    kamera.m_Lens.OrthographicSize = 7.3f;
                    sJumpClosed = false;
                    jumpKullaninca.enableEmission = false;
                    j1.enableEmission = false;
                    j2.enableEmission = false;
                } else if (haveSuperPower)
                {
                    sesEfekti(1);
                    TarodevController.PlayerController._jumpHeight = 40f;
                    kamera.m_Lens.OrthographicSize = 9;
                    kamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain += 0.7f;
                    sJumpClosed = true;
                    jumpKullaninca.enableEmission = true;
                    j1.enableEmission = true;
                    j2.enableEmission = true;
                }
                
            }
            
            if(Input.GetKeyDown(KeyCode.H)){
                if(haveSpeedPower){
                    sesEfekti(1);
                    TarodevController.PlayerController._acceleration = 400f;
                    TarodevController.PlayerController._moveClamp = 26;
                    TarodevController.PlayerController._deAcceleration = 1000f;
                    fillDownSpeed = true;
                    haveSpeedPower = false;

                    kamera.m_Lens.OrthographicSize = 9;
                    kamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain += 0.7f;
                    hizKullaninca.enableEmission = true;
                    h1.enableEmission = true;
                    h2.enableEmission = true;
                }
                if (haveSuperPower && sSpeedClosed && kamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain>0)
                {
                    kamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain -= 0.7f;
                    TarodevController.PlayerController._acceleration = 90f;
                    TarodevController.PlayerController._moveClamp = 13;
                    TarodevController.PlayerController._deAcceleration = 60f;
                    kamera.m_Lens.OrthographicSize = 7.3f;
                    sSpeedClosed = false;
                    hizKullaninca.enableEmission = false;
                    h1.enableEmission = false;
                    h2.enableEmission = false;

                } else if (haveSuperPower)
                {
                    sesEfekti(1);
                    TarodevController.PlayerController._acceleration = 400f;
                    TarodevController.PlayerController._moveClamp = 26;
                    TarodevController.PlayerController._deAcceleration = 1000f;
                    
                    kamera.m_Lens.OrthographicSize = 9;
                    kamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain += 0.7f;
                    sSpeedClosed = true;
                    hizKullaninca.enableEmission = true;
                    h1.enableEmission = true;
                    h2.enableEmission = true;

                }
            }
            if(Input.GetKeyDown(KeyCode.R)){
                restartFunc();
            }

            if (Input.GetKey(KeyCode.K))
            {
                if (haveInvisPower)
                {
                    
                    kamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain += 0.7f;
                    activlyInvis = true;
                    GameObject.Find("KarakterSprite").GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                    haveInvisPower = false;
                    sInvisClosed = true;
                    invisCheck = true;
                }
                if (haveSuperPower && !sInvisClosed)
                {
                    
                    kamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain += 0.7f;
                    activlyInvis = true;
                    GameObject.Find("KarakterSprite").GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                    sInvisClosed = true;
                }
            }

            

            if (Input.GetKeyUp(KeyCode.K))
            {
                if (kamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain > 0 && sInvisClosed ==true)
                {
                    if (invisCheck)
                    {
                        invisImage.SetActive(false);
                        invisCheck = false;
                    }
                    
                    kamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain -= 0.7f;
                    activlyInvis = false;
                    sInvisClosed = false;
                    GameObject.Find("KarakterSprite").GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1f);
                }
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
                    anaPlayer.transform.position = GameObject.Find("checkpoint4").transform.position;
                    break; 
                case "checkpoint4":
                    anaPlayer.transform.position = GameObject.Find("checkpoint5").transform.position;
                    break;
                case "checkpoint5":
                    anaPlayer.transform.position = GameObject.Find("checkpoint6").transform.position;
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
            
            haveInvisPower = false;
            invisImage.GetComponent<Image>().fillAmount = 1f;
            invisImage.SetActive(false);

            haveSuperPower = false;
            activlyInvis = false;
            sInvisClosed = false;
            
            kamera.m_Lens.OrthographicSize = 7.3f;
            kamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
            
            GameObject.Find("KarakterSprite").GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1f);
            
            hizKullaninca.enableEmission = false;
            jumpKullaninca.enableEmission = false;
            j1.enableEmission = false;
            j2.enableEmission = false;
            h1.enableEmission = false;
            h2.enableEmission = false;
            
        }

        void sesEfekti(int durum)
        {
            if (durum == 0)
            {
                sesKaynagi.clip = takePower;
                sesKaynagi.Play();
            } else if (durum == 1)
            {
                sesKaynagi.clip = usePower;
                sesKaynagi.Play();
            }
        }

        void restartGame()
        {
            SceneManager.LoadScene(0);
        }
    }
}