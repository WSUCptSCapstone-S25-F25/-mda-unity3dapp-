import math
import random
import numpy as np
import warp as wp


class LognormalRandom:
    def __init__(self, mu=5.01, sigma=1.15, seed=None, randomize_seed=True):
        """
        Creates a lognormal RNG for whisker length generation

        Parameters:
        """
        if seed is not None:
            self.seed = seed
        else:
            self.seed = random.randint
        # Mu and Sigma are defined in tens of microns.
        self.rng = random.Random(self.seed)
        self.mu = mu
        self.sigma = sigma

    def generate_lognormal_random(self, num_samples=1000):
        """
        Generates lognormal random whisker lengths from parameters self was generated using.
        """
        sample_lengths = np.zeros(num_samples, dtype=np.float32)
        for i in range(num_samples):
            u1 = self.rng.random()
            u2 = self.rng.random()
            std_norm_rand = math.exp(-2.0 * math.log(u1)) * math.sin(2.0 * math.pi * u2)
            sample_lengths[i] = self.mu + self.sigma * std_norm_rand
        return sample_lengths

    def set_mu(self, mu: float) -> None:
        """
        Sets the mean value of the lognormal distribution.

        Parameters:
            mu (float): Average whisker length in tens of microns
        """
        self.mu = mu

    def set_sigma(self, sigma: float) -> None:
        """
        Sets the standard deviation of the lognormal distribution.

        Parameters:
            sigma (float): Standard deviation of whisker length in tens of microns
        """
        self.sigma = sigma

    def set_rng_seed(self, seed: int) -> None:
        """
        Sets the seed for random generation.

        Parameters:
            seed: Default for truly pseudorandom metal whisker
        """
        self.rng = random.Random(seed) if seed is not None else random
