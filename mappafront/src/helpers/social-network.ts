import {
  EdgeType,
  GraphDataType,
  GraphFilterType,
  NodeType,
  NodeTypesEnum,
  PseudoEdgeType,
  RawGraphDataType,
} from "@/models/social-network";

export const getNodeColor = (nodeType: NodeTypesEnum) => {
  switch (nodeType) {
    case NodeTypesEnum.OrdinaryPersonNode:
      return "red";
    case NodeTypesEnum.UnordinaryPersonNode:
      return "pink";
    case NodeTypesEnum.EthnicityNode:
      return "blue";
    case NodeTypesEnum.ReligionNode:
      return "green";
    case NodeTypesEnum.ProfessionNode:
      return "cyan";
    case NodeTypesEnum.GenderNode:
      return "yellow";
    case NodeTypesEnum.SourceNode:
      return "purple";
  }
};

export const calculateNodeSize = (node: NodeType, filters: GraphFilterType) => {
  let size = 1;

  if (filters.ordinaries) size += node.ordinaryRelCount;
  if (filters.unordinaries) size += node.unordinaryRelCount;
  if (filters.sources) size += node.sourceRelCount;
  if (filters.religions) size += node.religionRelCount;
  if (filters.ethnicities) size += node.ethnicityRelCount;
  if (filters.professions) size += node.professionRelCount;
  if (filters.genders) size += node.genderRelCount;

  return size;
};

export const filterNodes = (
  fullNodeData: GraphDataType,
  filters: GraphFilterType
) => {
  let filteredNodeTypes: NodeTypesEnum[] = [];
  if (filters.ordinaries)
    filteredNodeTypes.push(NodeTypesEnum.OrdinaryPersonNode);
  if (filters.unordinaries)
    filteredNodeTypes.push(NodeTypesEnum.UnordinaryPersonNode);
  if (filters.sources) filteredNodeTypes.push(NodeTypesEnum.SourceNode);
  if (filters.religions) filteredNodeTypes.push(NodeTypesEnum.ReligionNode);
  if (filters.ethnicities) filteredNodeTypes.push(NodeTypesEnum.EthnicityNode);
  if (filters.professions) filteredNodeTypes.push(NodeTypesEnum.ProfessionNode);
  if (filters.genders) filteredNodeTypes.push(NodeTypesEnum.GenderNode);

  let filteredNodes: NodeType[] = [];
  let edgeIdxsToRemove: number[] = [];

  fullNodeData.nodes.forEach((node) => {
    if (filteredNodeTypes.includes(node.type)) {
      filteredNodes.push(node);
    } else {
      fullNodeData.links.forEach((edge, edgeIdx) => {
        if (edge.source === node.id || edge.target === node.id) {
          edgeIdxsToRemove.push(edgeIdx);
        }
      });
    }
  });

  const filteredEdges = fullNodeData.links.filter(
    (_, idx) => !edgeIdxsToRemove.includes(idx)
  );

  return { nodes: filteredNodes, links: filteredEdges } as GraphDataType;
};

const convertToNode = (data: any[], type: NodeTypesEnum, lastId: number) => {
  let localLastId = lastId;
  const nodes = data.map((item) => {
    localLastId += 1;
    return {
      id: localLastId,
      dataId: item.id,
      name: item.name,
      type: type,
      ordinaryRelCount: 0,
      unordinaryRelCount: 0,
      sourceRelCount: 0,
      religionRelCount: 0,
      ethnicityRelCount: 0,
      professionRelCount: 0,
      genderRelCount: 0,
    } as NodeType;
  });
  return [nodes, localLastId];
};

