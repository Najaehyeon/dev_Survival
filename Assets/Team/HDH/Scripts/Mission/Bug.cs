using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    [field: SerializeField] public bool isDead;

    public Rect FieldRange;

    private Quaternion bugRotation;

    // Start is called before the first frame update
    void Start()
    {
        SetRandomPosition();
        transform.rotation = SetRandomRotation();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SetRandomPosition()
    {
        float randomX = Random.Range(-FieldRange.width / 2, FieldRange.width / 2);
        float randomY = Random.Range(-FieldRange.height / 2, FieldRange.height / 2);

        transform.position = new Vector3(randomX, randomY);
    }

    Quaternion SetRandomRotation()
    {
        float randomZ = Random.Range(-180, 180);
        return Quaternion.Euler(new Vector3(0, 0, randomZ));
    }

    bool IsInField()
    {
        if (transform.position.x < -FieldRange.width / 2 || transform.position.x > FieldRange.width / 2) return false;
        if (transform.position.y < -FieldRange.height / 2 || transform.position.y > FieldRange.height / 2) return false;

        return true;
    }

    


}
