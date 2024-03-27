using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform Prefab;
    [SerializeField]
    private Transform Player;
    [SerializeField]
    private LineRenderer Path;
    [SerializeField]
    private float PathHeightOffset = 1.25f;
    [SerializeField]
    private float SpawnHeightOffset = 1.5f;
    [SerializeField]
    private float PathUpdateSpeed = 0.25f;

    private NavMeshTriangulation Triangulation;
    private Coroutine DrawPathCoroutine;

    private void Awake()
    {
        Triangulation = NavMesh.CalculateTriangulation();
    }

    private void Start()
    {
        DrawPathCoroutine = StartCoroutine(DrawPathToCollectable());
    }



    private IEnumerator DrawPathToCollectable()
    {
        WaitForSeconds Wait = new WaitForSeconds(PathUpdateSpeed);
        NavMeshPath path = new NavMeshPath();

        while (Prefab != null)
        {
            if (NavMesh.CalculatePath(Player.position, Prefab.transform.position, NavMesh.AllAreas, path))
            {
                Path.positionCount = path.corners.Length;

                for (int i = 0; i < path.corners.Length; i++)
                {
                    Path.SetPosition(i, path.corners[i] + Vector3.up * PathHeightOffset);
                }
            }
            else
            {
                Debug.LogError($"Unable to calculate a path on the NavMesh between {Player.position} and {Prefab.transform.position}!");
            }

            yield return Wait;
        }
    }
}
