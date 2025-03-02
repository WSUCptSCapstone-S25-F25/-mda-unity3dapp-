from controller.controller import Controller

if __name__ == "__main__":
    controller = Controller()
    box = (-5, 5, 10, 20, -5, 5)
    controller.set_box_spawner(box)
    controller.set_batch_size(1000000)
    controller.set_num_whiskers(1000000)
    controller.set_num_particles(7)
    controller.set_mu(4.5)
    controller.set_sigma(0.7)
    controller.set_dt(0.01)
    controller.set_simulation_steps(10000)
    controller.run_simulation()
