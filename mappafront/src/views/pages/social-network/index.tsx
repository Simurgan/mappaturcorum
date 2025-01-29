import React, { useMemo } from "react";
import ForceGraph2D from "react-force-graph-2d";
import "./style.scss";

function generateRandomGraph() {
  const nodeCount = Math.floor(Math.random() * 21) + 80;
  const linkCount = Math.floor(Math.random() * 21) + 100;

  // Build array of nodes. Mark some of them red, others blue
  const nodes = Array.from({ length: nodeCount }, (_, i) => ({
    id: i,
    name: `Node ${i}`,
    // Example: random 30% chance for a node to be red
    isRed: Math.random() < 0.3,
  }));

  // Build array of links
  const links = Array.from({ length: linkCount }, () => {
    const source = Math.floor(Math.random() * nodeCount);
    let target = Math.floor(Math.random() * nodeCount);

    while (target === source) {
      target = Math.floor(Math.random() * nodeCount);
    }

    return { source, target };
  });

  return { nodes, links };
}

const GraphPage: React.FC = () => {
  const data = useMemo(() => generateRandomGraph(), []);

  return (
    <section className="section social-network-section">
      <div className="container">
        <div className="graph-container">
          <ForceGraph2D
            graphData={data}
            // We'll still use nodeLabel for the tooltip hover text.
            nodeLabel="name"
            // Color the node either red or blue
            nodeColor={(node: any) => (node.isRed ? "red" : "blue")}
            // We want to add a permanent label for red nodes. We do this using nodeCanvasObject.
            nodeCanvasObject={(node: any, ctx, globalScale) => {
              // Only draw a permanent label for red nodes
              if (node.isRed) {
                const label = node.name;
                const fontSize = 12 / globalScale;
                ctx.font = `${fontSize}px Sans-Serif`;
                ctx.fillStyle = "black";
                ctx.fillText(label, node.x + 8, node.y + 3);
              }
            }}
            // nodeCanvasObjectMode returns either "after", "before", or "replace".
            // Using "after" means the default circle is drawn first;
            // then we "augment" the node with our label if it's red.
            nodeCanvasObjectMode={(node: any) =>
              node.isRed ? "after" : undefined
            }
            enableNodeDrag={true}
          />
        </div>
        <div className="graph-tools-container"></div>
      </div>
    </section>
  );
};

export default GraphPage;