export const generateGraph = (data: RawGraphDataType) => {
  let nodes: NodeType[] = [];
  let pseudoEdges: PseudoEdgeType[] = [];
  let lastId = -1;

  let [newNodes, newLastId] = convertToNode(
    data.religions,
    NodeTypesEnum.ReligionNode,
    lastId
  );
  nodes = [...nodes, ...(newNodes as NodeType[])];
  lastId = newLastId as number;

  [newNodes, newLastId] = convertToNode(
    data.ethnicities,
    NodeTypesEnum.EthnicityNode,
    lastId
  );
  nodes = [...nodes, ...(newNodes as NodeType[])];
  lastId = newLastId as number;

  [newNodes, newLastId] = convertToNode(
    data.professions,
    NodeTypesEnum.ProfessionNode,
    lastId
  );
  nodes = [...nodes, ...(newNodes as NodeType[])];
  lastId = newLastId as number;

  [newNodes, newLastId] = convertToNode(
    data.genders,
    NodeTypesEnum.GenderNode,
    lastId
  );
  nodes = [...nodes, ...(newNodes as NodeType[])];
  lastId = newLastId as number;

  [newNodes, newLastId] = convertToNode(
    data.sources,
    NodeTypesEnum.SourceNode,
    lastId
  );
  nodes = [...nodes, ...(newNodes as NodeType[])];
  lastId = newLastId as number;

  data.ordinaries.forEach((ordinary) => {
    lastId += 1;
    nodes.push({
      id: lastId,
      dataId: ordinary.id,
      name: ordinary.name,
      type: NodeTypesEnum.OrdinaryPersonNode,
      ordinaryRelCount: 0,
      unordinaryRelCount: 0,
      sourceRelCount: 0,
      religionRelCount: 0,
      ethnicityRelCount: 0,
      professionRelCount: 0,
      genderRelCount: 0,
    });

    if (ordinary.religion) {
      pseudoEdges.push({
        source: { type: NodeTypesEnum.OrdinaryPersonNode, id: ordinary.id },
        target: { type: NodeTypesEnum.ReligionNode, id: ordinary.religion },
      });
    }

    if (ordinary.ethnicity) {
      pseudoEdges.push({
        source: { type: NodeTypesEnum.OrdinaryPersonNode, id: ordinary.id },
        target: { type: NodeTypesEnum.EthnicityNode, id: ordinary.ethnicity },
      });
    }

    if (ordinary.profession) {
      pseudoEdges.push({
        source: { type: NodeTypesEnum.OrdinaryPersonNode, id: ordinary.id },
        target: { type: NodeTypesEnum.ProfessionNode, id: ordinary.profession },
      });
    }

    if (ordinary.gender) {
      pseudoEdges.push({
        source: { type: NodeTypesEnum.OrdinaryPersonNode, id: ordinary.id },
        target: { type: NodeTypesEnum.GenderNode, id: ordinary.gender },
      });
    }

    ordinary.sources.forEach((inter) => {
      pseudoEdges.push({
        source: { type: NodeTypesEnum.OrdinaryPersonNode, id: ordinary.id },
        target: { type: NodeTypesEnum.SourceNode, id: inter },
      });
    });

    [
      ...ordinary.interactionsWithOrdinaryA,
      ...ordinary.interactionsWithOrdinaryB,
    ].forEach((inter) => {
      pseudoEdges.push({
        source: { type: NodeTypesEnum.OrdinaryPersonNode, id: ordinary.id },
        target: { type: NodeTypesEnum.OrdinaryPersonNode, id: inter },
      });
    });

    ordinary.interactionsWithUnordinary.forEach((inter) => {
      pseudoEdges.push({
        source: { type: NodeTypesEnum.OrdinaryPersonNode, id: ordinary.id },
        target: { type: NodeTypesEnum.UnordinaryPersonNode, id: inter },
      });
    });
  });

  data.unordinaries.forEach((unordinary) => {
    lastId += 1;
    nodes.push({
      id: lastId,
      dataId: unordinary.id,
      name: unordinary.name,
      type: NodeTypesEnum.UnordinaryPersonNode,
      ordinaryRelCount: 0,
      unordinaryRelCount: 0,
      sourceRelCount: 0,
      religionRelCount: 0,
      ethnicityRelCount: 0,
      professionRelCount: 0,
      genderRelCount: 0,
    });

    if (unordinary.religion) {
      pseudoEdges.push({
        source: { type: NodeTypesEnum.UnordinaryPersonNode, id: unordinary.id },
        target: { type: NodeTypesEnum.ReligionNode, id: unordinary.religion },
      });
    }

    if (unordinary.ethnicity) {
      pseudoEdges.push({
        source: { type: NodeTypesEnum.UnordinaryPersonNode, id: unordinary.id },
        target: { type: NodeTypesEnum.EthnicityNode, id: unordinary.ethnicity },
      });
    }

    if (unordinary.profession) {
      pseudoEdges.push({
        source: { type: NodeTypesEnum.UnordinaryPersonNode, id: unordinary.id },
        target: {
          type: NodeTypesEnum.ProfessionNode,
          id: unordinary.profession,
        },
      });
    }

    if (unordinary.gender) {
      pseudoEdges.push({
        source: { type: NodeTypesEnum.UnordinaryPersonNode, id: unordinary.id },
        target: { type: NodeTypesEnum.GenderNode, id: unordinary.gender },
      });
    }

    unordinary.sources.forEach((inter) => {
      pseudoEdges.push({
        source: { type: NodeTypesEnum.UnordinaryPersonNode, id: unordinary.id },
        target: { type: NodeTypesEnum.SourceNode, id: inter },
      });
    });

    unordinary.interactionsWithOrdinary.forEach((inter) => {
      pseudoEdges.push({
        source: { type: NodeTypesEnum.OrdinaryPersonNode, id: inter },
        target: { type: NodeTypesEnum.UnordinaryPersonNode, id: unordinary.id },
      });
    });

    [
      ...unordinary.interactionsWithUnordinaryA,
      ...unordinary.interactionsWithUnordinaryB,
    ].forEach((inter) => {
      pseudoEdges.push({
        source: { type: NodeTypesEnum.UnordinaryPersonNode, id: unordinary.id },
        target: { type: NodeTypesEnum.UnordinaryPersonNode, id: inter },
      });
    });
  });

  let edgeIdxsToRemove: number[] = [];

  for (let i = 0; i < pseudoEdges.length; i++) {
    const pedge1 = pseudoEdges[i];
    if (
      !(
        pedge1.target.type === NodeTypesEnum.OrdinaryPersonNode ||
        pedge1.target.type === NodeTypesEnum.UnordinaryPersonNode
      ) ||
      edgeIdxsToRemove.includes(i)
    )
      continue;

    for (let k = i; k < pseudoEdges.length; k++) {
      const pedge2 = pseudoEdges[k];
      if (
        !(
          pedge2.target.type === NodeTypesEnum.OrdinaryPersonNode ||
          pedge2.target.type === NodeTypesEnum.UnordinaryPersonNode
        ) ||
        edgeIdxsToRemove.includes(k)
      )
        continue;

      const equivalence =
        (pedge1.source.type === pedge2.source.type &&
          pedge1.target.type === pedge2.target.type &&
          pedge1.source.id === pedge2.source.id &&
          pedge1.target.id === pedge2.target.id) ||
        (pedge1.source.type === pedge2.target.type &&
          pedge1.target.type === pedge2.source.type &&
          pedge1.source.id === pedge2.target.id &&
          pedge1.target.id === pedge2.source.id);

      if (equivalence) {
        edgeIdxsToRemove.push(k);
      }
    }
  }

  edgeIdxsToRemove.forEach((idx) => {
    pseudoEdges.splice(idx, 1);
  });

  // convert them to real edges:
  let links: EdgeType[] = [];

  pseudoEdges.forEach((pedge) => {
    const sourceNode = nodes.find(
      (node) =>
        node.type === pedge.source.type && node.dataId === pedge.source.id
    );
    const targetNode = nodes.find(
      (node) =>
        node.type === pedge.target.type && node.dataId === pedge.target.id
    );

    if (sourceNode && targetNode) {
      switch (sourceNode.type) {
        case NodeTypesEnum.OrdinaryPersonNode:
          targetNode.ordinaryRelCount += 1;
          break;
        case NodeTypesEnum.UnordinaryPersonNode:
          targetNode.unordinaryRelCount += 1;
          break;
      }

      switch (targetNode.type) {
        case NodeTypesEnum.OrdinaryPersonNode:
          sourceNode.ordinaryRelCount += 1;
          break;
        case NodeTypesEnum.UnordinaryPersonNode:
          sourceNode.unordinaryRelCount += 1;
          break;
        case NodeTypesEnum.SourceNode:
          sourceNode.sourceRelCount += 1;
          break;
        case NodeTypesEnum.ReligionNode:
          sourceNode.religionRelCount += 1;
          break;
        case NodeTypesEnum.EthnicityNode:
          sourceNode.ethnicityRelCount += 1;
          break;
        case NodeTypesEnum.ProfessionNode:
          sourceNode.professionRelCount += 1;
          break;
        case NodeTypesEnum.GenderNode:
          sourceNode.genderRelCount += 1;
          break;
      }
      links.push({ source: sourceNode.id, target: targetNode.id });
    }
  });

  return { nodes, links };
};
