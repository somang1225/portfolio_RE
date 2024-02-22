using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType {  Exp_bar, Level, Kill_bar, Kill_bar_text, Time, Hp_bar, Mp_bar, Ap, Power, Stage, Gold ,
    AP_Damage, AP_Damage_Level, AP_HP, AP_HP_Level, AP_MP, AP_MP_Level}

    public InfoType type;
    Text mytext;
    Slider myslider;

    private void Awake()
    {
        mytext = GetComponent<Text>();
        myslider = GetComponent<Slider>();   
    }

    private void LateUpdate()
    {
        switch(type)
        {
            case InfoType.Exp_bar:
                myslider.value = GameManager.Instance.HUD_Exp();
                break;

            case InfoType.Level:
                mytext.text = string.Format("Lv. {0}",GameManager.Instance.level+1);
                break;

            case InfoType.Stage:
                mytext.text = string.Format("Stage {0}", GameManager.Instance.stage+1);
                break;

            case InfoType.Power:
                mytext.text = string.Format("전투력 {0}", GameManager.Instance.player_damage);
                break;

            case InfoType.Kill_bar:
                float cur_kill_num = GameManager.Instance.kill_num;
                float max_kill_num = GameManager.Instance.max_kill_num[GameManager.Instance.stage];
                myslider.value = cur_kill_num / max_kill_num;
                break;

            case InfoType.Kill_bar_text:
                mytext.text = string.Format("{0} / {1}", GameManager.Instance.kill_num, GameManager.Instance.max_kill_num[GameManager.Instance.stage]);
                break;

            case InfoType.Time:
                float reMain_time = GameManager.Instance.maxplayTime - GameManager.Instance.playTime;
                int min = Mathf.FloorToInt(reMain_time / 60);
                int sec = Mathf.FloorToInt(reMain_time % 60);
                mytext.text = $"{min.ToString("D2")} : {sec.ToString("D2")}";
                break;

            case InfoType.Hp_bar:
                float curHp = GameManager.Instance.player_Hp;
                float maxHp = GameManager.Instance.player_Max_Hp;
                myslider.value = curHp / maxHp;
                break;

            case InfoType.Mp_bar:
                float curMp = GameManager.Instance.player_Mp;
                float maxMp = GameManager.Instance.player_Max_Mp;
                myslider.value = curMp / maxMp;
                break;

            case InfoType.Ap:
                mytext.text = string.Format("특성 포인트 :  {0} / {1}", GameManager.Instance.ap, GameManager.Instance.level * 3);
                break;

            case InfoType.Gold:
                mytext.text = string.Format("{0}", GameManager.Instance.gold); 
                break;
            
            case InfoType.AP_Damage:
                mytext.text = string.Format("{0} -> {1}", GameManager.Instance.ap_damage, 
                    GameManager.Ap_damage_plus + GameManager.Instance.ap_damage);
                break;
            case InfoType.AP_Damage_Level:
                mytext.text = string.Format("Lv. {0}",GameManager.Instance.ap_damage_Level);
                break;
            case InfoType.AP_HP:
                mytext.text = string.Format("{0} -> {1}", GameManager.Instance.ap_hp, 
                    GameManager.Instance.ap_hp_plus + GameManager.Instance.ap_hp);
                break;
            case InfoType.AP_HP_Level:
                mytext.text = string.Format("Lv. {0}",GameManager.Instance.ap_hp_Level);
                break;
            case InfoType.AP_MP:
                mytext.text = string.Format("{0} -> {1}", GameManager.Instance.ap_mp, 
                    GameManager.Instance.ap_mp_plus + GameManager.Instance.ap_mp);
                break;
            case InfoType.AP_MP_Level:
                mytext.text = string.Format("Lv. {0}",GameManager.Instance.ap_mp_Level);
                break;
        }
    }

}
