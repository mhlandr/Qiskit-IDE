import os
from qiskit import QuantumCircuit, transpile
from qiskit.providers.aer import AerSimulator
import matplotlib.pyplot as plt

# Create a Quantum Circuit
qc = QuantumCircuit(2, 2)
qc.h(0)
qc.cx(0, 1)
qc.measure([0, 1], [0, 1])

# Use the AerSimulator
simulator = AerSimulator()

# Transpile the circuit for the simulator
compiled_circuit = transpile(qc, simulator)

# Run the circuit on the simulator
job = simulator.run(compiled_circuit)
result = job.result()

# Print the results
counts = result.get_counts(qc)
print("Counts:", counts)

# Draw the circuit
circuit_image = qc.draw('mpl')

# Ensure the output directory exists
output_dir = '/usr/src/app/output'
os.makedirs(output_dir, exist_ok=True)

# Save the image
plt.savefig(os.path.join(output_dir, 'quantum_circuit.png'))
