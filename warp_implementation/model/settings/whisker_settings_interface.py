from typing import Optional
from model.object_generation.lognormal_random import LognormalRandom


# TODO: IMPLEMENT VALIDATOR LOGIC

class WhiskerSettings:
    def __init__(self, mu=5.01, sigma=1.15, num_particles=7, seed=0):
        """Manages the state of the whisker settings: mu, sigma, and num particles."""
        self._mu = mu
        self._sigma = sigma
        self._num_particles = num_particles
        self._lognormal_random = LognormalRandom(mu=mu, sigma=sigma, seed=seed)
        self._seed = seed

    # Maintain alphabetical order.
    @property
    def update_lognormal_random(self):
        self._lognormal_random = LognormalRandom()

    @property
    def mu(self) -> Optional[float]:
        """Getter for the average lognormal whisker length mu."""
        return self._mu

    @mu.setter
    def mu(self, mu: float) -> None:
        """
        Setter for the average lognormal whisker length mu.

        Parameters:
            mu (float): Average length of whiskers.
        """
        self._mu = mu

    @property
    def num_particles(self) -> Optional[int]:
        """Getter for number of particles in one whisker."""
        return self._num_particles

    @num_particles.setter
    def num_particles(self, num_particles: int) -> None:
        """
        Setter for number of particles in one whisker.

        Param:
            num_particles (int): Number of particles in a metal whisker
        """
        self._num_particles = num_particles

    @property
    def sigma(self) -> Optional[float]:
        """Getter for the lognormal distribution standard deviation sigma"""
        return self._sigma

    @sigma.setter
    def sigma(self, sigma: float) -> None:
        """
        Setter for the lognormal distribution standard deviation sigma

        Parameters:
            sigma: (float): Standard deviation of whiskers.
        """
        self._sigma = sigma
