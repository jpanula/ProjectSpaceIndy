using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Path : MonoBehaviour
{
    public Node[] Nodes;
    public Color GizmoColor;

    public Node GetClosestNode(Vector3 position)
    {
        var closestDistance = float.PositiveInfinity;
        Node closestNode = Nodes[0];
        foreach (var node in Nodes)
        {
            var newDistance = Vector3.Distance(position, node.GetPosition());
            if (Vector3.Distance(position, node.GetPosition()) < closestDistance)
            {
                closestDistance = newDistance;
                closestNode = node;
            }
        }

        return closestNode;
    }

    public Vector3 GetNodePosition(Node node)
    {
        return node.GetPosition();
    }

    public Node GetNextNode(Node current)
    {
        var nextNode = Nodes[0];
        for (int i = 0; i < Nodes.Length; i++)
        {
            if (current == Nodes[i])
            {
                nextNode = Nodes[(i + 1) % Nodes.Length];
            }
        }

        return nextNode;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = GizmoColor;
        for (int i = 0; i < Nodes.Length; i++)
        {
            if (Nodes[i] != null && Nodes[(i + 1) % Nodes.Length] != null)
            {
                Gizmos.DrawLine(Nodes[i].GetPosition(), Nodes[(i + 1) % Nodes.Length].GetPosition());
            }
        }
    }
}
