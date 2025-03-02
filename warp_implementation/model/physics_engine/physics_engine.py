import warp as wp


# Object generation and the physics engine will be tied closely together.
class PhysicsEngine:
    @staticmethod
    @wp.kernel
    def drop_kernel(positions: wp.array(dtype=wp.vec3),
                    velocities: wp.array(dtype=wp.vec3),
                    dt: (float)):
        # Id of the GPU core operating on this falling object.
        tid = wp.tid()
        # Subtracts the acceleration of gravity every second the kernel falls.
        gravity = wp.vec3(0.0, -9.81, 0.0)
        # Update the velocity of whisker:
        velocities[tid] = velocities[tid] + gravity
        # Update position of whisker:
        positions[tid] = positions[tid] + velocities[tid] * dt

    @staticmethod
    @wp.kernel
    def init_whisker_kernel(positions: wp.array(dtype=wp.vec3),
                            velocities: wp.array(dtype=wp.vec3),
                            whisker_lens: wp.array(dtype=wp.float32),
                            num_particles: int,
                            spawn_positions: wp.array(dtype=wp.vec3),
                            ) -> None:
        tid = wp.tid()
        whisker_idx = tid // num_particles
        particle_idx = tid % num_particles

        whisker_len = whisker_lens[whisker_idx]
        spacing = float(whisker_len) / float(num_particles - 1)

        spawn_offset = spawn_positions[whisker_idx]
        positions[tid] = spawn_offset + wp.vec3(0.0, whisker_len - float(particle_idx) * spacing, 0.0)
        velocities[tid] = wp.vec3(0.0, 0.0, 0.0)


