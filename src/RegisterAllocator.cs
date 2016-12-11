using System;
using System.Collections.Generic;

namespace Tastier {
    class RegisterAllocator {
        private static int MACHINE_REGISTERS = 5;

        public static void colour(Graph graph) {
            // Chaitin's Algorithm
            Stack<Node> stack = new Stack<Node>();
            List<Node> spillList = new List<Node>();
            Graph replicaGraph = replicate(graph);
            graph.PrintGraph();

            while (replicaGraph.Size() > 0) {
                processNextNode(replicaGraph, stack, spillList);
            }

            if (spillList.Count == 0) {
                colourGraph(graph, stack);
            } else {
                // Interference magic to happen here :)
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
                // Spill thing
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

        public static Graph example() {
            // Lecture Example
            Node g = new Node("G");
            Node h = new Node("H");
            Node f = new Node("F");
            Node e = new Node("E");
            Node m = new Node("M");
            Node b = new Node("B");
            Node c = new Node("C");
            Node d = new Node("D");
            Node k = new Node("K");
            Node j = new Node("J");

            // Edges
            Graph graph = new Graph();
            graph.CreateEdge(g, h);
            graph.CreateEdge(g, k);
            graph.CreateEdge(g, j);
            graph.CreateEdge(j, k);
            graph.CreateEdge(j, h);
            graph.CreateEdge(j, d);
            graph.CreateEdge(j, e);
            graph.CreateEdge(j, f);
            graph.CreateEdge(k, d);
            graph.CreateEdge(k, b);
            graph.CreateEdge(b, c);
            graph.CreateEdge(b, e);
            graph.CreateEdge(m, b);
            graph.CreateEdge(m, d);
            graph.CreateEdge(m, c);
            graph.CreateEdge(f, e);
            graph.CreateEdge(f, m);
            graph.CreateEdge(d, b);

            return graph;
        }
    }
}
