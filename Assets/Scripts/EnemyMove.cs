using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private GameObject[] ponts;
    [SerializeField] private float speed = 20f;

    private bool is_move = false;
    private int nextPoint;
    private System.Random random = new System.Random();
    private Transform nextPosition;


    private void Update()
    {
        //������ �������� �� ��������
        this.gameObject.GetComponent<Animator>().SetBool("Is move", is_move);

        //���� �������� ���, �� �������� ����� �����
        if (!is_move)
        {
            nextPoint = random.Next(0, ponts.Length);
            is_move = true;
        }

        //������������ � ������� �����
        Vector3 newDir = Vector3.RotateTowards(transform.forward, (ponts[nextPoint].transform.position - transform.position), speed * Time.deltaTime, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);

        //������ ������
        transform.position = Vector3.MoveTowards(transform.position, ponts[nextPoint].transform.position, speed * Time.deltaTime);
        
        //�������� ���������� ����� � ���� �� ��� ������ �� ������������� ��������
        nextPosition = ponts[nextPoint].transform;
        if (this.transform.position.x == nextPosition.position.x && this.transform.position.y == nextPosition.position.y && this.transform.position.z == nextPosition.position.z)
            is_move = false;
    }
}
