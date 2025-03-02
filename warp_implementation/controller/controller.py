import warp as wp
import numpy as np
from model.object_generation.box_spawner import BoxSpawner
from model.object_generation.lognormal_random import LognormalRandom
from model.settings.parameter_validation import ParameterValidator
from model.physics_engine.physics_engine import PhysicsEngine
from model.settings.simulation_settings import SimulationSettings
from model.object_generation.whisker_gen import WhiskerGen
from model.settings.whisker_settings_interface import WhiskerSettings


class Controller:
    def __init__(self) -> None:
        """Orchestrates the usage of methods across classes."""
        # Instantiate validator to ensure graceful program use
        self.validator = ParameterValidator()
        self.simulation_settings = SimulationSettings()
        self.whisker_settings = WhiskerSettings()
        self.lrng = LognormalRandom()
        self.scene = None
        self.viewer = None

    # Set Simulation Settings ------------------------------------------------------ Alphabetical Ordering

    def set_batch_size(self, batch_size: int) -> None:
        """
        Sets the number of whiskers to instantiate and drop at one time.

        Parameters:
            batch_size (int): Number of whiskers to instantiate at once.
        """
        self.simulation_settings.batch_size = batch_size

    def set_box_spawner(self, dims: tuple) -> None:
        """
            Creates and sends a valid spawn box for the WhiskerSetting class to maintain.

            Parameters:
                dims (tuple): A tuple with 6 floating points representing
                [x1, x2, y1, y2, z1, z2] for the spawn box bounds where
                {var}1 <= {var}2
        """
        x1, x2, y1, y2, z1, z2 = dims
        box_spawner = BoxSpawner(x1, x2, y1, y2, z1, z2)
        self.simulation_settings.box_spawner = box_spawner

    def set_dt(self, dt: float) -> None:
        self.simulation_settings.dt = dt

    def set_num_whiskers(self, num_whiskers: int):
        """
        Sets the number of whiskers to be instantiated on one simulation.

        Parameters (int):
            Number of whiskers to instantiate in one simulation.
        """
        self.simulation_settings.num_whiskers = num_whiskers

    def set_pcb(self, pcb_path):
        # TODO: Create parsing logic for IPC-2581 files.
        pass

    def set_simulation_steps(self, simulation_steps: int) -> None:
        self.simulation_settings.simulation_steps = simulation_steps

    # Set Whisker settings --------------------------------------------------------- Alphabetical Ordering

    def set_mu(self, mu: float) -> None:
        """Sets average whisker length in the WhiskerSettings class."""
        self.whisker_settings.mu = mu
        self.lrng.set_mu(mu)

    def set_num_particles(self, num_particles: int) -> None:
        """
        Sets the number of particles to represent a whisker for the next simulation.

        Parameters:
            num_particles (int): Number of particles to represent a whisker.
        """
        self.whisker_settings.num_particles = num_particles

    def set_sigma(self, sigma: float) -> None:
        """
        Sets the standard deviation in the WhiskerSettings class

        parameters:
            sigma: Standard deviation for the lognormal distribution.
        """
        self.whisker_settings.sigma = sigma
        self.lrng.set_sigma(sigma)

    # Simulation Runner ----------------------------------------------------------------------------------------

    def run_simulation(self) -> None:
        """
        Starts a single physics simulation batch:
            - Instantiates batch of whiskers.
            - Spawn Whiskers at random locations in the spawn box.
            - Use lognormal RNG to determine whisker length.
            - Updates physics via GPU kernels.
        """
        # Get simulation settings:
        spawn_box = self.simulation_settings.box_spawner
        total_whiskers = self.simulation_settings.num_whiskers
        num_particles = self.whisker_settings.num_particles
        steps = self.simulation_settings.simulation_steps
        dt = self.simulation_settings.dt

        # Generate lognormal random lengths for batch.
        whisker_lens = self.lrng.generate_lognormal_random(self.simulation_settings.num_whiskers)
        whisker_spawn_pos_np = spawn_box.spawn_whiskers(total_whiskers)
        # Generate spawn positions for each whisker in batch.
        spawn_positions = wp.from_numpy(whisker_spawn_pos_np, dtype=wp.vec3)
        # Generate warp lengths
        whisker_lens = wp.from_numpy(whisker_lens, dtype=wp.float32)
        # Generate all whiskers for the simulation
        whisker_objects = WhiskerGen(total_whiskers, num_particles, spawn_positions, whisker_lens)
        for step in range(steps):
            self.simulation_step(whisker_objects, dt)

            # Debug physics:
            first_whisker_particles = whisker_objects.positions.numpy()[:num_particles]
            print(first_whisker_particles)

    @staticmethod
    def simulation_step(whisker_batch, dt: float) -> None:
        total_particles = whisker_batch.total_particles
        wp.launch(PhysicsEngine.drop_kernel, dim=(total_particles,),
                  inputs=[whisker_batch.positions, whisker_batch.velocities, dt])
