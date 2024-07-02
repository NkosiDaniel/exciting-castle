using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PuzzleTween : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject movingBlockRight;
    [SerializeField] GameObject movingBlockLeft;

    [SerializeField] Vector3 rightBlockEndPos = new Vector3(2.37f, -2.52f, 0);
    [SerializeField] Vector3 leftBlockEndPos = new Vector3(-1.5f, -0.55f, 0);
    
    
    private void Start() {
        moveBlocks(1f);
    }
    public void moveBlocks(float moveTime) 
    {
        var sequence = DOTween.Sequence();
        movingBlockLeft.transform.DOLocalMove(leftBlockEndPos, moveTime, false).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        movingBlockRight.transform.DOLocalMove(rightBlockEndPos, moveTime, false).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }


}
