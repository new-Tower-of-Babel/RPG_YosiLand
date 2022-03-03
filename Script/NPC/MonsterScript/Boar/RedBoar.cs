using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBoar : Monster_Base     //boss script
{
    protected override void Dead()
    {
        base.Dead();
        StartCoroutine(DropCoroutine());

    }
    private IEnumerator DropCoroutine()
    {
        int _random = Random.Range(0, 8);
        switch (_random)
        {
            case 0:
                break;
            case 1:
                Instantiate(hP_Potion1, monsterPrefab.transform.position + Vector3.up, Quaternion.identity);
                break;
            case 2:
                Instantiate(sP_Potion1, monsterPrefab.transform.position + Vector3.up, Quaternion.identity);
                break;
            case 3:
                Instantiate(combi_Ingre1, monsterPrefab.transform.position + Vector3.up, Quaternion.identity);
                break;
            case 4:
                Instantiate(hP_Potion1, monsterPrefab.transform.position + Vector3.up, Quaternion.identity);
                Instantiate(sP_Potion1, monsterPrefab.transform.position + Vector3.up, Quaternion.identity);
                break;
            case 5:
                Instantiate(hP_Potion1, monsterPrefab.transform.position + Vector3.up, Quaternion.identity);
                Instantiate(combi_Ingre1, monsterPrefab.transform.position + Vector3.up, Quaternion.identity);
                break;
            case 6:
                Instantiate(sP_Potion1, monsterPrefab.transform.position + Vector3.up, Quaternion.identity);
                Instantiate(combi_Ingre1, monsterPrefab.transform.position + Vector3.up, Quaternion.identity);
                break;
            case 7:
                Instantiate(hP_Potion1, monsterPrefab.transform.position + Vector3.up, Quaternion.identity);
                Instantiate(sP_Potion1, monsterPrefab.transform.position + Vector3.up, Quaternion.identity);
                Instantiate(combi_Ingre1, monsterPrefab.transform.position + Vector3.up, Quaternion.identity);
                break;
        }
        yield return new WaitForSeconds(2);
        Destroy(monsterPrefab);
    }
}
