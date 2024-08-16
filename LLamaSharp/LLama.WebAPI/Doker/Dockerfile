# Use an official Python runtime as a parent image
FROM python:3.9-slim

# Set the working directory
WORKDIR /usr/src/app

# Install git and other dependencies
RUN apt-get update && apt-get install -y git && rm -rf /var/lib/apt/lists/*

# Install specific versions of qiskit and qiskit-aer
RUN pip install qiskit==0.34.2 qiskit-aer==0.10.3 matplotlib pylatexenc numpy 

# List installed packages for debugging
RUN pip list

# Copy the test script into the container
COPY test_qiskit_aer.py .

# Run the test script to verify installation
RUN python test_qiskit_aer.py

# Copy the main script into the container
COPY test_script.py .

# Run the main script
CMD ["python", "/usr/src/app/test_script.py"]
