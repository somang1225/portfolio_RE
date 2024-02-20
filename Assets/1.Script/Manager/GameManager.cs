using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //게임 플레이 관련 변수
    [Header("Game Control")]
    public float playTime;
    public float maxplayTime;
    public int stage;

    [Header("Player Info")]
    //게임 성장 관련 변수
    public int exp;
    public int[] nextExp = {  };  //임시용 경험치통
    public int level;
    public int kill_num;
    public int[] max_kill_num = { }; //임시용 스테이지 클리어 몬스터 수
    public int ap;
    public int sp;
    public int power;
    public float player_Hp;
    public float player_Max_Hp;
    public float player_Mp;
    public float player_Max_Mp;

    [Header("Game Money")]
    //게임 재화
    public int gold;
    public int pp;

    [Header("Game Script")]
    //스크립트 연결
    public Player player;
    public PoolManager pool;

    [Header("Game Object")]
    //오브젝트 연결
    public GameObject skill_Panel; //스킬 판넬
    public GameObject info_Panel; //인벤 판넬
    public GameObject reinfo_Pannel; //강화 판넬

    private void Awake()
    {
        instance = this;
        player_Max_Hp = 100;
        player_Max_Mp = 100;
        maxplayTime = 60 * 1.5f;
        max_kill_num = new int[] { 10, 10, 10, 30, 300, 500, 800, 1000 };
        nextExp = new int[] { 10, 20, 20, 20, 30, 50, 50, 60 } ; 
}

    private void Start()
    {
        kill_num = 0;
        player_Hp = player_Max_Hp;
        player_Mp = player_Max_Mp;
        
    }

    void Update()
    {
        //지정된 시간안에 몬스터를 못 잡을 경우 시간과 잡은 몬스터 수 초기화
        playTime += Time.deltaTime;

        if (playTime >= maxplayTime)
        {
            playTime = 0;
            kill_num = 0;
        }

    }

    public void GetExp()
    {
        exp++;
        if (exp == nextExp[level])
        {
            //레벨업 후 능력치 부여 + 경험치 통 초기화
            exp = 0;
            level++;
            ap+= 3;

            Debug.Log("현재 레벨");
            Debug.Log(level + 1);

            // 이후 HP, MP 초기화
            player_Hp = player_Max_Hp;
            player_Mp = player_Max_Mp;
        }
    }

    public void GetKill()
    {
        kill_num++;
        if (kill_num == max_kill_num[stage])
        {
            kill_num = 0;
            stage++;
            Debug.Log("현재 스테이지" + stage);
            

        }
        
    }

    //스킬정보창
    public void Click_Skill_Btn()
    {
        if (!skill_Panel.activeSelf)
        {
            skill_Panel.SetActive(true);
        }

        else
        {
            skill_Panel.SetActive(false);
        }
    }
    //강화창 클릭
    public void Click_Reinfo_Btn()
    {
        if (!reinfo_Pannel.activeSelf)
        {
            reinfo_Pannel.SetActive(true);
        }

        else
        {
            reinfo_Pannel.SetActive(false);
        }
    }
    //인벤토리 클릭
    public void Click_Info_Btn()
    {
        if (!info_Panel.activeSelf)
        {
            info_Panel.SetActive(true);
        }

        else
        {
            info_Panel.SetActive(false);
        }
    }

}
