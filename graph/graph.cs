using System;
using System.Collections.Generic;
using System.Linq;

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
			neighbours = new List<Node>();
		}
	}

	public List<Node> GetNeighbours(){
		return neighbours;
	}
	
	public int NumNeighbours(){
		return neighbours.Count;
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
	
	
    public Node degreeLtReg(int val){
		int i = graph.Count-1;
		int[] temp = new int[graph.Count];
		foreach (var node in graph)
		{
			Console.WriteLine(node.name+" ->"+node.NumNeighbours());
			temp[i] = node.NumNeighbours();
			i--;
		}
		Console.WriteLine("------------");
		
		int degree = temp.Min();
		
		if(degree < val){
			int lowestNode = Array.IndexOf(temp, temp.Min());
			return graph.ElementAt(lowestNode);
		} else{
			return null;
		}
	}



	static void Main(string[] args)
	{
		const int MachineRegisters = 4;

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
		graph.CreateEdge(g,h);
		graph.CreateEdge(g,k);
		graph.CreateEdge(g,j);
		graph.CreateEdge(j,k);
		graph.CreateEdge(j,h);
		graph.CreateEdge(j,d);
		graph.CreateEdge(j,e);
		graph.CreateEdge(j,f);
		graph.CreateEdge(k,d);
		graph.CreateEdge(k,b);
		graph.CreateEdge(b,c);
		graph.CreateEdge(m,b);
		graph.CreateEdge(m,d);
		graph.CreateEdge(m,c);
		graph.CreateEdge(f,e);
		graph.CreateEdge(f,m);
		
		graph.PrintGraph();


		// Chaitin's Algorithm
		
		Stack<Node> stack = new Stack<Node>();
		Stack<Node> spillList = new Stack<Node>();
		
		while(graph.Size() > 0){
			Node node = graph.degreeLtReg(MachineRegisters);
			if(node != null){
				stack.Push(node);
				graph.RemoveEdge(node);
			}
			else{
				
			}
		}
		if(spillList.Count == 0){
			while(stack.Count > 0){
				Console.WriteLine(stack.Pop().name);
			}
		}
		else{
			// Interference magic to happen here :)
		}
		
	}

}
