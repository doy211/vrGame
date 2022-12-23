using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class audio_play : MonoBehaviour
{
    // music 컴포넌트
    private AudioSource audioSource;
    public AudioClip music;

    // 플레이 버튼
    public Button button;
    // 체크용 Text 변수
    public Text text;

    // 오디오 재생 시간
    public float audio_time;

    public GameObject block;

    bool check1 = false;
    bool check2 = false;

    public int a = 0;

    void Start()
    {
        // 버튼 객체 할당
        button = GetComponent<Button>();
        // 텍스트 객체 할당
        text.text = "현재시간 : X";

        button.onClick.AddListener(OnClickPlayButton);

        // 선택된 노래
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = music;
        // 뮤트: true일 경우 소리가 나지 않음
        audioSource.mute = false;
        // 루핑: true일 경우 반복 재생
        audioSource.loop = false;
        // 자동 재생: true일 경우 자동 재생
        audioSource.playOnAwake = false;
        
    }

    void OnCollisionEnter(Collision coll) {
        // 충돌한 오브젝트의 태그값 비교
        
        if(coll.collider.tag == "STICK"){
            if(!check1){
                if(!audioSource.isPlaying) {
                    check1 = true;
                    check2 = true;
                    Invoke("sta", 5);
                }else {
                    end();
                }
            }
            if(check2){
                end();
            }
        }
        
    }
    
    void Update()
    {
        text.text = "현재시간 : " + audioSource.time;
        audio_time = audioSource.time;
    }

    // 버튼을 클릭하면 노래가 재생되고 한 번 더 누르면 멈춤
    void OnClickPlayButton(){
        a++;

        if(!audioSource.isPlaying && (a == 2)) {
            //Invoke("sta", 5);
            sta();

        }else if(a == 4) {
            end();
            a = 0;
        }
    }

    public void sta(){
        audioSource.Play();
        GameObject.Find("Play_music").GetComponentInChildren<Text>().text = "Pause";
    }

    public void end(){
        audioSource.Pause();
        GameObject.Find("Play_music").GetComponentInChildren<Text>().text = "Play";
    }

    public float get_audio_time(){
        return audio_time;
    }
}



