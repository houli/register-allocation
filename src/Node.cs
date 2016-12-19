using System.Collections.Generic;

namespace Tastier {
    public class Node {
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

        public bool HasNeighbour(Node node) {
            return neighbours.Contains(node);
        }

        public List<Node> GetNeighbours() {
            return neighbours;
        }

        public int NumNeighbours() {
            return neighbours.Count;
        }
    }
}
