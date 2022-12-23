using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class note_control_editor : MonoBehaviour
{
    // 노트 생성 각도
    public float angleX = 180.0f;
    public float angleY = 180.0f;
    // 노트 속도
    private float speed = 800f;
    // 노트 이동거리 계산용 변수
    private Vector3 before_location;
    private Vector3 after_location;
    private float distance = 0f;
    // 현재 노트타입
    private int note_type;
    private Rigidbody rb;
    // 노트 타입별 머티리얼
    public Material[] materials;

    void Start()
    {   
        // 노트 처음 위치
        before_location = transform.position;
        // Rigidbody 컴포넌트 추출
        rb = GetComponent<Rigidbody>();

        
        // 노트 타입 가져오기
        note_type = GameObject.Find("Generate_plate_editor").GetComponent<note_generate>().get_note_type();
        // 타입별 노트에 맞는 로직
        note_change();

        // 각도에 맞춰 노트의 속도를 조절해줌
        change_speed();

        // 노트의 전진 방향에 속도(또는 힘)을 가한다.
        rb.AddForce(-rb.transform.forward * speed);
    }

    void OnCollisionEnter(Collision coll) {
        calculate_distance(coll);
    }

    // 노트 속도 변경
    void change_speed(){
        speed = Random.Range(1000f, 1500f);
    }

    // 노트 타입에 따른 다양화
    void note_change(){
        // 일반노트
        if(note_type == 1){
            rb.GetComponent<Renderer>().material = materials[0];
            // 노트의 각도를 랜덤하게 설정함.
            angleX = Random.Range(-30, 30);
            angleY = Random.Range(-30, 30);
            rb.transform.rotation *= Quaternion.Euler(angleX, angleY, 0f);
        }
        // 홀드노트
        else if(note_type == 2){
            rb.GetComponent<Renderer>().material = materials[1];
            rb.transform.rotation *= Quaternion.Euler(angleX, angleY, 0f);
        }
        // 난타노트
        else if(note_type == 3){
            rb.GetComponent<Renderer>().material = materials[2];
            // 노트의 각도를 랜덤하게 설정함.
            angleX = Random.Range(-30, 30);
            angleY = Random.Range(-30, 30);
            rb.transform.rotation *= Quaternion.Euler(angleX, angleY, 0f);
        }
        // 동시노트
        else if(note_type == 4){
            rb.GetComponent<Renderer>().material = materials[3];
            // 노트의 각도를 랜덤하게 설정함.
            angleX = Random.Range(-30, 30);
            angleY = Random.Range(-30, 30);
            rb.transform.rotation *= Quaternion.Euler(angleX, angleY, 0f);
        }
    }

    // 노트 이동거리 계산
    void calculate_distance(Collision coll){
        // hit_plate와의 거리게산
        if(coll.collider.tag == "WALL"){
            distance += Vector3.Distance(coll.contacts[0].point, before_location);
            before_location = coll.contacts[0].point;
        }else if(coll.collider.tag == "HIT_PLATE"){
            distance += Vector3.Distance(coll.contacts[0].point, before_location);
        }
    }

    void took(){
        Debug.Log(distance);
    }
}