import random
import sys
import warp as wp
import numpy as np


class Util:
    @staticmethod
    def rand_seed():
        return random.uniform(-sys.maxsize - 1, sys.maxsize)

    @staticmethod
    # Whisker density is a normal variate value.
    # Normal variate mu and sigma will be used from page 16 of the following pdf:
    # https://nepp.nasa.gov/whisker/reference/tech_papers/2008-Brusse-CALCE-Metal-Whiskers.pdf
    def estimate_density_mu_sigma(mu=53.74, sigma=1.572, n_samples=1000, has_conf_coat=False):
        if has_conf_coat:
            mu = 4.985
            sigma = 1.913

        # Generate an array of zeros the length of our total samples.
        n_whiskers = wp.zeros((n_samples,), dtype=wp.float32)

        log_arr = wp.zeros((n_samples,), dtype=wp.float32)
        wp.launch(gen_log_arr, (len(log_arr),), [n_whiskers, log_arr])

        sqr_arr = wp.zeros((n_samples,), dtype=wp.float32)
        wp.launch(gen_sqr_arr, (n_samples,), [log_arr, sqr_arr])

        mu_arr = wp.zeros((n_samples,), dtype=wp.float32)
        mu = fold_function(log_arr, mu_arr, reduce_arr) / n_samples

        sigma_arr = wp.zeros((n_samples,), dtype=wp.float32)
        wp.copy(sigma_arr, sqr_arr)
        mu_sqr = mu * mu
        wp.launch(subtract_mu_sqr, (n_samples,), [sigma_arr, mu_sqr])

        sigma = fold_function(sqr_arr, sigma_arr, reduce_arr) / n_samples

        return mu, sigma


@wp.kernel
def subtract_mu_sqr(log_sqr_arr: wp.array1d(dtype=wp.float32), mu_sqr: wp.array1d(dtype=wp.float32)):
    tid = wp.tid()[0]
    if tid < len(log_sqr_arr):
        log_sqr_arr[tid] = log_sqr_arr[tid] - mu_sqr


@wp.kernel
def gen_log_arr(whiskers: wp.array1d(dtype=wp.float32), ln_arr: wp.array1d(dtype=wp.float32)) -> None:
    tid = whiskers.tid()[0]
    if tid < len(ln_arr):
        ln_arr[tid] = wp.log(ln_arr[tid])


@wp.kernel
def gen_sqr_arr(whiskers: wp.array1d(dtype=wp), sum_arr: wp.array1d(dtype=wp.float32)) -> None:
    tid = wp.tid()[0]
    if tid < len(whiskers):
        sum_arr[tid] = whiskers[tid] * whiskers[tid]


@wp.kernel
def reduce_arr(whiskers: wp.array1d(dtype=wp.float32), sum_arr: wp.array1d(dtype=wp.float32), fold_elements) -> None:
    tid = whiskers.tid()[0]
    to_fold_elements = fold_elements // 2
    if tid < fold_elements:
        idx_1 = tid
        idx_2 = tid + to_fold_elements

        if fold_elements % 2 == 1 and tid * 2 == to_fold_elements - 2:
            sum_arr[idx_2] = whiskers[idx_2] + whiskers[idx_2 + 1]

        sum_arr[idx_1] = whiskers[idx_1] + whiskers[idx_2]


def fold_function(in_arr: wp.array1d(dtype=wp.float32), out_arr: wp.array1d(dtype=wp.float32), fold_func):
    fold_n_elements = in_arr.shape[0]
    while fold_n_elements > 0:
        wp.launch(fold_func, (len(in_arr),), [in_arr, out_arr, fold_n_elements])
        fold_n_elements = fold_n_elements // 2 + fold_n_elements % 2
    return out_arr[0]


print(Util.estimate_density_mu_sigma())
