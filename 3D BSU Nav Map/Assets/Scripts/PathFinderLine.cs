using UnityEngine;

public class PathFinderLine : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private Transform Player;
    [SerializeField] private LineRenderer Path;

    private Controller closestNodeTarget;

    private void Start()
    {
        closestNodeTarget = Player.GetComponent<Controller>();
        // Initialize the LineRenderer
        if (Path != null)
        {
            Path.positionCount = 2; // Only need two points for a single straight line
            Path.startWidth = 0.05f; // Adjust the start width of the line
            Path.endWidth = 0.05f;   // Adjust the end width of the line
            // Optionally, set other LineRenderer properties such as materials here
        }
    }

    private void Update()
    {
        Target = closestNodeTarget.closestNode.transform;

        DrawPath();
    }

    void DrawPath()
    {
        if (Target != null && Player != null && Path != null)
        {
            // Set the positions for the line renderer to draw the line
            Path.SetPosition(0, Player.position);
            Path.SetPosition(1, Target.position);
        }
    }
}
