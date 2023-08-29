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
        //Меняем анимацию на двежение
        gameObject.GetComponent<Animator>().SetBool("Is move", isMove);

        //Если движения нет, то выбираем новую точку
        if (!isMove)
        {
            nextPoint = random.Next(0, ponts.Length);
            isMove = true;
        }

        //Поворачиваем в сторону точки
        Vector3 newDir = Vector3.RotateTowards(transform.forward, (ponts[nextPoint].transform.position - transform.position), speed * Time.deltaTime, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);

        //Движем объект
        transform.position = Vector3.MoveTowards(transform.position, ponts[nextPoint].transform.position, speed * Time.deltaTime);
        
        //Получаем координаты точки и если мы уже пришли то останавливаем движение
        nextPosition = ponts[nextPoint].transform;
        if (transform.position.x == nextPosition.position.x && transform.position.y == nextPosition.position.y && transform.position.z == nextPosition.position.z)
            isMove = false;
    }
}
