using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class note_control : MonoBehaviour
{
    // 리지드바디
    private Rigidbody rb;
    // 노트 생성 각도
    public float angleX = 0f;
    public float angleY = 0f;
    // 노트 속도
    private float speed = 10f;
    // 노트 등장 시간비교용
    private float time;
    private float up_time;
    // 노트가 움직이고 있는가?
    private bool move = false;
    // 노트가 마지막 지점에 도달했는가?
    private bool reach = false;
    // 노트 타입
    private int note_type;
    // 노트 타입별 머티리얼
    public Material[] materials;
    // 노트 이동거리
    private float distance;
    // 노트 거리계산 클래스
    private note_calculate_distance ncd;
    // 반사지점 저장 리스트
    private List<Vector3> vector3List;
    private int after = 1;
    // 노트 처음 위치
    private Vector3 note_location;
    // 충돌지점에 이펙트 생성
    public GameObject effect;

    void Start()
    {   
        // Rigidbody 컴포넌트 추출
        rb = GetComponent<Rigidbody>();

        // 타입별 노트에 맞는 로직
        note_change();
        
        // 거리 계산
        calculate_distance();

        // 각도에 맞춰 노트의 속도를 조절해줌
        change_speed();
        
        note_location = transform.position;
    }

    void Update(){
        // 노트 등장 시간
        up_time = GameObject.Find("Summon_plate").GetComponent<note_summon>().get_button_time();
        
        // 반사되는 지점에 도달했으면 이펙트 생성후 다음 지점지정
        if(Vector3.Distance(transform.position, vector3List[after]) == 0){
            if(!reach) {
                GameObject eff = Instantiate(effect, transform.position, Quaternion.identity);
                Destroy(eff, 1f);
            }

            if(vector3List.Count-1 > after) ++after;
            note_location = transform.position;
        }
        if(!reach) {
            if(time > up_time - 0.005f && time < up_time + 0.005f) move = true;
            if(move == true){
                this.transform.position = Vector3.MoveTowards(transform.position, vector3List[after], speed * Time.deltaTime);
            }
        }

        // 노트가 perfact 까지 갔다면
        if(Vector3.Distance(transform.position, vector3List[vector3List.Count-1]) == 0){
            reach = true;
            // perface에 도달하면 움직임이 멈추기 때문에 MISS존까지의 추가적인 힘이 필요함
            Vector3 vc =  vector3List[vector3List.Count-1] - note_location;
            rb.AddForce(vc * 30f);
            
        }
    }

    // 노트 타입에 따른 다양화. 이때 노트의 껍데기(shell)의 머티리얼을 변경함
    void note_change(){
        // 일반노트
        if(note_type == 1){
            rb.transform.GetChild(0).GetComponent<Renderer>().material = materials[0];
            // 노트의 각도를 랜덤하게 설정함.
            angleX = Random.Range(-30, 30);
            angleY = Random.Range(-30, 30);
            rb.transform.rotation *= Quaternion.Euler(angleX, angleY, 0f);
        }
        // 홀드노트
        else if(note_type == 2){
            rb.transform.GetChild(0).GetComponent<Renderer>().material = materials[1];
            rb.transform.rotation *= Quaternion.Euler(angleX, angleY, 0f);
        }
        // 난타노트
        else if(note_type == 3){
            rb.transform.GetChild(0).GetComponent<Renderer>().material = materials[2];
            // 노트의 각도를 랜덤하게 설정함.
            angleX = Random.Range(-30, 30);
            angleY = Random.Range(-30, 30);
            rb.transform.rotation *= Quaternion.Euler(angleX, angleY, 0f);
        }
        // 동시노트
        else if(note_type == 4){
            rb.transform.GetChild(0).GetComponent<Renderer>().material = materials[3];
            // 노트의 각도를 랜덤하게 설정함.
            angleX = Random.Range(-30, 30);
            angleY = Random.Range(-30, 30);
            rb.transform.rotation *= Quaternion.Euler(angleX, angleY, 0f);
        }
    }

    // 노트 속도 변경
    // 일직선 이동거리 50, 시간 5, 속도 10기준
    void change_speed(){
        speed = distance / 5;
    }

    // 노트 이동거리 계산
    void calculate_distance(){
        ncd = gameObject.AddComponent<note_calculate_distance>();
        ncd.set_note_calculate_distance(transform.position, -rb.transform.forward);
        distance = ncd.get_distance();
        vector3List = ncd.get_List();
    }





    /*
    // 거리 계산용 충돌체크
    void OnCollisionEnter(Collision coll) {
        calculate_distance2(coll);
    }

     // 노트 이동거리 계산
    void calculate_distance2(Collision coll){
        // hit_plate와의 거리게산
        if(coll.collider.tag == "WALL"){
            distance2 += Vector3.Distance(coll.contacts[0].point, before_location);
            before_location = coll.contacts[0].point;
        }else if(coll.collider.tag == "HIT_PLATE"){
            distance2 += Vector3.Distance(coll.contacts[0].point, before_location);
            Debug.Log("거리 : " + distance + "\n실제 이동거리 : " + distance2 + "\n속도 : " + speed);
        }
    }
    */




    // json에서 받아온 정보 set용
    public void set_time(float time){
        this.time = time;
    }
    public void set_type(int type){
        this.note_type = type;
    }
}

