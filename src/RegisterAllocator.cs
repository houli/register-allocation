using System;
using System.Collections.Generic;

namespace Tastier {
    class RegisterAllocator {
        private static int MACHINE_REGISTERS = 5;

        public static void colour(Graph graph) {
            // Chaitin's Algorithm
            Stack<Node> stack = new Stack<Node>();
            List<Node> spillList = new List<Node>();

            // Create a copy of our graph to modify
            Graph replicaGraph = replicate(graph);
            graph.PrintGraph();

            while (replicaGraph.Size() > 0) {
                processNextNode(replicaGraph, stack, spillList);
            }

            if (spillList.Count == 0) {
                colourGraph(graph, stack);
            } else {
                // spill each node in spillList everywhere, rebuild the interference graph,
                // and repeat the procedure
            }
            Console.WriteLine("");
            graph.PrintGraph();
        }

        private static Graph replicate(Graph graph) {
            Graph replicaGraph = new Graph();
            foreach (var node in graph.nodes) {
                replicaGraph.AddNode(node);
            }
            return replicaGraph;
        }

        private static void processNextNode(Graph graph, Stack<Node> stack, List<Node> spillList) {
            Node node = graph.DegreeLtReg(MACHINE_REGISTERS);
            if (node != null) {
                stack.Push(node);
                graph.RemoveEdge(node);
            } else {
                spillList.Add(node);
                graph.RemoveEdge(node);
            }
        }

        private static void colourGraph(Graph graph, Stack<Node> stack) {
            while (stack.Count > 0) {
                int colour = 5;
                Node node = stack.Pop();
                Node graphItem = graph.FindElem(node.name);

                foreach (var x in graphItem.GetNeighbours()) {
                    foreach (var neighbour in graphItem.GetNeighbours()) {
                        if (colour == neighbour.colour) {
                            colour++;
                        }
                    }
                }
                graphItem.colour = colour;
            }
        }

        // Graph build will take a Dictionary of Tuple to List[]
        public static Graph GraphBuilder() {

            Graph graph = new Graph();
            Dictionary<string, Node> createdNodes = new Dictionary<string, Node>();
            Dictionary<string, List<string>> interferenceList = new Dictionary<string, List<string>>();
            interferenceList.Add("tuple1", (new List<string> { "G", "H"}));
            interferenceList.Add("tuple2", (new List<string> { "G", "K", "J"}));
            interferenceList.Add("tuple3", (new List<string> { "J", "H"}));
            interferenceList.Add("tuple4", (new List<string> { "J", "D"}));
            interferenceList.Add("tuple5", (new List<string> { "J", "F", "E"}));
            interferenceList.Add("tuple6", (new List<string> { "D", "B", "M"}));
            interferenceList.Add("tuple7", (new List<string> { "C", "M"}));
            interferenceList.Add("tuple8", (new List<string> { "K", "B", "D"}));
            interferenceList.Add("tuple9", (new List<string> { "F", "M"}));
            interferenceList.Add("tuple10", (new List<string> { "B", "C"}));
            interferenceList.Add("tuple11", (new List<string> { "B", "E"}));

            foreach (var item in interferenceList) {
                var interferences = item.Value;
                for (int i = 0; i < interferences.Count - 1; i++) {
                    foreach (var j in interferences) {
                        BuildEdges(graph, createdNodes, interferences[i], j);
                    }
                }
            }

            return graph;
        }

        private static void BuildEdges(Graph graph, Dictionary<string, Node> createdNodes, string a, string b) {
            Node node1, node2;
            if (a != b){
                node1 = GetNodeRef(createdNodes,a);
                node2 = GetNodeRef(createdNodes,b);
                graph.CreateEdge(node1,node2);
            }
        }

        public static Node GetNodeRef(Dictionary<string, Node> createdNodes, string val) {
            Node node;
            if (createdNodes.ContainsKey(val)) {
                node = createdNodes[val];
            } else {
                createdNodes.Add(val, new Node(val));
                node = createdNodes[val];
            }
            return node;
        }
    }
}
