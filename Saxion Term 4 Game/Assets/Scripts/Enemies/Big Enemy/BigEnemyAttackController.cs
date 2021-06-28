using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemyAttackController : MonoBehaviour
{
    GameObject player;

    public GameObject armsCilincer;
    public GameObject sawBladeBox;
    public GameObject head;

    List<GameObject> sawBlades = new List<GameObject>();

    public float armRotationSpeed;
    public float armRotationStartDistance;

    void Start()
    {
        player = GameObject.Find("Player");
        armRotationSpeed = 0.75f;
        armRotationStartDistance = 30;

        foreach (Transform childObj in transform.GetChild(3).transform.GetChild(2).transform.GetChild(0).transform.GetChild(1))
        {
            if (childObj.name == "sawBlades")
            {
                sawBlades.Add(childObj.GetChild(0).gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        RotateSawBlades();
        SawBladeAttack();     
    }

    void SawBladeAttack()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 40)
        {
            if (player.transform.position.y > 5 && player.transform.position.y > sawBladeBox.transform.position.y && sawBladeBox.transform.position.y < 12)
            {
                MoveArmsUp();
            }
            if (player.transform.position.y < sawBladeBox.transform.position.y && sawBladeBox.transform.position.y > 3)
            {
                MoveArmsDown();
            }
        }
        else if(sawBladeBox.transform.position.y > 3)
        {
            MoveArmsDown();
        }
    }

    void MoveArmsUp()
    {
        armsCilincer.transform.Rotate(0, armRotationSpeed, 0);
        sawBladeBox.transform.Rotate(0, -armRotationSpeed, 0);
    }

    void MoveArmsDown()
    {
        armsCilincer.transform.Rotate(0, -1, 0);
        sawBladeBox.transform.Rotate(0, 1, 0);
    }

    void RotateSawBlades()
    {
        foreach (GameObject sawBlade in sawBlades)
        {
            sawBlade.transform.Rotate(5, 0, 0);
        }
    }
}
