using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    // List to hold references to connected nodes
    public List<Node> connectedNodes = new List<Node>();

    // Method to connect this node to another
    public void ConnectNode(Node nodeToConnect)
    {
        if (!connectedNodes.Contains(nodeToConnect))
        {
            connectedNodes.Add(nodeToConnect);
            // Optionally, also connect the other node to this one (if the connections are bidirectional)
            nodeToConnect.connectedNodes.Add(this);
        }
    }
}
