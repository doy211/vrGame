using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class note_remove_editor : MonoBehaviour
{
    // 점수 저장용 변수
    private int score;
    // 점수 출력용 변수
    //public Text countText;
    // 판정 출력용 변수
    //public Text areaCheckText;

    void Update() {

    }

    void Start(){
        
    }

    // 만약 노트를 놓쳤다면
    void OnCollisionEnter(Collision coll) {
        // 충돌한 오브젝트의 태그값 비교
        if(coll.collider.tag == "NOTE"){
            // 충돌한 노트 삭제후 HP 깎음
            Destroy(coll.gameObject);
        }
    }
}
