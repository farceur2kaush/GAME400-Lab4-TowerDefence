using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100f;
    public float health = 100f;
    public float moveSpeed = 3f;

    public int goldDrop = 10;
    public int pathIndex = 0;

    private int wayPointIndex = 0;

    public float timeEnemyStaysFrozenInSeconds = 2f;
    public bool frozen;
    private float freezeTimer;

    void Start()
    {
        EnemyManager.Instance.RegisterEnemy(this);
    }
    void OnGotToLastWayPoint()
    {
        GameManager.Instance.OnEnemyEscape();
        Die();
    }
  
    public void TakeDamage(float amountOfDamage)
    {
        health -= amountOfDamage;

        if (health <= 0)
        {
            DropGold();
            Die();
        }
    }

    void DropGold()
    {
        GameManager.Instance.gold += goldDrop;
    }

    void Die()
    {
        if (gameObject != null)
        {
            //1
            EnemyManager.Instance.UnRegister(this);
            //2
            gameObject.AddComponent<AutoScaler>().scaleSpeed = -2;
            //3
            enabled = false;
            //4
            Destroy(gameObject, 0.3f);
        }
    }

    //1
    public void Freeze()
    {
        if (!frozen)
        {
            //2
            frozen = true;
            moveSpeed /= 2;
        }
    }
    //3
    void Defrost()
    {
        freezeTimer = 0f;
        frozen = false;
        moveSpeed *= 2;
    }

    void Update()
    {
        //1
        if (wayPointIndex <
        WayPointManager.Instance.Paths[pathIndex].WayPoints.Count)
        {
            UpdateMovement();
        }
        else
        { // 2
            OnGotToLastWayPoint();
        }
        //1
        if (frozen)
        {
            //2
            freezeTimer += Time.deltaTime;
            //3
            if (freezeTimer >= timeEnemyStaysFrozenInSeconds)
            {
                Defrost();
            }
        }
    }
    private void UpdateMovement()
    {
        //3
        Vector3 targetPosition =
        WayPointManager.Instance.Paths[pathIndex].WayPoints[wayPointIndex].position;
        //4
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        //5
        transform.localRotation = UtilityMethods.SmoothlyLook(transform, targetPosition);
        //6
        if (Vector3.Distance(transform.position, targetPosition) < .1f)
        {
            wayPointIndex++;
        }
    }
}
