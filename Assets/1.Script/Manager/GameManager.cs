using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance;
        }
    }
    
    public float playTime{get; private set;}
    public float maxplayTime{get; private set;}
    public int stage{get; private set;}

    [Header("Player Info")]
    public float player_Hp;
    public float player_Max_Hp;
    public float player_Mp;
    public float player_Max_Mp;
    //게임 성장 관련 변수
    public int exp{get; private set;}
    public int[] nextExp{get; private set;} = {  };  //임시용 경험치통
    public int level{get; private set;}
    public int kill_num{get; private set;}
    public int[] max_kill_num{get; private set;} = { }; //임시용 스테이지 클리어 몬스터 수
    public int ap;
    public int sp{get; private set;}
    public int player_damage; //추가요소 : AP  //사용처 : 무기

    [Header("AP Info")]

    //ap 능력치 레벨
    public int ap_damage_Level;
    public int ap_hp_Level;
    public int ap_mp_Level;

    public int ap_speed_Level;

    //ap 능력치 증가량
    public int ap_damage;
    public int ap_hp;
    public int ap_mp;

    public float ap_speed;

    //ap 증가량 수치
    int ap_damage_plus;
    public static int Ap_damage_plus{get => Instance.ap_damage_plus; private set => Instance.ap_damage_plus = value; }

    //public int ap_damage_plus{get; private set;}
    public int ap_hmp_plus{get; private set;}

    public float ap_speed_plus{get; private set;}




    [Header("Game Money")]
    //게임 재화
    public int pp;
    public int gold{get; private set;}


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
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
        
        Ap_damage_plus = 3;
        ap_hmp_plus = 10;
        ap_speed_plus = 0.001f;

        maxplayTime = 60 * 1.5f;
        max_kill_num = new int[] { 5, 10, 15, 30, 3000, 500, 800, 1000 };
        nextExp = new int[] { 5, 10, 20, 20, 30, 50, 50, 60 } ; 
}

    private void Start()
    {
        //kill_num = 0;
        player_Max_Hp = 100;
        player_Max_Mp = 100;
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

            //Debug.Log(level + 1);

            // 이후 HP, MP 초기화
            player_Hp = player_Max_Hp;
            player_Mp = player_Max_Mp;
        }
    }
    public float HUD_Bar(float cur, float cur_Max)
    {
        float cur_bar = cur;
        float curMax_bar = cur_Max;
        //nextExp[level];
        return cur_bar/curMax_bar;
    }
    public void GetKill()
    {
        kill_num++;

        //스테이지 필요 몬스터 수 처치
        if (kill_num == max_kill_num[stage])
        {
            kill_num = 0;
            playTime = 0;
            stage++;
            //Debug.Log("현재 스테이지" + stage);
        }
        
    }
    public void GetItem(Item item)
    {
        switch (item.data.itemType)
        {   
            //골드 먹을 경우
            case ItemData.ItemType.Gold:
                gold++;
                break;

            //물약 획득
            case ItemData.ItemType.Heal:
                player_Hp = player_Max_Hp;
                break;
                
            //상자 획득
            case ItemData.ItemType.Box:
                
                
                break;
        }
    }

    //스킬정보창
    public void Click_Skill_Btn()
    {
        if (!skill_Panel.activeSelf)
        {
            skill_Panel.SetActive(true);
            reinfo_Pannel.SetActive(false);
            info_Panel.SetActive(false);
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
            skill_Panel.SetActive(false);
            reinfo_Pannel.SetActive(true);
            info_Panel.SetActive(false);
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
            skill_Panel.SetActive(false);
            reinfo_Pannel.SetActive(false);
            info_Panel.SetActive(true);
        }

        else
        {
            info_Panel.SetActive(false);
        }
    }

    public void Test_Click()
    {


    }

    public int Test_Info()
    {
        return Ap_damage_plus;
    }
}