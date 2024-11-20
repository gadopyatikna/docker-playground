# eavesdropper.py (Simulating Packet Sniffing)
import socket

# Create a raw socket to listen to the network traffic
eavesdropper_socket = socket.socket(socket.AF_INET, socket.SOCK_RAW, socket.IPPROTO_TCP)

# Bind to the local interface (localhost in this case)
eavesdropper_socket.bind(("localhost", 8080))

print("Eavesdropping... (Capturing all HTTP traffic)")

# Capture the data from the network traffic
while True:
    data = eavesdropper_socket.recvfrom(65565)
    print(f"Captured data: {data[0].decode(errors='ignore')}")
