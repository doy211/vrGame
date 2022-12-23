using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class note_remove_and_pointup : MonoBehaviour
{
    // 점수 저장용 변수
    public static int score;
    // 점수 출력용 변수
    public TextMesh countText;
    // 판정 출력용 변수
    public Text areaCheckText;
    // HP관련 변수들
    public Slider hpbar;
    public float maxHp;
    public float currenthp;
    // 에어리어 체크용 변수
    public GameObject greatArea_front;
    public GameObject greatArea_back;
    public GameObject perfact_Area;
    private int checked_area; // perfact 2, great 1, miss 0
    // MISS, GTRAT, PERFACT
    public GameObject miss;
    public GameObject great;
    public GameObject perfact;

    void Update() {
        // HP바를 실시간으로 보여줌
        hpbar.value = currenthp / maxHp;
        
        // 밑 함수는 마우스 클릭시 노트가 터지고 점수가 올라가는 코드
        // 오큘러스 퀘스트 컨트롤러로 바꾸면 밑의 코드를 수정해야 함
        if(Input.GetMouseButtonDown(0)){
            //noteHit();
        }

        /*
        // 체력이 0보다 낮으면 모든 노트를 삭제하고 게임오버 (나중에구현)
        if(currenthp <= 0){
            GameObject.Find("Summon_plate").GetComponent<note_summon>().stop_audio();
            GameObject[] allNote = GameObject.FindGameObjectsWithTag("NOTE");
            for(int i = 0; i < allNote.Length; ++i)
                Destroy(allNote[i]);
        }
        */
    }

    // 시작시 Count : 0, HP 100 으로 시작하게 함
    void Start(){
        score = 0;
        countText.text = ""+note_remove_and_pointup.score;
        areaCheckText.text = "NULL";

        maxHp = 100f;
        currenthp = 100f;
    }

    // 만약 노트를 놓쳤다면
    void OnCollisionEnter(Collision coll) {
        // 충돌한 오브젝트의 태그값 비교
        if(coll.collider.tag == "NOTE"){
            // 충돌한 노트 삭제후 HP 깎음
            check_Area(coll);
                
            // 히트한 지점에 따른 처리
            // 현재 채력에 5를 더하는데 만약 100보다 크면 100으로 변경
            // 노트를 클릭하면 스코어가 판정에 따라 올라감
            if(checked_area == 0) {
                areaCheckText.text = "MISS";
                GameObject eff = Instantiate(miss, transform.position, Quaternion.identity);
                Destroy(eff, 0.5f);
                currenthp -= 2f;
                
            }else if(checked_area == 1) {
                areaCheckText.text = "Great";
                GameObject eff = Instantiate(great, transform.position, Quaternion.identity);
                Destroy(eff, 0.5f);
                currenthp += 5;
                if(currenthp > 1000f) currenthp = maxHp;
                note_remove_and_pointup.score += 50;
                countText.text = "" + note_remove_and_pointup.score;
            }
            else if(checked_area == 2) {
                areaCheckText.text = "Perfact";
                GameObject eff = Instantiate(perfact, transform.position, Quaternion.identity);
                Destroy(eff, 0.5f);
                currenthp += 10;
                if(currenthp > 1000f) currenthp = maxHp;
                note_remove_and_pointup.score += 100;
                countText.text = "" + note_remove_and_pointup.score;
            }

            Destroy(coll.gameObject);
            currenthp -= 2f;
            if(currenthp < 0) currenthp = 0;
        }
    }

    // 노트가 어디서 히트 됐는지 확인하는 함수
    // Z값 비교해서 결정. 능동적이지 못하므로 필요시 수정 필요
    void check_Area(Collision coll){
        float hit_z = coll.transform.position.z;
        float gab_z = greatArea_back.transform.position.z;
        float gaf_z = greatArea_front.transform.position.z;
        float pa_z = perfact_Area.transform.position.z;

        if(hit_z > pa_z -0.25 && hit_z < pa_z +0.25) checked_area = 2;
        else if(hit_z > gab_z -0.25 && hit_z < gab_z +0.25) checked_area = 1;
        else if(hit_z > gaf_z -0.25 && hit_z < gaf_z +0.25) checked_area = 1;
        else checked_area = 0;
    }
}

