import React, { useEffect, useMemo, useState } from "react";
import ForceGraph2D from "react-force-graph-2d";
import "./style.scss";
import { useWindowSize } from "@react-hook/window-size";
import { getOrdinaryGraph } from "@/actions/ordinary-people";
import { getAllReligions } from "@/actions/religion";
import { getUnordinaryGraph } from "@/actions/unordinary-people";
import { getAllWrittenSources } from "@/actions/written-source";
import { getAllEthnicities } from "@/actions/ethnicity";
import { getAllProfessions } from "@/actions/profession";
import { getAllGenders } from "@/actions/gender";
import {
  GraphDataType,
  GraphFilterType,
  RawGraphDataType,
} from "@/models/social-network";
import {
  calculateNodeSize,
  filterNodes,
  generateGraph,
  getNodeColor,
} from "@/helpers/social-network";

const GraphPage: React.FC = () => {
  const [width, height] = useWindowSize();
  const [filters, setFilters] = useState<GraphFilterType>({
    ordinaries: false,
    religions: false,
    unordinaries: false,
    sources: false,
    ethnicities: false,
    professions: false,
    genders: false,
  });
  const [rawData, setRawData] = useState<RawGraphDataType>({
    ordinaries: [],
    religions: [],
    unordinaries: [],
    sources: [],
    ethnicities: [],
    professions: [],
    genders: [],
  });
  const fullNodeData = useMemo(() => generateGraph(rawData), [rawData]);
  const [data, setData] = useState<GraphDataType>();

  const getInitialData = async () => {
    const [ordinaryResponse, religionResponse] = await Promise.all([
      getOrdinaryGraph(),
      getAllReligions(),
    ]);

    if (ordinaryResponse.status === 200 && religionResponse.status === 200) {
      setRawData({
        ordinaries: ordinaryResponse.data,
        religions: religionResponse.data,
        unordinaries: [],
        sources: [],
        ethnicities: [],
        professions: [],
        genders: [],
      });

      setFilters({
        ordinaries: true,
        religions: true,
        unordinaries: false,
        sources: false,
        ethnicities: false,
        professions: false,
        genders: false,
      });
    }
  };

  const getFullData = async () => {
    const [
      unordinaryResponse,
      sourceResponse,
      ethnicityResponse,
      professionResponse,
      genderResponse,
    ] = await Promise.all([
      getUnordinaryGraph(),
      getAllWrittenSources(),
      getAllEthnicities(),
      getAllProfessions(),
      getAllGenders(),
    ]);

    if (
      unordinaryResponse.status === 200 &&
      sourceResponse.status === 200 &&
      ethnicityResponse.status === 200 &&
      professionResponse.status === 200 &&
      genderResponse.status === 200
    ) {
      setRawData((prev) => {
        return {
          ...prev,
          unordinaries: unordinaryResponse.data,
          sources: sourceResponse.data,
          ethnicities: ethnicityResponse.data,
          professions: professionResponse.data,
          genders: genderResponse.data,
        };
      });
    }
  };

  useEffect(() => {
    getInitialData();
    getFullData();
  }, []);

  useEffect(() => {
    const nodsod = filterNodes(fullNodeData, filters);
    const shamans = rawData.ordinaries.filter(
      (ordinary) => ordinary.religion === 18841609
    );

    shamans.forEach((shaman) => {
      const nod = nodsod.nodes.filter((no) => no.dataId === shaman.id);
      if (nod.length !== 1) {
        console.log("cloudn't found shaman:");
        console.log(shaman);
      }

      const shamanismNode = nodsod.nodes.filter((no) => no.dataId === 18841609);
      if (shamanismNode.length !== 1) {
        console.log("no shamanism node");
      }
      const edges = nodsod.links.filter(
        (edge) =>
          edge.source === nod[0].id && edge.target === shamanismNode[0].id
      );

      if (edges.length !== 1) {
        console.log("edge bug found:");
        console.log(nod[0]);
      }
    });
    console.log("done with checking");

    setData(nodsod);
  }, [filters]);

  return (
    <section className="section social-network-section">
      <div className="container">
        <div className="graph-container">
          <ForceGraph2D
            width={width - 240}
            height={height - 48}
            graphData={data}
            // We'll still use nodeLabel for the tooltip hover text.
            nodeLabel="name"
            // Color the node either red or blue
            nodeColor={(node: any) => getNodeColor(node.type)}
            // We want to add a permanent label for red nodes. We do this using nodeCanvasObject.
            nodeCanvasObject={(node: any, ctx, globalScale) => {
              // Only draw a permanent label for red nodes
              const label = node.name;
              const fontSize = 12 / globalScale;
              ctx.font = `${fontSize}px Sans-Serif`;
              ctx.fillStyle = "black";
              ctx.fillText(label, node.x + 8, node.y + 3);
            }}
            nodeVal={(node) => calculateNodeSize(node, filters)}
            // nodeCanvasObjectMode returns either "after", "before", or "replace".
            // Using "after" means the default circle is drawn first;
            // then we "augment" the node with our label if it's red.
            nodeCanvasObjectMode={"after"}
            enableNodeDrag={true}
            // dagMode="radialout"
            // dagLevelDistance={10}
          />
        </div>
        <div className="graph-tools-container"></div>
      </div>
    </section>
  );
};

export default GraphPage;
