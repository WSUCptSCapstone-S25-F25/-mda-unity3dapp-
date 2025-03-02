from typing import Optional, Tuple


# TODO: IMPLEMENT VALIDATOR LOGIC

class SimulationSettings:
    """
    Instantiates a class to manage the state of the simulation settings: batch size, box spawner, num whiskers, and PCB.
    """

    def __init__(self):
        self._batch_size: Optional[int] = None
        self._box_spawner = None
        self._dt = None
        self._num_whiskers: Optional[int] = None
        self._pcb = None
        self._simulation_steps = None

    # Maintain alphabetical order.

    @property
    def batch_size(self) -> Optional[int]:
        """Getter for batch size."""
        return self._batch_size

    @batch_size.setter
    def batch_size(self, batch_size: int) -> None:
        """
        Sets the size of whisker batches to run through the simulation at the same time.

        Parameters:
            batch_size: Number of whiskers to be simulated at once.
        """
        self._batch_size = batch_size

    @property
    def box_spawner(self):
        """Getter for spawn box"""
        return self._box_spawner

    @box_spawner.setter
    def box_spawner(self, box_spawner) -> None:
        """
        Sets the SpawnBox dimensions for this simulation

        Parameters:
            dims (tuple): A tuple with 6 floating points representing
            [x1, x2, y1, y2, z1, z2] for the spawn box bounds where
            {var}1 <= {var}2
        """
        # Model uses a model instead of the controller to create
        self._box_spawner = box_spawner

    @property
    def dt(self) -> Optional[float]:
        return self._dt

    @dt.setter
    def dt(self, dt: float) -> None:
        self._dt = dt

    @property
    def num_whiskers(self) -> Optional[int]:
        """Return number of whiskers per simulation."""
        return self._num_whiskers

    @num_whiskers.setter
    def num_whiskers(self, num_whiskers: int) -> None:
        """
        Sets the total number of whiskers to be dropped in this simulation.

        Parameters:
            num_whiskers (int): Sum of whiskers to be dropped.
        """
        self._num_whiskers = num_whiskers

    @property
    def pcb(self):
        """Get the data abstraction of the PCB object"""
        return self._pcb

    # TODO: Implement set_pcb method.
    @pcb.setter
    def pcb(self, path) -> None:
        """
        Parses a selected ICP file to create a PCB data abstraction in the simulation.
        set_pcb will then send static positional data to the view.

        Parameters:
            pcb_icp_file (object): ICP object to parse
        """
        self._pcb = None

    @property
    def simulation_steps(self) -> int:
        return self._simulation_steps

    @simulation_steps.setter
    def simulation_steps(self, steps: int) -> None:
        self._simulation_steps = steps
