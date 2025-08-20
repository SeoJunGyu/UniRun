using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    //1. 스프라이트 렌더러를 이용해서 너비 구하기
    //2. 콜라이더를 이용해서 너비 구하기
    private float width;

    private void Start()
    {
        //렌더러
        var sr = GetComponent<SpriteRenderer>();
        //부정확한 계산이라서 오차가 발생할 수 있다. (해상도나 프레임에 따라서, 알아차리기 힘들다.)
        width = sr.sprite.rect.width / sr.sprite.pixelsPerUnit; //유니티 단위로보는 너비를 구할 수 있다.

        //콜라이더
        //var col = GetComponent<BoxCollider2D>();
        //width = col.size.x;
    }

    private void Update()
    {
        if(transform.position.x < -width)
        {
            transform.position += new Vector3(width * 2, 0f, 0f);
        }
    }
}
