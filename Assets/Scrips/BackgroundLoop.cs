using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    //1. ��������Ʈ �������� �̿��ؼ� �ʺ� ���ϱ�
    //2. �ݶ��̴��� �̿��ؼ� �ʺ� ���ϱ�
    private float width;

    private void Start()
    {
        //������
        var sr = GetComponent<SpriteRenderer>();
        //����Ȯ�� ����̶� ������ �߻��� �� �ִ�. (�ػ󵵳� �����ӿ� ����, �˾������� �����.)
        width = sr.sprite.rect.width / sr.sprite.pixelsPerUnit; //����Ƽ �����κ��� �ʺ� ���� �� �ִ�.

        //�ݶ��̴�
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
