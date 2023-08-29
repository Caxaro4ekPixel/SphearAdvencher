using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private GameObject[] ponts;
    [SerializeField] private float speed = 20f;

    private bool isMove = false;
    private int nextPoint;
    private System.Random random = new System.Random();
    private Transform nextPosition;


    private void Update()
    {
        //������ �������� �� ��������
        gameObject.GetComponent<Animator>().SetBool("Is move", isMove);

        //���� �������� ���, �� �������� ����� �����
        if (!isMove)
        {
            nextPoint = random.Next(0, ponts.Length);
            isMove = true;
        }

        //������������ � ������� �����
        Vector3 newDir = Vector3.RotateTowards(transform.forward, (ponts[nextPoint].transform.position - transform.position), speed * Time.deltaTime, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);

        //������ ������
        transform.position = Vector3.MoveTowards(transform.position, ponts[nextPoint].transform.position, speed * Time.deltaTime);
        
        //�������� ���������� ����� � ���� �� ��� ������ �� ������������� ��������
        nextPosition = ponts[nextPoint].transform;
        if (transform.position.x == nextPosition.position.x && transform.position.y == nextPosition.position.y && transform.position.z == nextPosition.position.z)
            isMove = false;
    }
}
