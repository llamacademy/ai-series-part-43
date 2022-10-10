using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
[RequireComponent(typeof(LineRenderer), typeof(NavMeshAgent), typeof(AgentLinkMover))]
public class PathDisplayer : MonoBehaviour
{
    [SerializeField]
    private float Resolution = 25f;
    private LineRenderer LineRenderer;
    private NavMeshAgent Agent;
    private AgentLinkMover LinkMover;

    private NavMeshPath Path;
    private List<LinkPosition> CornersThatAreLinks = new();
    private Vector3[] LastCorners;

    private struct LinkPosition : IEquatable<LinkPosition>
    {
        public Vector3 StartPosition;
        public Vector3 EndPosition;

        public bool Equals(LinkPosition Other)
        {
            return StartPosition.Equals(Other.StartPosition)
                && EndPosition.Equals(Other.EndPosition);
        }
    }

    private void Awake()
    {
        LineRenderer = GetComponent<LineRenderer>();
        Agent = GetComponent<NavMeshAgent>();
        LinkMover = GetComponent<AgentLinkMover>();
    }

    public void SetPath(NavMeshPath Path)
    {
        this.Path = Path;
        Vector3[] corners = Path.corners;
        if (LastCorners == null 
            || corners.Length != LastCorners.Length)
        {
            CornersThatAreLinks.Clear();
            for (int i = 0; i < corners.Length - 1; i++)
            {
                if (NavMesh.Raycast(
                    corners[i], 
                    corners[i + 1], 
                    out NavMeshHit hit, 
                    Agent.areaMask))
                {
                    CornersThatAreLinks.Add(new LinkPosition()
                    {
                        StartPosition = corners[i],
                        EndPosition = corners[i + 1],
                    });
                }
            }
        }
    }

    private void Update()
    {
        if (Path != null)
        {
            Vector3 yOffset = Vector3.up * Agent.height / 2f;
            Vector3[] corners = Path.corners;

            LineRenderer.positionCount = corners.Length;
            LineRenderer.SetPosition(0, transform.position + yOffset);
            int lineRendererIndex = 1;
            for (int cornerIndex = 1; cornerIndex < corners.Length; cornerIndex++)
            {
                LinkPosition linkPosition = CornersThatAreLinks.Find((item) => item.StartPosition == corners[cornerIndex]);
                if (!linkPosition.Equals(new LinkPosition()))
                {
                    LineRenderer.positionCount += Mathf.CeilToInt(Resolution);
                    int curveIndex = 0;

                    Vector3 startPosition = linkPosition.StartPosition;
                    Vector3 endPosition = linkPosition.EndPosition;
                    while (curveIndex <= Resolution)
                    {
                        float time = (curveIndex) / Resolution;
                        LineRenderer.SetPosition(
                            lineRendererIndex, 
                            Vector3.Lerp(startPosition, endPosition, time)
                                + Vector3.up * LinkMover.Curve.Evaluate(time) + yOffset);
                        lineRendererIndex++;
                        curveIndex++;
                    }
                }
                else
                {
                    LineRenderer.SetPosition(lineRendererIndex, corners[cornerIndex] + yOffset);
                    lineRendererIndex++;
                }
            }
        }
    }
}
