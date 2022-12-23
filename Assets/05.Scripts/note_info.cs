using System;

// 노트의 기본정보가 담겨있음
[Serializable]
public class note_info
{
    public int note_type;
    public float time;
    public float position_x;
    public float position_y;
    public float position_z;

    public note_info(int note_type, float time, float position_x, float position_y, float position_z){
        this.note_type = note_type;
        this.time = time;
        this.position_x = position_x;
        this.position_y = position_y;
        this.position_z = position_z;
    }
}
