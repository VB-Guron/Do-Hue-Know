#
#   Hello World server in Python
#   Binds REP socket to tcp://*:5555
#   Expects b"Hello" from client, replies with b"World"
#

import zmq
from GD6_DTW_Blue import calc_Blue
from GD6_DTW_Green import calc_Green
from GD6_DTW_Orange import calc_Orange
from GD6_DTW_Red import calc_Red
from GD6_DTW_Violet import calc_Violet
from GD6_DTW_Yellow import calc_Yellow


context = zmq.Context()
socket = context.socket(zmq.REP)
socket.bind("tcp://*:5555")

while True:
    #  Wait for next request from client
    message = socket.recv()
    #print("Received request: %s" % message)

    
    #  Do some 'work'.
    #  Try reducing sleep time to 0.01 to see how blazingly fast it communicates
    #  In the real world usage, you just need to replace time.sleep() with
    #  whatever work you want python to do, maybe a machine learning task?
    #time.sleep(0.01)

    #  Send reply back to client
    #  In the real world usage, after you finish your work, send your output here
    if(message == b'Blue'):
        socket.send_string(calc_Blue())
    elif (message == b'Green'):
        socket.send_string(calc_Green())
    elif (message == b'Orange'):
        socket.send_string(calc_Orange())
    elif (message == b'Red'):
        socket.send_string(calc_Red()) 
    elif (message == b'Violet'):
        socket.send_string(calc_Violet())
    elif (message == b'Yellow'):
        socket.send_string(calc_Yellow())
    else:
        socket.send_string("ERROR IN SERVER")


