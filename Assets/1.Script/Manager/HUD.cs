using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType {  Exp, Level, Kill_num, Kill_num_text, Time, Hp, Mp, Ap, Power, Stage, Gold }

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
            case InfoType.Exp:
                float curExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[GameManager.instance.level];
                myslider.value = curExp / maxExp;
                break;

            case InfoType.Level:
                mytext.text = string.Format("Lv. {0}",GameManager.instance.level+1);
                break;

            case InfoType.Stage:
                mytext.text = string.Format("Stage {0}", GameManager.instance.stage+1);
                break;

            case InfoType.Power:
                //mytext.text = string.Format("전투력 {0}", GameManager.instance.power);
                break;

            case InfoType.Kill_num:
                float cur_kill_num = GameManager.instance.kill_num;
                float max_kill_num = GameManager.instance.max_kill_num[GameManager.instance.stage];
                myslider.value = cur_kill_num / max_kill_num;
                break;

            case InfoType.Kill_num_text:
                mytext.text = string.Format("{0} / {1}", GameManager.instance.kill_num, GameManager.instance.max_kill_num[GameManager.instance.stage]);
                break;

            case InfoType.Time:
                float reMain_time = GameManager.instance.maxplayTime - GameManager.instance.playTime;
                int min = Mathf.FloorToInt(reMain_time / 60);
                int sec = Mathf.FloorToInt(reMain_time % 60);
                mytext.text = $"{min.ToString("D2")} : {sec.ToString("D2")}";
                break;

            case InfoType.Hp:
                float curHp = GameManager.instance.player_Hp;
                float maxHp = GameManager.instance.player_Max_Hp;
                myslider.value = curHp / maxHp;
                break;

            case InfoType.Mp:
                float curMp = GameManager.instance.player_Mp;
                float maxMp = GameManager.instance.player_Max_Mp;
                myslider.value = curMp / maxMp;
                break;

            case InfoType.Ap:
                mytext.text = string.Format("특성 포인트 :  {0} / {1}", GameManager.instance.ap, GameManager.instance.level * 3);
                break;

            case InfoType.Gold:
                mytext.text = string.Format("{0}", GameManager.instance.gold); 
                break;
        }
    }

}
