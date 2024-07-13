using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeCardReward : MonoBehaviour
{
    public StoreData storeData;
    
    public CardUI cardUI1, cardUI2, cardUI3;

    private void OnEnable()
    {
        ShowReward();
    }

    public void ShowReward()
    {
        int randomIdx = Random.Range(0, storeData.saleItems.Count-1);
        int randomIdx2 = Random.Range(0, storeData.saleItems.Count-1);
        int randomIdx3 = Random.Range(0, storeData.saleItems.Count-1);

        cardUI1.InitCard(storeData.saleItems[randomIdx].ability, storeData.saleItems[randomIdx].ballSkillEnum);
        cardUI2.InitCard(storeData.saleItems[randomIdx2].ability, storeData.saleItems[randomIdx2].ballSkillEnum);
        cardUI3.InitCard(storeData.saleItems[randomIdx3].ability, storeData.saleItems[randomIdx3].ballSkillEnum);
    }

    public void ChooseCard(CardUI cardUI)
    {
        GameManager.Instance.Player.AddAbility(cardUI._skillEnum, cardUI.ablity);

        gameObject.SetActive(false);
    }
}
