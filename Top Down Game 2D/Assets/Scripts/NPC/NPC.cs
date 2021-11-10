using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float speed;

    private float initialSpeed;
    private int index;
    private Animator anim;

    public List<Transform> paths = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        initialSpeed = speed;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(DialogueControl.instance.IsShowing)
        {
            speed = 0;
            anim.SetBool("isWalking", false);
        }
        else
        {
            speed = initialSpeed;
            anim.SetBool("isWalking", true);
        }
        transform.position = Vector2.MoveTowards(transform.position, paths[index].position, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, paths[index].position) < 0.1f)
        {
            if(index < paths.Count - 1)
            {
                // index++; // sempre vai pro proximo
                index = Random.Range(0, paths.Count);
            }
            else
            {
                index = 0;
            }
        }
        // se tiver indo pra direita valor +, esquerda valor -, no eixo X
        Vector2 direction = paths[index].position - transform.position;
        if(direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

        if (direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }
}
