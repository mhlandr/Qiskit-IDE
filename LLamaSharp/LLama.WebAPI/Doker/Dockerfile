# Use an official Python runtime as a parent image
FROM python:3.9-slim

# Set the working directory
WORKDIR /usr/src/app

# Install dependencies
RUN pip install qiskit matplotlib pylatexenc

# Command to keep the container running
CMD ["tail", "-f", "/dev/null"]
