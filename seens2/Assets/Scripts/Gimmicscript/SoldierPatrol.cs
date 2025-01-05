using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SoldierPatrol : MonoBehaviour
{
    public Transform[] patrolPoints; // パトロールポイント
    public Transform player; // プレイヤーのTransform
    public float chaseDistance = 5f; // 追跡開始距離
    public float returnDistance = 20f; // 接触距離
    public float waitTimeAtPoint = 2f; // パトロールポイントでの待機時間
    public float investigateDuration = 5f; // 調査時間
    public float patrolSpeed = 3.5f; // パトロール時の速度
    public float chaseSpeed = 5.5f; // 追跡時の速度

    public string scenenamae;

    public NavMeshAgent agent;
    private int currentPointIndex = 0;
    private bool isInvestigating = false; // ツボを調査中かどうか
    private Vector3 investigatePosition; // 調査対象の位置
    private Vector3 soldierStartPosition; // 兵士の初期位置
    private Vector3 playerStartPosition; // プレイヤーの初期位置
    private int contactCount = 0; // プレイヤーとの接触回数



    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        soldierStartPosition = transform.position;
        playerStartPosition = player.position;

        if (patrolPoints.Length > 0)
        {
            MoveToNextPoint();
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < chaseDistance && !isInvestigating)
        {
            // プレイヤーを追いかける
            agent.speed = chaseSpeed;
            agent.SetDestination(player.position);

            // 接触チェック
            if (distanceToPlayer < returnDistance)
            {
                HandleContact();
            }
        }
        else if (isInvestigating)
        {
            // ツボ調査ロジック
            if (Vector3.Distance(transform.position, investigatePosition) < 1.0f)
            {
                StartCoroutine(InvestigateAroundPosition());
            }
        }
        else
        {
            // 通常のパトロール
            agent.speed = patrolSpeed;
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                MoveToNextPoint();
            }
        }
    }

    private void MoveToNextPoint()
    {
        if (patrolPoints.Length == 0 || isInvestigating) return;

        agent.SetDestination(patrolPoints[currentPointIndex].position);
        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
    }

    public void OnPotPlaced(Vector3 position)
    {
        // ツボが置かれたときに呼び出される
        isInvestigating = true;

        // ツボ周辺のランダムなポイントを計算
        Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        investigatePosition = position + randomOffset;

        agent.SetDestination(investigatePosition);
        Debug.Log("ツボ周辺を調査します: " + investigatePosition);
    }

    private IEnumerator InvestigateAroundPosition()
    {
        for (int i = 0; i < 3; i++) // ツボ周辺を3回ランダムに移動
        {
            Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            agent.SetDestination(investigatePosition + randomOffset);

            while (agent.pathPending || agent.remainingDistance > 0.5f)
            {
                yield return null; // 目標地点に到達するまで待機
            }

            yield return new WaitForSeconds(1f); // 移動後に1秒待機
        }

        // 調査終了後、パトロールを再開
        isInvestigating = false;
        MoveToNextPoint();
    }

    private void HandleContact()
    {
        // 兵士を初期位置に戻す
        transform.position = soldierStartPosition;

        // プレイヤーを初期位置に戻す
        if (player.TryGetComponent<CharacterController>(out CharacterController characterController))
        {
            // CharacterControllerの場合
            characterController.enabled = false; // 一時的に無効化
            player.position = playerStartPosition;
            characterController.enabled = true; // 再有効化
        }
        else if (player.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            // Rigidbodyの場合
            rb.velocity = Vector3.zero; // 速度をリセット
            rb.angularVelocity = Vector3.zero; // 回転をリセット
            player.position = playerStartPosition;
        }
        else
        {
            // Transformを直接変更
            player.position = playerStartPosition;
        }

        contactCount++;
        Debug.Log($"接触しました！現在の接触回数: {contactCount}");

        // 接触回数が3回に達したらシーン遷移
        if (contactCount >= 3)
        {
            Debug.Log("シーン遷移します...");
            SceneManager.LoadScene(scenenamae); // 遷移するシーン名を指定
        }
    }

}
