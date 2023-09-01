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

    private Animator animatorEnemyMove;

    private Vector3 targetPosition;
    private Vector3 direction;
    private Quaternion newRotation;

    private void Start()
    {
        animatorEnemyMove = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        animatorEnemyMove.SetBool("Is move", isMove);

        if (!isMove)
        {
            nextPoint = random.Next(0, ponts.Length);
            isMove = true;
        }

        targetPosition = ponts[nextPoint].transform.position;
        direction = targetPosition - transform.position;
        newRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, speed * Time.deltaTime);

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition)
            isMove = false;
    }
}
