import random
import sys
import numpy as np


class BoxSpawner:
    def __init__(self, x1, x2, y1, y2, z1, z2, seed=0, randomize_seed=True):
        """
        Creates a spawner in a rectangular prism.

        Parameters:
            x1 (float): Negative x bound.
            x2 (float): Positive x bound.
            y1 (float): Negative y bound.
            y2 (float): Positive y bound.
            z1 (float): Negative z bound.
            z2 (float): Positive z bound.
        """
        self.x1 = x1
        self.x2 = x2
        self.y1 = y1
        self.y2 = y2
        self.z1 = z1
        self.z2 = z2
        if not randomize_seed:
            self.seed = random.randint(-sys.maxsize - 1, sys.maxsize)
        else:
            self.seed = seed

    def spawn_whiskers(self, num_whiskers: int) -> (np.float32, np.float32, np.float32):
        """
        Generate random spawn position within the spawn box.

        Returns:
            np.array(np.float32): Random coordinate denoting the origin of a metal whiskers spawn.
        """
        whisker_spawn_positions = np.zeros((num_whiskers, 3), dtype=np.float32)
        for i in range(num_whiskers):
            x = np.float32(random.uniform(self.x1, self.x2))
            y = np.float32(random.uniform(self.y1, self.y2))
            z = np.float32(random.uniform(self.z1, self.z2))
            whisker_spawn_positions[i] = np.array([x, y, z])

        return whisker_spawn_positions
