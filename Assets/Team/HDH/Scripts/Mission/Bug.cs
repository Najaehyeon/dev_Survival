using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    [field: SerializeField] public bool isDead;
    [field: SerializeField] public float speed;
    [field: SerializeField] public Vector2 boarderMargin;
    [field: SerializeField] private float respawnTime;

    [HideInInspector] public Rect FieldRange;

    private Coroutine respawnCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = SetRandomPosition();
        transform.rotation = SetRandomRotation();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (!IsInField() && respawnCoroutine == null)
        {
            Debug.Log("Respawn");
            respawnCoroutine = StartCoroutine(RespawnBug());
        }

    }

    Vector3 SetRandomPosition()
    {
        float randomX = Random.Range(boarderMargin.x, FieldRange.width - boarderMargin.x);
        float randomY = Random.Range(boarderMargin.y, FieldRange.height - boarderMargin.y);

        return new Vector3(randomX, randomY, 0);
    }

    Quaternion SetRandomRotation()
    {
        float randomZ = Random.Range(-180, 180);
        return Quaternion.Euler(new Vector3(0, 0, randomZ));
    }

    bool IsInField()
    {
        if (transform.localPosition.x <= 0 || transform.localPosition.x >= FieldRange.width) return false;
        if (transform.localPosition.y <= 0 || transform.localPosition.y >= FieldRange.height) return false;

        return true;
    }

    IEnumerator RespawnBug()
    {
        yield return new WaitForSeconds(respawnTime);

        transform.localPosition = SetRandomPosition();
        transform.rotation = SetRandomRotation();
        respawnCoroutine = null;
    }

    


}
