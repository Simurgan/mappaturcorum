import React, { useMemo } from "react";
import ForceGraph2D from "react-force-graph-2d";

// Generate random graph data
function generateRandomGraph() {
  // random number of nodes from 80 to 100
  const nodeCount = Math.floor(Math.random() * 21) + 80;

  // random number of edges from 100 to 120
  const linkCount = Math.floor(Math.random() * 21) + 100;

  // build array of nodes
  const nodes = Array.from({ length: nodeCount }, (_, i) => ({
    id: i,
    // You can add any custom field (for labeling, etc.)
    name: `Node ${i}`,
  }));

  // build array of links
  const links = Array.from({ length: linkCount }, () => {
    const source = Math.floor(Math.random() * nodeCount);
    let target = Math.floor(Math.random() * nodeCount);

    // ensure source != target (avoid loops)
    while (target === source) {
      target = Math.floor(Math.random() * nodeCount);
    }

    return { source, target };
  });

  return { nodes, links };
}

const GraphPage: React.FC = () => {
  // memoize so we don't regenerate on every render
  const data = useMemo(() => generateRandomGraph(), []);

  return (
    <div style={{ width: "100vw", height: "100vh" }}>
      <ForceGraph2D
        // Supply the graph data
        graphData={data}
        // Automatically show tooltip on hover (using the 'name' field)
        nodeLabel="name"
        // By default, nodes are draggable and the force simulation is updated
        // You can customize simulation parameters if desired
        enableNodeDrag={true}

        // You can also tune the velocity decay, repulsion, etc.
        // For example, to slow down the freeze after dragging:
        // d3AlphaDecay={0.01}
        // d3VelocityDecay={0.3}

        // (Optional) Adjust how the node is drawn if you'd like.
        // nodeCanvasObject={(node, ctx, globalScale) => {
        //   const label = node.name;
        //   // Draw a simple circle
        //   ctx.beginPath();
        //   ctx.arc(node.x!, node.y!, 5, 0, 2 * Math.PI);
        //   ctx.fillStyle = '#6fc';
        //   ctx.fill();
        //   // Optionally draw label always (not just on hover)
        //   // const fontSize = 12/globalScale;
        //   // ctx.font = `${fontSize}px Sans-Serif`;
        //   // ctx.fillStyle = 'black';
        //   // ctx.fillText(label, node.x! + 8, node.y! + 3);
        // }}
      />
    </div>
  );
};

export default GraphPage;
