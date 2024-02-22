using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class State_Manager : MonoBehaviour
{
    public enum State_Type{Damage, HP, MP}

    public State_Type state_Type;

    //버튼 클릭으로 레벨업
    public void Click_AP_Btn()
    {
        if( GameManager.Instance.ap != 0)
        {
            switch(state_Type)
            {
                case State_Type.Damage:
                    GameManager.Instance.ap_damage_Level ++;
                    GameManager.Instance.ap_damage = GameManager.Instance.ap_damage_Level * GameManager.Ap_damage_plus;
                    //GameManager.Instance.ap_damage = GameManager.Instance.ap_damage_Level * GameManager.Instance.Info();
                    GameManager.Instance.player_damage = GameManager.Instance.ap_damage;
                    break;
                case State_Type.HP:
                    GameManager.Instance.ap_hp_Level++;
                    GameManager.Instance.ap_hp = GameManager.Instance.ap_hp_Level * GameManager.Instance.ap_hp_plus;
                    GameManager.Instance.player_Max_Hp = 100 +  GameManager.Instance.ap_hp;
                    break;
                case State_Type.MP:
                    GameManager.Instance.ap_mp_Level ++;
                    GameManager.Instance.ap_mp = GameManager.Instance.ap_mp_Level * GameManager.Instance.ap_mp_plus;
                    GameManager.Instance.player_Max_Mp = 100 + GameManager.Instance.ap_mp;
                    break;   
            }
            GameManager.Instance.ap--;
        }
    }
}
