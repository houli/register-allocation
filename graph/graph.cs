using System;
using System.Collections.Generic;

	class Node{
		public string name {get; }
		private List<Node> neighbours { get; set; }
		
		public Node(string name){
			this.name = name;
			neighbours = new List<Node>();
		}
		
		public void AddNeighbour(Node node){
			neighbours.Add(node);
		}
		
		public void RemoveNode(Node node){
			if (neighbours.Contains(node)) {
				neighbours.Remove(node);
			}
			neighbours = new List<Node>();
		}
		
		public List<Node> GetNeighbours(){
			return neighbours;
		}		
	}
	
	class Graph
	{
		private List<Node> graph;

		public Graph() {
			this.graph = new List<Node>();
		}
		
		public int Size(){
			return this.graph.Count;
		}
		
		public void AddNode(Node node){
			graph.Add(node);
		}
		
		public void CreateEdge(Node Node1, Node Node2){
			if(!graph.Contains(Node1)){
				graph.Add(Node1);
			}
			if(!graph.Contains(Node2)){
				graph.Add(Node2);
			}
			Node1.AddNeighbour(Node2);
			Node2.AddNeighbour(Node1);
			
		}
		
		public void RemoveEdge(Node toBeRemoved){
			foreach (var node in graph) {
				node.RemoveNode(toBeRemoved);
			}
			if (graph.Contains(toBeRemoved)) {
				graph.Remove(toBeRemoved);
			}
		}
		
		public void PrintGraph(){
			foreach (var node in graph){
				Console.Write("Vertex:"+node.name+" Edges:");
				foreach (var neighbour in node.GetNeighbours())
				{
					Console.Write("|"+neighbour.name+"|");	
				}
				Console.WriteLine("");
			}
		}
		

	
	static void Main(string[] args)
	{
		const int MachineRegisters = 5;
		
		Stack<int> myStack = new Stack<int>();
		myStack.Push(60);
		
		Node node1 = new Node("$T1");
		Node node2 = new Node("$T2");
		Node node3 = new Node("$T3");
		Node node4 = new Node("$T4");

		Graph graph = new Graph();
		graph.CreateEdge(node1,node2);
		graph.CreateEdge(node1,node3);
		graph.AddNode(node4);

		graph.PrintGraph();

		graph.RemoveEdge(node1);
		Console.WriteLine("");
		graph.PrintGraph();


		graph.CreateEdge(node1,node4);
		Console.WriteLine("");
		graph.PrintGraph();
		
	}

}


