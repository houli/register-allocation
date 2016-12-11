using System;
using System.Collections.Generic;
using System.Linq;

namespace Tastier {
    class Node {
        public string name {
            get;
            set;
        }

        public int colour {
            get;
            set;
        }

        private List<Node> neighbours {
            get;
            set;
        }

        public Node(string name) {
            this.name = name;
            colour = -1;
            neighbours = new List<Node>();
        }

        public void AddNeighbour(Node node) {
            neighbours.Add(node);
        }

        public void RemoveNode(Node node) {
            if (neighbours.Contains(node)) {
                neighbours.Remove(node);
            }
        }

        public List<Node> GetNeighbours() {
            return neighbours;
        }

        public int NumNeighbours() {
            return neighbours.Count;
        }
    }

    class Graph {
        public List<Node> nodes {
            get;
        }

        public Graph() {
            this.nodes = new List<Node>();
        }

        public int Size() {
            return this.nodes.Count;
        }

        public void AddNode(Node node) {
            Node copy = new Node(node.name);
            List<Node> neighbours = node.GetNeighbours();
            foreach (var neigbour in neighbours) {
                copy.AddNeighbour(neigbour);
            }
            nodes.Add(copy);
        }

        public void CreateEdge(Node Node1, Node Node2) {
            if (!nodes.Contains(Node1)) {
                nodes.Add(Node1);
            }
            if (!nodes.Contains(Node2)) {
                nodes.Add(Node2);
            }
            Node1.AddNeighbour(Node2);
            Node2.AddNeighbour(Node1);
        }

        public void RemoveEdge(Node toBeRemoved) {
            foreach (var node in nodes) {
                node.RemoveNode(toBeRemoved);
            }
            if (nodes.Contains(toBeRemoved)) {
                nodes.Remove(toBeRemoved);
            }
        }

        public void PrintGraph() {
            foreach (var node in nodes) {
                Console.Write($"Vertex: {node.name} Colour: {node.colour} Edges:");
                foreach (var neighbour in node.GetNeighbours()) {
                    Console.Write($"| {neighbour.name} |");
                }
                Console.WriteLine("");
            }
        }

        public Node FindElem(string name) {
            foreach (var node in nodes) {
                if (node.name == name) {
                    return node;
                }
            }
            return null;
        }


        public Node DegreeLtReg(int val) {
            int i = nodes.Count - 1;
            int[] temp = new int[nodes.Count];
            foreach (var node in nodes) {
                temp[i] = node.NumNeighbours();
                i--;
            }

            int degree = temp.Min();

            if (degree < val) {
                int lowestNode = Array.IndexOf(temp, temp.Min());
                return nodes.ElementAt(lowestNode);
            } else {
                return null;
            }
        }
    }
}
