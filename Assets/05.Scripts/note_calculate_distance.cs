using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class note_calculate_distance : MonoBehaviour
{
	// 최대 반사수
	public int reflections = 20;
	// 최대 길이
	public float maxLength = 1000;

	private LineRenderer lineRenderer;
	// 레이 캐스트
	private Ray ray;
	// 충돌 지점
	private RaycastHit hit;
	// 방향
	private Vector3 direction;
	// 레이저의 모든 충돌지점을 저장하기 위함
	private List<Vector3> vectorList = new List<Vector3>();

    private float distance = 0f;

	// 노트의 생성위치, 방향
    private Vector3 tp;
    private Vector3 tf;

	// 노트 정보 저장
    public void set_note_calculate_distance(Vector3 position, Vector3 forward){
        tp = position;
        tf = forward;
		vectorList.Add(tp);
        cal();
    }

	private void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();		
	}

	// 레이저가 반사되고 반사되기 전의 거리를 distance에 += 방식
    void cal(){
        ray = new Ray(tp, tf);

		// 첫번째 반사
		lineRenderer.positionCount = 1;
		lineRenderer.SetPosition(0, transform.position);
		// 남은길이
		float remainingLength = maxLength;

		for (int i = 0; i < reflections; i++)
		{
			// 레이캐스트로 위치, 반사 각도, 히트 지점, 남은 길이 지정
			if(Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength))
			{
				lineRenderer.positionCount += 1;
				// 레이 캐스트로 원래 지점부터 반사되는 지점까지 레이저를 형성 해줌
				lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
				// 레이저 길이만큼 차감
				remainingLength -= Vector3.Distance(ray.origin, hit.point);
				// 레이저 길이를 distance에 계속 저장하고 리스트에 저장
                distance += Vector3.Distance(ray.origin, hit.point);
				vectorList.Add(hit.point);
				// 반사
				ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
				if (hit.collider.tag != "WALL") {
					vectorList.Add(hit.point);
					break;
				}
			}
			else // 반사되는 지점이 없다면
			{
				lineRenderer.positionCount += 1;
				lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
			}
            
		}
    }

    public float get_distance(){
        return distance;
    }

	public List<Vector3> get_List(){
		return vectorList;
	}
}