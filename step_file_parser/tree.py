from typing import List
from typing import Optional
from typing import Tuple
import typing


class Tree:
    def __init__(self):
        self.independent_nodes: typing.Dict[str, 'Tree.Node'] = dict()
        self.dependent_nodes: typing.Dict[str, 'Tree.Node'] = dict()
        self.geometric_representation: typing.Dict[str, 'Tree.GeometricNode'] = dict()

    def iter_tree(self, _id: str):
        visited = set()

        def _traverse(tree_node: 'Tree.Node'):
            if tree_node.id in visited:
                return
            visited.add(tree_node.id)
            yield tree_node

            for child in tree_node.iter_children():
                yield from _traverse(child)

        for node in self.independent_nodes.values():
            yield from _traverse(node)

    # TODO: Build the tree!
    def build(self, ids, entities):
        pass

    class Node:
        def __init__(self, _id: str, name: str, dep_nodes: List, param_nodes: List | None, parameters: List | None):
            self.parents = dep_nodes
            self.children = param_nodes
            self.parameters = parameters
            self._id = _id
            self.name = name

        def iter_children(self):
            for child in self.children:
                yield child

        def get_children(self):
            return self.parents

        def get_parents(self):
            return self.children

        def is_independent(self):
            return len(self.parents) < 1 or (len(self.parents) == 1 and self.parents[0] is self)

        def get_parameters(self):
            return self.parameters

        def get_id(self):
            return self._id

        def get_name(self):
            return self.name

        def __repr__(self):
            return f'Node({self._id}'

        @property
        def id(self):
            return self._id


    class GeometricNode:
        def __init__(self):
            self.parents = None
