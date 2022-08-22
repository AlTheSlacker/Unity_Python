import socket


class Client:

    connection = None

    def __init__(self, host_ip, port):
        try:
            self.connection = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            status = "Socket successfully created"
        except socket.error as err:
            status = "socket creation failed with error %s" % err
        print(status)
        self.connection.connect((host_ip, port))
        print("Connected to: ", host_ip, " port: ", port)

    def send(self, message):
        self.connection.send(message.encode())

    def close(self):
        self.connection.close()
