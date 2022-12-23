using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class note_save : MonoBehaviour
{
    // 세이브 버튼
    public Button button;
    // 노트 리스트
    public List<note_info> note_list;
    // json 저장용 컨테이너
    
    void Start() {
        // 버튼 객체 할당
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickPlayButton);
        // 노트 리스트 가져오기
        this.note_list = GameObject.Find("Generate_plate_editor").GetComponent<note_generate>().note_list;
    }
    
    void Update() {
        
    }

    void OnClickPlayButton(){
        save();
    }

    // 노트를 json에 저장함
    void save(){
        // 리스트에 담긴 모든 노트를 가져옴
        // 일단 노트 하나마다 json 하나씩 생성하는 방식
        for(int i = 0; i<note_list.Count; ++i) {
            string json = JsonUtility.ToJson(note_list[i], true);
            note_info myJson = JsonUtility.FromJson<note_info>(json);
            string file_name = "tmp";
            string path = Application.dataPath + "/" + "\\tmp\\" + file_name + "[" + i + "]" + ".json";
            
            File.WriteAllText(path, json);
        }
    }
}
