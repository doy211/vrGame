using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class note_generate : MonoBehaviour
{
    // 노트 타입 결정. 1_일반노트 2_홀드노트 3_난타노트 4_동시타격노트
    public int note_type = 1;
    private string note_type_str;
    // 노트 프리펩
    public GameObject Note;

    // 콜라이더 범위 저장용
    private BoxCollider area;
    // 체크용 Text 변수
    public Text text;
    // 노트 리스트
    public List<note_info> note_list = new List<note_info>();

    // (임시) 노트 생성 포인트
    public GameObject tmp_point;

    void Start()
    {
        note_type = 1;
        area = GetComponent<BoxCollider>();
    }

    void Update()
    {
        
        // (임시) 1, 2, 3, 4를 눌러 노트 타입 변경
        change_note_type();
        // (임시) 화살표로 움직임
        change_tmp_point();

        // A를 눌렀을 때 summon 함수 호출
        if(note_type == 2) {
            if(Input.GetKey(KeyCode.A)){
                generate();
                add_note_in_list();
            }
        }else {
            if(Input.GetKeyDown(KeyCode.A)){
                generate();
                add_note_in_list();
            }
        }
    }

    // (임시)
    void change_tmp_point(){
        if(Input.GetKey(KeyCode.UpArrow)){
            tmp_point.transform.position += new Vector3(0, 0.05f, 0);
        }else if(Input.GetKey(KeyCode.DownArrow)){
            tmp_point.transform.position += new Vector3(0, -0.05f, 0);
        }else if(Input.GetKey(KeyCode.LeftArrow)){
            tmp_point.transform.position += new Vector3(0.05f, 0, 0);
        }else if(Input.GetKey(KeyCode.RightArrow)){
            tmp_point.transform.position += new Vector3(-0.05f, 0, 0);
        }
    }
    // (임시) 생성 포인트에 노트 생성
    private Vector3 GetPosition()
    {
        return tmp_point.transform.position;
    }
    
    public void generate(){
        // 위치함수
        Vector3 spawnPos = GetPosition();
        // 노트 객체 생성
        GameObject instance = Instantiate(Note, spawnPos, Quaternion.identity);
        Debug.Log(spawnPos);
    }

    // 노트 타입 변경
    void change_note_type(){
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            note_type = 1;
            note_type_str = "일반노트";
        }else if(Input.GetKeyDown(KeyCode.Alpha2)){
            note_type = 2;
            note_type_str = "홀드노트";
        }else if(Input.GetKeyDown(KeyCode.Alpha3)){
            note_type = 3;
            note_type_str = "난타노트";
        }else if(Input.GetKeyDown(KeyCode.Alpha4)){
            note_type = 4;
            note_type_str = "동시노트";
        }
        
        text.text = "현재 노트타입 : " + note_type_str;
    }

    // 노트 정보를 리스트에 추가
    void add_note_in_list(){
        float audio_time = GameObject.Find("Play_music").GetComponent<audio_play>().get_audio_time();
        float x = tmp_point.transform.position.x;
        float y = tmp_point.transform.position.y;
        float z = 54;
        note_info note = new note_info(note_type, audio_time, x, y, z);
        
        note_list.Add(note);
    }

    // 노트 타입 반환
    public int get_note_type(){
        return note_type;
    }
}


