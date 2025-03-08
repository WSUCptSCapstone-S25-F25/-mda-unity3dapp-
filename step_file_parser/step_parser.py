import os
from steputils import p21


class StepParser:
    """
    A parser for ISO 10303 STEP files using the p21 library.
    This class allows you to add STEP files, store their parsed content,
    and extract entities from the files.
    """

    def __init__(self):
        # Mapping of file names to their parsed content.
        self._files = {}
        # Ordered list of file names added.
        self._names = []

    def add_file(self, step_file_path):
        """
        Load and add a STEP file for parsing.

        Args:
            step_file_path (str): The path to the STEP file.
        """
        step_file_name = os.path.basename(step_file_path)
        with open(step_file_path, 'r') as f:
            p21_file = p21.load(f)

        file_data = {
            'root': p21_file,
            'data': p21_file.data,
            'head': p21_file.header
        }
        if step_file_name in self._files:
            # Support multiple additions from the same file by storing lists.
            existing = self._files[step_file_name]
            for key in ['root', 'data', 'head']:
                if not isinstance(existing[key], list):
                    existing[key] = [existing[key]]
            existing['root'].append(p21_file)
            existing['data'].append(p21_file.data)
            existing['head'].append(p21_file.header)
        else:
            self._files[step_file_name] = file_data
            self._names.append(step_file_name)

    @property
    def files(self):
        """
        Returns the dictionary of parsed files.
        """
        return self._files

    @property
    def names(self):
        """
        Returns the list of file names added.
        """
        return self._names

    def parse_entities(self):
        """
        Parse and extract entities from the added STEP files.

        Returns:
            dict: Parsed entities organized by file name.
                  The structure is:
                  {
                      file_name: {
                          entity_id: {
                              'name': [entity names],
                              'ref': [entity references] (if available),
                              'params': [entity parameters]
                          },
                          ...
                      },
                      ...
                  }
        """
        parsed_entities = {}
        for file_name in self._names:
            file_info = self._files[file_name]
            data_list = file_info['data']
            # Ensure data_list is iterable
            if not isinstance(data_list, list):
                data_list = [data_list]
            parsed_entities[file_name] = {}
            for data in data_list:
                for entity_id, entity in data.instances.items():
                    if self.has_multiple_entities(entity):
                        for ent in entity.entities:
                            ref = ent.ref if hasattr(ent, 'ref') else None
                            name = ent.name
                            params = getattr(ent, 'params', None)
                            if entity_id not in parsed_entities[file_name]:
                                parsed_entities[file_name][entity_id] = {
                                    'name': [name],
                                    'ref': [ref],
                                    'params': [params]
                                }
                            else:
                                parsed_entities[file_name][entity_id]['params'].append(params)
                    else:
                        temp_ent = entity.entity
                        name = temp_ent.name
                        ref = entity.ref
                        params = temp_ent.params
                        parsed_entities[file_name][entity_id] = {
                            'name': [name],
                            'ref': [ref],
                            'params': [params]
                        }
        return parsed_entities

    def __getitem__(self, key):
        return self._files.get(key, f"File {key} not found.")

    def keys(self):
        return self._files.keys()

    def values(self):
        return self._files.values()

    def items(self):
        return self._files.items()

    def __iter__(self):
        return iter(self._files)

    @staticmethod
    def has_multiple_entities(entity):
        """
        Determine whether an entity contains multiple entries.

        Args:
            entity: The entity object to check.

        Returns:
            bool: True if the entity has multiple entries, False otherwise.

        Raises:
            IOError: If the entity does not have expected attributes.
        """
        if hasattr(entity, "entities"):
            return True
        elif hasattr(entity, "entity"):
            return False
        else:
            raise IOError(f'Critical Error. Entity broke the program.\n\n {dir(entity)}')
