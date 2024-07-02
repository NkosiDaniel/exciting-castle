using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class lifeCrystals : MonoBehaviour
{
    [SerializeField] bool lifeOn = true;
    [SerializeField] Sprite liveCrystal;
    [SerializeField] Sprite deadCrystal;



    private void Start() {
        setCrystal();
    }
    public void setCrystal() 
    {
        var sequence = DOTween.Sequence();

            if(lifeOn) {
                GetComponent<Image>().sprite = liveCrystal;
                transform.localScale = new Vector3(3.5f, 3.5f, 1);
                sequence.Kill();
                transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0), 2f, 1, 0.2f).SetLoops(-1);
            }
            else {
                GetComponent<Image>().sprite = deadCrystal;
                transform.localScale = new Vector3(2.5f, 2.5f, 1);
                sequence.Kill();
                transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0), 1f, 5, 0).SetLoops(-1);
            }
        }

    public void changeCrystal(bool activation) 
    {
        lifeOn = activation;
        setCrystal();
    }
}
    
