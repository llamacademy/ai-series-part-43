using Unity.AI.Navigation;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AgentLinkMover))]
public class AgentLinkMoverEditor : Editor
{
    private NavMeshLink[] Links;

    private float Resolution = 25;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Resolution = EditorGUILayout.FloatField("Arc Display Resolution", Resolution);
    }

    private void OnEnable()
    {
        Links = GameObject.FindObjectsOfType<NavMeshLink>();
    }

    private void OnSceneGUI()
    {
        AgentLinkMover linkMover = (AgentLinkMover)target;

        if (linkMover != null)
        {
            foreach (NavMeshLink link in Links)
            {
                AgentLinkMover.LinkTraversalConfiguration linkTraversalConfiguration = linkMover.NavMeshLinkTraversalTypes.Find(item => item.AreaType == link.area);
                if (linkTraversalConfiguration == null)
                {
                    linkTraversalConfiguration = new AgentLinkMover.LinkTraversalConfiguration()
                    {
                        AreaType = link.area,
                        MoveMethod = linkMover.DefaultMoveMethod
                    };
                }

                switch (linkTraversalConfiguration.MoveMethod)
                {
                    case OffMeshLinkMoveMethod.Teleport:
                        DrawTeleport(link, linkMover);
                        break;
                    case OffMeshLinkMoveMethod.NormalSpeed:
                        DrawNormalSpeed(link, linkMover);
                        break;
                    case OffMeshLinkMoveMethod.Parabola:
                        DrawParabola(link, linkMover);
                        break;
                    case OffMeshLinkMoveMethod.Curve:
                        DrawCurve(link, linkMover);
                        break;
                }
            }
        }
    }

    private void DrawCurve(NavMeshLink link, AgentLinkMover linkMover)
    {
        Handles.color = Color.black;
        Vector3 startPoint = link.startPoint + link.transform.position;
        Vector3 endPoint = link.endPoint + link.transform.position;
        for (int i = 1; i < Resolution; i++)
        {
            Handles.DrawLine(
                Vector3.Lerp(startPoint, endPoint, (i - 1) / Resolution) 
                    + Vector3.up * linkMover.Curve.Evaluate((i - 1) / Resolution),
                Vector3.Lerp(startPoint, endPoint, i / Resolution)
                    + Vector3.up * linkMover.Curve.Evaluate(i / Resolution)
            );
        }
    }

    private void DrawParabola(NavMeshLink link, AgentLinkMover linkMover)
    {
        Handles.color = Color.black;
        Vector3 startPoint = link.startPoint + link.transform.position;
        Vector3 endPoint = link.endPoint + link.transform.position;
        for (int i = 1; i < Resolution; i++)
        {
            float lastFrameTime = (i - 1) / Resolution;
            float currentFrameTime = i / Resolution;
            float yOffset = linkMover.ParabolaHeight * (currentFrameTime - currentFrameTime * currentFrameTime);
            float lastYOffset = linkMover.ParabolaHeight * (lastFrameTime - lastFrameTime * lastFrameTime);
            Handles.DrawLine(
                Vector3.Lerp(startPoint, endPoint, lastFrameTime)
                    + Vector3.up * lastYOffset,
                Vector3.Lerp(startPoint, endPoint, currentFrameTime)
                    + Vector3.up * yOffset
            );
        }
    }

    private void DrawNormalSpeed(NavMeshLink link, AgentLinkMover linkMover)
    {
        Handles.color = Color.black;
        Vector3 startPoint = link.startPoint + link.transform.position;
        Vector3 endPoint = link.endPoint + link.transform.position;
        for (int i = 1; i < Resolution; i++)
        {
            Handles.DrawLine(
                Vector3.Lerp(startPoint, endPoint, (i - 1) / Resolution),
                Vector3.Lerp(startPoint, endPoint, i / Resolution)
            );
        }
    }

    private void DrawTeleport(NavMeshLink link, AgentLinkMover linkMover)
    {
        Vector3 startPoint = link.startPoint + link.transform.position;
        Vector3 endPoint = link.endPoint + link.transform.position;
        for (int i = 1; i < Resolution; i++)
        {
            Handles.color = Color.green;
            Handles.DrawSolidDisc(
                startPoint, Vector3.up, 0.25f
            );
            Handles.color = Color.black;
            Handles.DrawSolidDisc(
                endPoint, Vector3.up, 0.25f
            );
        }
    }
}
