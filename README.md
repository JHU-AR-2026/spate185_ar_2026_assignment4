# AR 2026 – Assignment 4

**Author:** Sahil Patel (spate185)


## Q1 – Kalman Filter (Python)

- **Q1e** completes the Kalman Filter update step (Kalman gain, state correction, covariance correction) for a 1D position/velocity model with constant acceleration. The plot shows the true position, the noisy measurements, and the filtered estimate.
- **Q1f** skips the update step so the filter only uses its model. The plot shows the prediction drifting away from the true position, with a ±2σ band showing the uncertainty growing over time.
### Running

Requires Python 3 with `numpy` and `matplotlib`.

Using [uv](https://docs.astral.sh/uv/):

```bash
uv run --with numpy --with matplotlib Q1/Q1e/Q1e.py
uv run --with numpy --with matplotlib Q1/Q1f/Q1f.py
```

Or with pip:

```bash
pip install numpy matplotlib
python Q1/Q1e/Q1e.py
python Q1/Q1f/Q1f.py
```

Each script opens the plot in a window and saves it as a PNG. The noise is random, so each run looks slightly different.


## Q2 – Unity Roll-a-ball

Built with **Unity 6.2 (6000.2.6f2)**, Universal 3D template.

### Running

1. Open Unity Hub → **Add project from disk** → select the `Q2/roll ball` folder.
2. Open the scene `Assets/Scenes/SampleScene.unity`.
3. Press **Play**.