import client

host_ip = 'localhost'
port = 5000
connection = client.Client(host_ip, port)
connection.send("hello from python")
connection.close()
