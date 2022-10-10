using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent), typeof(AgentLinkMover))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Camera Camera = null;
    private NavMeshAgent Agent;
    [SerializeField]
    private Animator Animator = null;
    [SerializeField]
    private LayerMask LayerMask;
    [SerializeField]
    private PathDisplayer PathDisplayer;
    private AgentLinkMover LinkMover;

    private const string MotionSpeedParameter = "MotionSpeed";
    private const string SpeedParameter = "Speed";
    private const string JumpParameter = "Jump";
    private const string GroundedParameter = "Grounded";

    private RaycastHit Hit;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        LinkMover = GetComponent<AgentLinkMover>();

        LinkMover.OnLinkStart += HandleLinkStart;
        LinkMover.OnLinkEnd += HandleLinkEnd;
    }

    private void HandleLinkStart(OffMeshLinkMoveMethod MoveMethod)
    {
        if (MoveMethod != OffMeshLinkMoveMethod.Teleport && MoveMethod != OffMeshLinkMoveMethod.NormalSpeed)
        {
            Animator.SetBool(JumpParameter, true);
            Animator.SetBool(GroundedParameter, false);
        }
    }

    private void HandleLinkEnd(OffMeshLinkMoveMethod MoveMethod)
    {
        if (MoveMethod != OffMeshLinkMoveMethod.Teleport && MoveMethod != OffMeshLinkMoveMethod.NormalSpeed)
        {
            Animator.SetBool(JumpParameter, false);
            Animator.SetBool(GroundedParameter, true);
        }
    }

    private void OnFootstep() { }

    private void OnLand() { }

    private void Update()
    {
        if (Application.isFocused && Mouse.current.leftButton.wasReleasedThisFrame)
        {
            Ray ray = Camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out Hit, float.MaxValue, LayerMask.value))
            {
                Agent.SetDestination(Hit.point);
                //StartCoroutine(SetPath()); 
            }
        }

        // if you want to manage the corners of a path and not have to get the path every frame, you can 
        // comment out this if and uncomment the StartCoroutine above and the Coroutine function below.
        if (PathDisplayer != null && Agent.hasPath)
        {
            PathDisplayer.SetPath(Agent.path);
        }

        if (!Agent.isOnOffMeshLink)
        {
            Animator.SetFloat(MotionSpeedParameter, Mathf.Clamp01(Agent.velocity.magnitude));
            Animator.SetFloat(SpeedParameter, Agent.velocity.magnitude);
        }
    }

    //private IEnumerator SetPath()
    //{
    //    yield return new WaitUntil(() => Agent.hasPath && !Agent.pathPending);
    //    if (PathDisplayer != null && Agent.hasPath)
    //    {
    //        PathDisplayer.SetPath(Agent.path);
    //    }
    //}
}