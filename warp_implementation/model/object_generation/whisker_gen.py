import warp as wp

from model.physics_engine.physics_engine import PhysicsEngine


class WhiskerGen:
    def __init__(self, batch_size: int, num_particles: int, spawn_positions: wp.array, whisker_lens: wp.array):
        """
        Instantiates whisker object prior to being dropped.

        Parameters:
            batch_size (int): Number of metal whiskers processed for this batch.
            whisker_length (float): Length of whisker in microns.
            total_particles (int): Number of particles representing one whisker in the simulation.
            spawn_positions (wp.array): Warp array containing the spawn position of each whisker.
        """
        self.batch_size = batch_size
        self.num_particles = num_particles
        self.whisker_length = whisker_lens
        self.total_particles = batch_size * num_particles

        # Allocate warp arrays on the GPU
        self.positions = spawn_positions  # Particle positional data
        self.velocities = wp.zeros((self.total_particles,), dtype=wp.vec3)  # Particle initial velocity

        # Initialize batch of whiskers on GPU
        wp.launch(PhysicsEngine.init_whisker_kernel, dim=(self.total_particles,),
                  inputs=[self.positions, self.velocities, whisker_lens, num_particles, spawn_positions])
        wp.synchronize()

    def get_positions(self):
        return self.positions.np()
