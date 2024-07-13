using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeCardReward : MonoBehaviour
{
    public StoreData storeData;
    
    public CardUI cardUI1, cardUI2, cardUI3;

    public void ShowReward()
    {
        int randomIdx = Random.Range(0, storeData.saleItems.Count-1);
        int randomIdx2 = Random.Range(0, storeData.saleItems.Count-1);
        int randomIdx3 = Random.Range(0, storeData.saleItems.Count-1);

        cardUI1.InitCard(storeData.saleItems[randomIdx].ability, storeData.saleItems[randomIdx].ballSkillEnum);
        cardUI2.InitCard(storeData.saleItems[randomIdx2].ability, storeData.saleItems[randomIdx2].ballSkillEnum);
        cardUI3.InitCard(storeData.saleItems[randomIdx3].ability, storeData.saleItems[randomIdx3].ballSkillEnum);

        Time.timeScale = 0;
    }

    public void ChooseCard(CardUI cardUI)
    {
        if(cardUI._skillEnum == AbilityEnums.BallUpdate)
        {
            BallSystem sys = GameObject.FindObjectOfType<BallSystem>();
            if (sys)
            {
                sys.SettingBallType(((BallAsset)(cardUI.ablity)).BallType);
            }
        }
        else
        {
            GameManager.Instance.Player.AddAbility(cardUI._skillEnum, cardUI.ablity);
        }
        Time.timeScale = 1;
        gameObject.SetActive(false);

    }
}
