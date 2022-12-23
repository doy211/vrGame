using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class note_summon : MonoBehaviour
{
    // music 컴포넌트
    private AudioSource audioSource;
    public AudioClip music;
    // 노트 프리펩
    public GameObject Note;
    // 콜라이더 범위 저장용
    private BoxCollider area;
    // 노트 정보 저장용
    private note_info ni;
    // 플레이 버튼
    public Button button;
    // 오디오 재생 시간
    public float audio_time;
    // 버튼 눌렀을 때의 시간
    public float start_time;
    // 버튼 누른 이후의 시간
    public float button_time;
    public bool button_push;

    private void Awake()
    {
        area = GetComponent<BoxCollider>();
        //Play 버튼이 눌렸는지 확인용
        button_push = false;
    }

    void Start(){
        // 버튼 객체 할당
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

    void Update(){
        audio_time = audioSource.time;
        if(button_push) {
            button_time = Time.time - start_time;
        }
    }

    public bool summon(){
        // 폴더 내의 json 파일을 배열형태로 불러옴
        string[] files = Directory.GetFiles(Application.dataPath + "/" + "\\tmp\\", "*.json");
        foreach (var file in files)
        {
            // json에 담긴 정보를 note_list 클래스에 맡게 가져옴
            string json = File.ReadAllText(file);
            ni = JsonUtility.FromJson<note_info>(json);
            Vector3 spawnPos = new Vector3(ni.position_x, ni.position_y, ni.position_z);
            // 노트 객체 생성
            GameObject instance = Instantiate(Note, spawnPos, Quaternion.identity);
            instance.GetComponent<note_control>().set_time(ni.time);
            instance.GetComponent<note_control>().set_type(ni.note_type);
        }

        return true;
    }

    // 버튼을 누르면 노래가 재생되고 노트가 형성
    void OnClickPlayButton(){
        bool go = false;
        if(!button_push) {
            go = summon();
            button.transform.position += new Vector3(200, 0, 0);
        }
        transform.position += new Vector3(0, 0, -2);
        if(go){
            start_time = Time.time;
            button_push = true;
            Invoke("play_audio", 5);
        }
    }

    void play_audio(){
        audioSource.Play();
    }

    public void stop_audio(){
        audioSource.Stop();
    }

    public float get_audio_time(){
        return audio_time;
    }

    public float get_button_time(){
        return button_time;
    }
}

