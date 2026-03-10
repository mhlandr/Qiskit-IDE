try:
    from qiskit import QuantumCircuit, transpile
    from qiskit.providers.aer import AerSimulator
    print("Qiskit Aer is successfully imported and ready to use.")
except Exception as e:
    print(f"Error: {e}")
