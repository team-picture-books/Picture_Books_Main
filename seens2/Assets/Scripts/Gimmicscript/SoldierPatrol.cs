using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SoldierPatrol : MonoBehaviour
{
    public Transform[] patrolPoints; // �p�g���[���|�C���g
    public Transform player; // �v���C���[��Transform
    public float chaseDistance = 5f; // �ǐՊJ�n����
    public float returnDistance = 20f; // �ڐG����
    public float waitTimeAtPoint = 2f; // �p�g���[���|�C���g�ł̑ҋ@����
    public float investigateDuration = 5f; // ��������
    public float patrolSpeed = 3.5f; // �p�g���[�����̑��x
    public float chaseSpeed = 5.5f; // �ǐՎ��̑��x

    public string scenenamae;

    public NavMeshAgent agent;
    private int currentPointIndex = 0;
    private bool isInvestigating = false; // �c�{�𒲍������ǂ���
    private Vector3 investigatePosition; // �����Ώۂ̈ʒu
    private Vector3 soldierStartPosition; // ���m�̏����ʒu
    private Vector3 playerStartPosition; // �v���C���[�̏����ʒu
    private int contactCount = 0; // �v���C���[�Ƃ̐ڐG��



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
            // �v���C���[��ǂ�������
            agent.speed = chaseSpeed;
            agent.SetDestination(player.position);

            // �ڐG�`�F�b�N
            if (distanceToPlayer < returnDistance)
            {
                HandleContact();
            }
        }
        else if (isInvestigating)
        {
            // �c�{�������W�b�N
            if (Vector3.Distance(transform.position, investigatePosition) < 1.0f)
            {
                StartCoroutine(InvestigateAroundPosition());
            }
        }
        else
        {
            // �ʏ�̃p�g���[��
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
        // �c�{���u���ꂽ�Ƃ��ɌĂяo�����
        isInvestigating = true;

        // �c�{���ӂ̃����_���ȃ|�C���g���v�Z
        Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        investigatePosition = position + randomOffset;

        agent.SetDestination(investigatePosition);
        Debug.Log("�c�{���ӂ𒲍����܂�: " + investigatePosition);
    }

    private IEnumerator InvestigateAroundPosition()
    {
        for (int i = 0; i < 3; i++) // �c�{���ӂ�3�񃉃��_���Ɉړ�
        {
            Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            agent.SetDestination(investigatePosition + randomOffset);

            while (agent.pathPending || agent.remainingDistance > 0.5f)
            {
                yield return null; // �ڕW�n�_�ɓ��B����܂őҋ@
            }

            yield return new WaitForSeconds(1f); // �ړ����1�b�ҋ@
        }

        // �����I����A�p�g���[�����ĊJ
        isInvestigating = false;
        MoveToNextPoint();
    }

    private void HandleContact()
    {
        // ���m�������ʒu�ɖ߂�
        transform.position = soldierStartPosition;

        // �v���C���[�������ʒu�ɖ߂�
        if (player.TryGetComponent<CharacterController>(out CharacterController characterController))
        {
            // CharacterController�̏ꍇ
            characterController.enabled = false; // �ꎞ�I�ɖ�����
            player.position = playerStartPosition;
            characterController.enabled = true; // �ėL����
        }
        else if (player.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            // Rigidbody�̏ꍇ
            rb.velocity = Vector3.zero; // ���x�����Z�b�g
            rb.angularVelocity = Vector3.zero; // ��]�����Z�b�g
            player.position = playerStartPosition;
        }
        else
        {
            // Transform�𒼐ڕύX
            player.position = playerStartPosition;
        }

        contactCount++;
        Debug.Log($"�ڐG���܂����I���݂̐ڐG��: {contactCount}");

        // �ڐG�񐔂�3��ɒB������V�[���J��
        if (contactCount >= 3)
        {
            Debug.Log("�V�[���J�ڂ��܂�...");
            SceneManager.LoadScene(scenenamae); // �J�ڂ���V�[�������w��
        }
    }

}
